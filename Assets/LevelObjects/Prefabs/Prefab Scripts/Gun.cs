using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Gun : Placeable
{
    private float fireTime = 0.0f;

    [SerializeField]
    private Transform bulletSpawn;
    [SerializeField]
    private AudioClip fireSound;

    [Tooltip("Check this off to enable the gun to fire. Can be enabled from an external script.")]
    public bool startFiring = false;
    public ShootType shootType = ShootType.ON_TIMER;
    public CollisionType collisionType = CollisionType.ON_COLLISION_ENTER;
    public List<string> collisionTags = new List<string> { "Player" };
    [SerializeField]
    [Tooltip("The amount of seconds inbetween shots fired")]
    private float fireCooldownSeconds = 1.0f;
    [SerializeField]
    [Tooltip("The amount of seconds inbetween the same object colliding with the gun and firing")]
    private float onCollisionFireCooldownSeconds = 0.5f;
    [SerializeField]
    [Tooltip("The bullet GameObject to be instantiated by the gun")]
    private GameObject bulletPrefab;

    public enum ShootType
    {
        ON_CONTACT,
        ON_TIMER
    }

    [System.Flags]
    public enum CollisionType
    {
        ON_COLLISION_ENTER = 1,
        ON_COLLISION_STAY = 2,
        ON_COLLISION_EXIT = 4
    }

    private void Awake()
    {
        fireTime = fireCooldownSeconds;
        if (bulletSpawn == null)
        {
            bulletSpawn = transform.GetChild(0);
        }
    }

    private void Start()
    {
        base.OnStart();
    }

    private void Update()
    {
        if (startFiring)
        {
            if (shootType == ShootType.ON_TIMER && fireTime > fireCooldownSeconds)
            {
                fireTime = 0.0f;
                FireBullet();
            }
            fireTime += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionType == CollisionType.ON_COLLISION_ENTER)
        {
            OnCollisionFire(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collisionType == CollisionType.ON_COLLISION_STAY)
        {
            OnCollisionFire(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisionType == CollisionType.ON_COLLISION_EXIT)
        {
            OnCollisionFire(collision);
        }
    }

    public void OnCollisionFire(Collision collision)
    {
        if (startFiring && shootType == ShootType.ON_CONTACT)
        {
            // order is on purpose for performace
            if ((fireTime > onCollisionFireCooldownSeconds) && collisionTags.Any(t => collision.gameObject.CompareTag(t)))
            {
                fireTime = 0.0f;
                FireBullet();
            }
        }
    }

    public override void Place()
    {
        base.Place();
        startFiring = true;
    }

    public override void Unplace()
    {
        base.Unplace();
        startFiring = false;
    }

    private void FireBullet()
    {
        if (fireSound)
        {
            AudioSource.PlayClipAtPoint(fireSound, bulletSpawn.position);
        }
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
