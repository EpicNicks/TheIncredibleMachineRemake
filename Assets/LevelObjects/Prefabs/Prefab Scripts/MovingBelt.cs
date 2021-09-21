using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MovingBelt : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Check this if you want the belt to move right to left")]
    private bool reverseDirection = false;
    [SerializeField]
    [Tooltip("The relative speed objects will be pushed on contact with this. A negative value will apply opposite velocity.")]
    private float pushSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(Vector3.right * pushSpeed * (reverseDirection ? -1 : 1));
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.rigidbody.AddForce(Vector3.right * pushSpeed * (reverseDirection ? -1 : 1));
    }
}
