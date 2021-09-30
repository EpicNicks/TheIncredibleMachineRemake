using System.Collections.Generic;
using UnityEngine;
using System;

public class CompleteEditing : MonoBehaviour
{
    private Vector3 playerStartPos;
    private List<Placeable> unsettable = new List<Placeable>();

    public EventHandler finishedEditingHandler;

    private void Start()
    {
        playerStartPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        unsettable.AddRange(FindObjectsOfType<Placeable>());
    }

    public void UndoFinishedEditing()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerStartPos;
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
        finishedEditingHandler?.Invoke(this, EventArgs.Empty);
        foreach (var placeable in FindObjectsOfType<Placeable>())
        {
            placeable.interactable = false;
        }
    }
}
