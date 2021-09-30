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
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().isKinematic = true;
        playerStartPos = player.transform.position;
        
        unsettable.AddRange(FindObjectsOfType<Placeable>());
    }

    public void UndoFinishedEditing()
    {
        player.transform.position = playerStartPos;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var placeable in FindObjectsOfType<Placeable>())
        {
            if (!unsettable.Contains(placeable))
            {
                placeable.interactable = true;
            }
        }
    }

    public void FinishedEditing()
    {
        player.GetComponent<Rigidbody>().isKinematic = false;
        finishedEditingHandler?.Invoke(this, EventArgs.Empty);
        foreach (var placeable in FindObjectsOfType<Placeable>())
        {
            placeable.interactable = false;
        }
    }
}
