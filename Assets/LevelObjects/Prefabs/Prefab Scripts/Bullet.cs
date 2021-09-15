using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("The constant speed the bullet moves at")]
    public float moveSpeed = 1.0f;
    [Tooltip("The amount of force to apply to the colliding object")]
    public float forceMuliplier = 1.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(collision.GetContact(0).normal * forceMuliplier, ForceMode.Impulse);
        }
        if (collision.gameObject.name != "Gun")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.position += transform.rotation * Vector3.right * moveSpeed * Time.deltaTime;
    }
}
