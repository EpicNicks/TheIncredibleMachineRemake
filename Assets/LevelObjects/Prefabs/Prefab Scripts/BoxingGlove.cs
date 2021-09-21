using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BoxingGlove : MonoBehaviour
{
    private bool isPunching = false;
    private float nextPunchDelaySeconds;
    public float punchForceMultiplier = 1.0f;
    private Transform startPoint;
    private Rigidbody rbod;
    private AudioSource audioSource;

    [Tooltip("The destination the boxing glove will complete the punch at.")]
    public Transform punchDestination;
    [Tooltip("The delay to launch the first punch if punchActivation is ON_TIMER.")]
    public float punchInitialDelaySeconds = 0.0f;
    [Tooltip("The delay to launch a punch inbetween punches if punchActivation is ON_TIMER.")]
    public float punchDelaySeconds = 1.0f;
    
    [Tooltip("The amount of time it takes for the punch to arrive at its destination.")]
    public float punchSeconds = 1.0f;
    [Tooltip("The amount of time for the boxing glove to wait before returning to its start point.")]
    public float punchStaySeconds = 0.5f;
    [Tooltip("The amount of time for the boxing glove to return to its start point.")]
    public float punchReturnSeconds = 1.5f;
    
    [Tooltip("The way the punch activates.")]
    public PunchActivation punchActivation = PunchActivation.ON_PLAYER_CONTACT;
    [Tooltip("The way the punch moves.")]
    public PunchMovement punchMoveType;
    [Tooltip("The way the punch returns.")]
    public PunchMovement returnMoveType;

    public AudioClip punchSound;

    private delegate Vector3 Move(Vector3 a, Vector3 b, float t);
    public enum PunchMovement
    {
        LINEAR,
        SMOOTH
    }
    public enum PunchActivation
    {
        ON_PLAYER_CONTACT,
        ON_TIMER
    }

    private void Awake()
    {
        GameObject go = new GameObject($"BoxingGlove{{name = {name}}}::StartingPoint");
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        startPoint = go.transform;

        rbod = GetComponent<Rigidbody>();
        if (punchSound)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = punchSound;
        }

        nextPunchDelaySeconds = punchInitialDelaySeconds;
    }

    private void Start()
    {
        if (punchDestination == null)
        {
            Debug.LogWarning($"Warning! BoxingGlove{{name = {name}}}::punchDestination is null and the boxing glove will not move!");
        }
    }

    private void Update()
    {
        if (!isPunching && punchActivation == PunchActivation.ON_TIMER)
        {
            nextPunchDelaySeconds -= Time.deltaTime;
            if (nextPunchDelaySeconds <= 0)
            {
                StartCoroutine(FullPunch());
                nextPunchDelaySeconds = punchDelaySeconds;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(-collision.GetContact(0).normal * punchForceMultiplier, ForceMode.Impulse);
            if (!isPunching && punchActivation == PunchActivation.ON_PLAYER_CONTACT)
            {
                StartCoroutine(FullPunch());
            }
        }
    }

    private Move PunchMovementFunc(PunchMovement pm) => pm switch
    {
        PunchMovement.LINEAR => Vector3.Lerp,
        PunchMovement.SMOOTH => Vector3.Slerp,
        _ => Vector3.Lerp
    };

    private IEnumerator FullPunch()
    {
        if (punchDestination == null)
        {
            yield return null;
        }

        isPunching = true;
        yield return Punch(punchDestination, punchSeconds, PunchMovementFunc(punchMoveType));
        yield return new WaitForSeconds(punchStaySeconds);
        yield return Punch(startPoint, punchReturnSeconds, PunchMovementFunc(returnMoveType));
        isPunching = false;
    }

    private IEnumerator Punch(Transform targetT, float timeToMove, Move moveFunc)
    {
        yield return Punch(targetT.position, timeToMove, moveFunc);
    }

    private IEnumerator Punch(Vector3 target, float timeToMove, Move moveFunc)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
        moveFunc ??= Vector3.Lerp;
        Vector3 startPoint = transform.position;
        float timeElapsed = 0.0f;
        while (!Mathf.Approximately(transform.position.magnitude, target.magnitude))
        {
            rbod.MovePosition(moveFunc(startPoint, target, timeElapsed / timeToMove));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        if (audioSource)
        {
            audioSource.Stop();
        }
    }
}
