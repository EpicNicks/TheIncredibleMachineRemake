using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test_MOVEMENT : MonoBehaviour
{
    Vector3 temp = new Vector3(7.0f, 0, 0);
    void Update()
    {
        transform.position += temp* Time.deltaTime;

    }
}
