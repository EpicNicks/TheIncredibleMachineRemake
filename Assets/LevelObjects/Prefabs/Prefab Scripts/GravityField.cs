using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravityField : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Edit bounds by editing the box collider")]
    private BoxCollider col;

    [SerializeField]
    private float inverseGravityMultiplier = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }

    private void OnTriggerEnter(Collider other)
    {

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
        
    }

    private void Awake()
    {
        col.isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
