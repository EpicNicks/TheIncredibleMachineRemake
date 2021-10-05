using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw_block : MonoBehaviour
{
private Rigidbody rbod;
private void Start(){
  if (rbod == null) rbod = GetComponent<Rigidbody>();
}
private void LateUpdate(){
  rbod.constraints |= RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
}

}
