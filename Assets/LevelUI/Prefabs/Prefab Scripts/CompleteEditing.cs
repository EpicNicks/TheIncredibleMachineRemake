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
            rbod.isKinematic = true;
        }
        playerStartPos = player.transform.position;
        
        unsettable.AddRange(FindObjectsOfType<Placeable>());
    }

    public void UndoFinishedEditing()
    {
        player.transform.position = playerStartPos;
        Rigidbody[] rbods = FindObjectsOfType<Rigidbody>();
        foreach (var rbod in rbods)
        {
            rbod.isKinematic = true;
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
            rbod.isKinematic = false;
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
