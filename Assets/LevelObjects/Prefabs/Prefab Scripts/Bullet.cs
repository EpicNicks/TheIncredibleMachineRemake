using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{

    private float killSeconds = 10.0f;
    private float killTimer = 0.0f;

    [SerializeField]
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
    }

    private void Awake()
    {
        if (rbod == null)
        {
            rbod = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        rbod.MovePosition(rbod.position + transform.rotation * Vector3.right * moveSpeed * Time.deltaTime);

        killTimer += Time.deltaTime;
        if (killTimer >= killSeconds)
        {
            Destroy(gameObject);
        }
    }
}
