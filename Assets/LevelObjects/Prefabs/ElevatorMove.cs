using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class ElevatorMove : MonoBehaviour
{

    /*
     * strictly private fields
     */
    private bool completed = false;
    private bool returning = false;
    private Transform startPoint;
    private bool flag = false;

    /*
     * Editor Fields
     */
    [SerializeField]
    private bool returnToOrigin = false;
    [SerializeField]
    private float moveSeconds = 1.0f;
    [SerializeField]
    private float returnSeconds = 1.0f;
    [SerializeField]
    private float waitToReturnSeconds = 0.5f;
    [SerializeField]
    private Transform endPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && endPoint)
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
        GameObject go = new GameObject($"{name}::startPoint");
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;
        startPoint = go.transform;
    }

    private IEnumerator FullMove()
    {
        yield return MoveTowards(endPoint.position, moveSeconds, 0.0f);
        if (returnToOrigin)
        {
            yield return MoveTowards(startPoint.position, returnSeconds, waitToReturnSeconds);
        }
    }

    private IEnumerator MoveTowards(Vector3 target, float timeToMove, float pauseBefore = 0.0f)
    {
        Vector3 startPoint = transform.position;
        yield return new WaitForSeconds(pauseBefore);
        float timeElapsed = 0.0f;
        while (!Mathf.Approximately(transform.position.magnitude, target.magnitude))
        {
            transform.position = Vector3.Lerp(startPoint, target, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
