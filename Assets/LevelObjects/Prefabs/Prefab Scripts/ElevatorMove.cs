using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ElevatorMove : Placeable
{
    private const float SCALING_FACTOR = 1.2f;
    /*
     * strictly private fields
     */
    private bool started = false;
    private bool completed = false;
    private Vector3 startPoint;
    private Rigidbody rbod;
    private AudioSource audioSource;

    /*
     * Editor Fields
     */
    [SerializeField]
    private GameObject tubePivot;
    [SerializeField]
    private List<string> collisionTags = new List<string> { "Player" };
    [SerializeField]
    [Range(0, 86400)]
    [Tooltip("amount of time (in seconds) for the elevator to wait before departing to its destination")]
    private float delaySeconds = 0.0f;
    [SerializeField]
    [Range(0, 86400)]
    [Tooltip("amount of time (in seconds) for the elevator to reach its destination")]
    private float moveSeconds = 1.0f;
    [SerializeField]
    [Tooltip("Activating this will cause the elevator to return to its origin upon completing its travel")]
    private bool returnToOrigin = false;
    [SerializeField]
    [Range(0, 86400)]
    [Tooltip("amount of time (in seconds) for the elevator to return to its start point (only if return to origin is checked)")]
    private float returnSeconds = 1.0f;
    [SerializeField]
    [Range(0, 86400)]
    [Tooltip("amount of time (in seconds) for the elevator to wait before returning to its start point (only if return to origin is checked)")]
    private float waitToReturnSeconds = 0.5f;
    [SerializeField]
    [Tooltip("The destination distance.")]
    private Vector3 destinationDistance;
    [SerializeField]
    private AudioClip elevatorMusic;

    private void OnCollisionEnter(Collision collision)
    {
        if (!started && !completed && collisionTags.Any(t => collision.gameObject.CompareTag(t)))
        {
            StartCoroutine(FullMove());
        }
    }

    private void Awake()
    {
        startPoint = transform.position;

        rbod = GetComponent<Rigidbody>();
        if (elevatorMusic)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = elevatorMusic;
        }
    }

    private void Start()
    {
        base.OnStart();
    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }

    public override void Place()
    {
        base.Place();
        startPoint = transform.position;
    }

    public override void Unplace()
    {
        base.Unplace();
    }

    private IEnumerator FullMove()
    {
        started = true;

        yield return MoveTowards(startPoint + destinationDistance, moveSeconds, delaySeconds);
        if (returnToOrigin)
        {
            yield return MoveTowards(startPoint, returnSeconds, waitToReturnSeconds);
        }

        completed = true;
    }

    private IEnumerator MoveTowards(Vector3 target, float timeToMove, float pauseBefore = 0.0f)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
        Vector3 startPoint = transform.position;
        Vector3 originalTubeScale = Vector3.one;
        if (tubePivot)
        {
            originalTubeScale = tubePivot.transform.localScale;
        }
        yield return new WaitForSeconds(pauseBefore);
        float timeElapsed = 0.0f;
        while (!Mathf.Approximately(transform.position.magnitude, target.magnitude))
        {
            rbod.MovePosition(Vector3.Lerp(startPoint, target, timeElapsed / timeToMove));
            if (tubePivot)
            {
                tubePivot.transform.localScale = Vector3.Lerp(originalTubeScale, originalTubeScale + new Vector3(0, 1f / 1.7f * target.y - tubePivot.transform.position.y, 0), timeElapsed / timeToMove);
            }
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
