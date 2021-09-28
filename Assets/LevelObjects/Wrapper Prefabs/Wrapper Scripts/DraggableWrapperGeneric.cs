using UnityEngine;

public class DraggableWrapperGeneric : MonoBehaviour
{
    private bool dragging;
    public GridGenerator gridGenerator;
    public ToyDrawerSelector toyDrawerSelector;
    public Placeable placeable;

    private void Awake()
    {
        if (gridGenerator == null)
        {
            gridGenerator = GameObject.Find("Grid").GetComponent<GridGenerator>();
        }
        if (placeable == null)
        {
            placeable = GetComponent<Placeable>();
        }
    }

    private void Start()
    {
        dragging = true;
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000))
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.CompareTag("GridPoint"))
                    {
                        GridPoint gp = hit.collider.GetComponent<GridPoint>();
                        gp.PlacedObject = gameObject;

                        transform.position = hit.transform.position;
                        transform.rotation = Quaternion.identity;
                        dragging = false;
                        gridGenerator.displayGrid = false;
                        GetComponent<Collider>().enabled = true;
                        placeable.Place();
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (toyDrawerSelector)
        {
            toyDrawerSelector.Instantiated--;
        }
    }
}
