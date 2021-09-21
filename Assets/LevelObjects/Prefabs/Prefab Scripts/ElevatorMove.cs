using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ElevatorMove : MonoBehaviour
{
    /*
     * strictly private fields
     */
    private bool started = false;
    private bool completed = false;
    private Transform startPoint;
    private Rigidbody rbod;

    /*
     * Editor Fields
     */
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
    [Tooltip("The destination transform. Use any dummy GameObject.")]
    private Transform endPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (endPoint && !started && !completed && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FullMove());
        }
    }

    private void Awake()
    {
        if (endPoint == null)
        {
            Debug.LogWarning("Elevator is missing an endpoint and will now function as a platform!");
        }
        GameObject go = new GameObject($"Elevator (Flat){{name = {name}}}::startPoint");
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;
        startPoint = go.transform;

        rbod = GetComponent<Rigidbody>();
    }

    private IEnumerator FullMove()
    {
        started = true;

        yield return MoveTowards(endPoint.position, moveSeconds, delaySeconds);
        if (returnToOrigin)
        {
            yield return MoveTowards(startPoint.position, returnSeconds, waitToReturnSeconds);
        }

        completed = true;
    }

    private IEnumerator MoveTowards(Vector3 target, float timeToMove, float pauseBefore = 0.0f)
    {
        Vector3 startPoint = transform.position;
        yield return new WaitForSeconds(pauseBefore);
        float timeElapsed = 0.0f;
        while (!Mathf.Approximately(transform.position.magnitude, target.magnitude))
        {
            rbod.MovePosition(Vector3.Lerp(startPoint, target, timeElapsed / timeToMove));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
