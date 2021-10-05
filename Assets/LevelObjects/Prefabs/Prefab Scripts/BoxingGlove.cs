using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BoxingGlove : Placeable
{
    private bool isPunching = false;
    private float nextPunchDelaySeconds;
    public float punchForceMultiplier = 1.0f;
    private Vector3 startpoint;
    private Rigidbody rbod;
    private AudioSource audioSource;
    private Animator boxAnimator;

    [Tooltip("The destination the boxing glove will complete the punch at.")]
    public Vector3 punchDisplacement;
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
    public PunchActivation punchActivation = PunchActivation.ON_CONTACT;
    public List<string> collisionTags = new List<string> { "Player" };
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
        ON_CONTACT,
        ON_TIMER
    }

    private void Awake()
    {
        rbod = GetComponent<Rigidbody>();
        if (punchSound)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = punchSound;
        }
    }

    private void Start()
    {
        boxAnimator = GetComponentInChildren<Animator>();
        base.OnStart();
        startpoint = transform.position;
        nextPunchDelaySeconds = punchInitialDelaySeconds;
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
    private void LateUpdate()
    {
        OnLateUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTags.Any(t => collision.gameObject.CompareTag(t)))
        {
            collision.rigidbody.AddForce(-collision.GetContact(0).normal * punchForceMultiplier, ForceMode.Impulse);
            if (!isPunching && punchActivation == PunchActivation.ON_CONTACT)
            {
                isPunching = true;
                StartCoroutine(FullPunch());
            }
        }
    }

    public override void Place()
    {
        base.Place();
        startpoint = transform.position;
    }

    public override void Unplace()
    {
        base.Unplace();
    }

    private Move PunchMovementFunc(PunchMovement pm) => pm switch
    {
        PunchMovement.LINEAR => Vector3.Lerp,
        PunchMovement.SMOOTH => Vector3.Slerp,
        _ => Vector3.Lerp
    };

    private IEnumerator FullPunch()
    {
        isPunching = true;
        yield return Punch(startpoint + punchDisplacement, punchSeconds, PunchMovementFunc(punchMoveType));
        yield return new WaitForSeconds(punchStaySeconds);
        yield return Punch(startpoint, punchReturnSeconds, PunchMovementFunc(returnMoveType));
        isPunching = false;
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
