using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rbod;

    [Tooltip("The constant speed the bullet moves at")]
    public float moveSpeed = 1.0f;
    [Tooltip("The amount of force to apply to the colliding object")]
    public float forceMuliplier = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(collision.GetContact(0).normal * -1 * forceMuliplier, ForceMode.Impulse);
        }
        if (collision.gameObject.name != "Gun")
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rbod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rbod.MovePosition(rbod.position + transform.rotation * Vector3.right * moveSpeed * Time.deltaTime);
    }
}
