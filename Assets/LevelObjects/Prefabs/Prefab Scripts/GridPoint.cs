using UnityEngine;

public class GridPoint : MonoBehaviour
{
    private GameObject placedObject;
    public GameObject PlacedObject
    {
        get => placedObject;
        set
        {
            if (placedObject)
            {
                Destroy(placedObject);
            }
            placedObject = value;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}