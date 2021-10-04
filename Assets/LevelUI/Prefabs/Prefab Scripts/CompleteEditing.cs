using System.Collections.Generic;
using UnityEngine;
using System;

public class CompleteEditing : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerStartPos;
    private List<Placeable> unsettable = new List<Placeable>();

    public EventHandler finishedEditingHandler;

    private void Start()
    {
        Rigidbody[] rbods = FindObjectsOfType<Rigidbody>();
        foreach (var rbod in rbods)
        {
            rbod.constraints |= RigidbodyConstraints.FreezePosition;
        }
        player = GameObject.Find("Player");
        playerStartPos = player.transform.position;
        
        unsettable.AddRange(FindObjectsOfType<Placeable>());
    }

    public void UndoFinishedEditing()
    {
        player.transform.position = playerStartPos;
        Rigidbody playerBody = player.GetComponent<Rigidbody>();
        playerBody.velocity = Vector3.zero;
        playerBody.angularVelocity = Vector3.zero;
        
        Rigidbody[] rbods = FindObjectsOfType<Rigidbody>();
        foreach (var rbod in rbods)
        {
            rbod.constraints |= RigidbodyConstraints.FreezePosition;
        }
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var placeable in FindObjectsOfType<Placeable>())
        {
            if (!unsettable.Contains(placeable))
            {
                placeable.interactable = true;
            }
        }
        ToyDrawerSelector[] selectors = FindObjectsOfType<ToyDrawerSelector>();
        foreach (var selector in selectors)
        {
            selector.ToggleEnabled(true);
        }
    }

    public void FinishedEditing()
    {
        Rigidbody[] rbods = FindObjectsOfType<Rigidbody>();
        foreach (var rbod in rbods)
        {
            rbod.constraints &= ~RigidbodyConstraints.FreezePosition;
        }
        finishedEditingHandler?.Invoke(this, EventArgs.Empty);
        foreach (var placeable in FindObjectsOfType<Placeable>())
        {
            placeable.interactable = false;
        }
        ToyDrawerSelector[] selectors = FindObjectsOfType<ToyDrawerSelector>();
        foreach (var selector in selectors)
        {
            selector.ToggleEnabled(false);
        }
    }
}
