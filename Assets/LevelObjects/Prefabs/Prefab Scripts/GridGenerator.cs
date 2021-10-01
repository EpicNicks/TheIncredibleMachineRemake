using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //possibly optimize this later to use an empty child for setting one inactive instead of every single gridpoint
    private List<GameObject> gridPoints = new List<GameObject>();
    private GameObject gridParent;

    public bool displayGrid = false;

    public float gridPointSphereRadius = 0.1f;
    public GameObject pointPrefab;
    public Vector2 gridPointsBounds;
    public Vector2 gridPointsSpaceBetween;

    private void OnDrawGizmos()
    {
        foreach (Vector3 point in GenerateGridPoints())
        {
            Gizmos.DrawSphere(point + new Vector3(transform.position.x, transform.position.y), gridPointSphereRadius);
        }
    }

    private void Start()
    {
        if (pointPrefab)
        {
            gridParent = new GameObject("GridParent");
            gridParent.transform.parent = transform;
            gridParent.transform.position = Vector3.zero;
            gridParent.SetActive(displayGrid);
            foreach (Vector3 point in GenerateGridPoints())
            {
                gridPoints.Add(Instantiate(pointPrefab, point + gridParent.transform.position, gridParent.transform.rotation, gridParent.transform));
            }
        }
        else
        {
            Debug.LogWarning("No prefab present for pointPrefab");
        }
    }

    private void Update()
    {
        gridParent.SetActive(displayGrid);
    }

    private List<Vector2> GenerateGridPoints()
    {
        List<Vector2> points = new List<Vector2>();
        if (gridPointsSpaceBetween.x > 0 && gridPointsSpaceBetween.y > 0)
        {
            for (float x = 0; x < gridPointsBounds.x; x += gridPointsSpaceBetween.x)
            {
                for (float y = 0; y < gridPointsBounds.y; y += gridPointsSpaceBetween.y)
                {
                    points.Add(new Vector3(x, y));
                }
            }
        }
        return points;
    }
}
