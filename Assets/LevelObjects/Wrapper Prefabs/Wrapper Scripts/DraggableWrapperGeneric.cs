using UnityEngine;

public class DraggableWrapperGeneric : MonoBehaviour
{
    [SerializeField]
    private Collider col;

    public bool dragging;
    public GridPoint placedOn;
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
        if (col == null)
        {
            col = GetComponentInChildren<Collider>();
        }
    }

    private void Start()
    {
        dragging = true;
        col.enabled = false;
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000))
                {
                    Debug.Log(hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.CompareTag("GridPoint"))
                    {
                        GridPoint gp = hit.collider.GetComponent<GridPoint>();
                        gp.PlacedObject = gameObject;
                        placedOn = gp;

                        transform.position = hit.transform.position;
                        transform.rotation = Quaternion.identity;
                        dragging = false;
                        col.enabled = true;
                        placeable.Place();
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                gridGenerator.displayGrid = false;
            }
        }
    }

    public void ReGrab()
    {
        col.enabled = false;
        placeable.Unplace();
        dragging = true;
        placedOn.PlacedObject = null;
        gridGenerator.displayGrid = true;
    }

    private void OnDestroy()
    {
        if (toyDrawerSelector)
        {
            toyDrawerSelector.Instantiated--;
        }
    }
}
