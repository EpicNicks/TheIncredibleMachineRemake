using UnityEngine;
public class Gun : MonoBehaviour
{
    private float fireTime = 0.0f;

    [Tooltip("Check this off to enable the gun to fire. Can be enabled from an external script.")]
    public bool startFiring = false;
    [SerializeField]
    [Tooltip("The amount of seconds inbetween shots fired")]
    private float fireCooldownSeconds = 1.0f;
    [SerializeField]
    [Tooltip("The bullet GameObject to be instantiated by the gun")]
    private GameObject bulletPrefab;

    private void Awake()
    {
        fireTime = fireCooldownSeconds;
    }

    private void Update()
    {
        if (startFiring)
        {
            if (fireTime > fireCooldownSeconds)
            {
                fireTime = 0.0f;
                FireBullet();
            }
            fireTime += Time.deltaTime;
        }
    }

    private void FireBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
