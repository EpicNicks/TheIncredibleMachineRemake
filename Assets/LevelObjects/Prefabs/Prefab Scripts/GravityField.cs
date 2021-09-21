using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityField : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    [Tooltip("Edit bounds by editing the box collider")]
    private BoxCollider col;

    [SerializeField]
    private float inverseGravityMultiplier = 1.0f;

    public AudioClip gravityConstantSoundEffect;
    public AudioClip gravityPullingSoundEffect;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gravityPullingSoundEffect)
            {
                audioSource.clip = gravityPullingSoundEffect;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(-Physics.gravity * 2 * inverseGravityMultiplier);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = gravityConstantSoundEffect;
        }
    }

    private void Awake()
    {
        col.isTrigger = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gravityConstantSoundEffect;
    }

    void Start()
    {
        if (audioSource.clip)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        
    }
}
