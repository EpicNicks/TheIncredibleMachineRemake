using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlace : Placeable
{

    public AudioClip elevatorSound;

    // Start is called before the first frame update
    private void Start() {
        base.OnStart();
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().playOnAwake = false;
            GetComponent<AudioSource>().clip = elevatorSound;
        }
    }

    private void LateUpdate() {
        base.OnLateUpdate();
    }

    private void OnCollisionEnter()
    {
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
