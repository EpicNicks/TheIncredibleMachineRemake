using UnityEngine;

public class GridPoint : MonoBehaviour
{
    private GameObject placedObject;
    public GameObject PlacedObject
    {
        get => placedObject;
        set
        {
            placedObject = value;
        }
    }
}
