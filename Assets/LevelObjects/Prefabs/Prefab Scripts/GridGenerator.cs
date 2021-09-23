using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //possibly optimize this later to use an empty child for setting one inactive instead of every single gridpoint
    private List<GameObject> gridPoints = new List<GameObject>();

    public bool displayGrid = false;

    public float gridPointSphereRadius = 0.1f;
    public GameObject pointPrefab;
    public Vector2 gridPointsBounds;
    public Vector2 gridPointsSpaceBetween;

    private void OnDrawGizmos()
    {
        foreach (Vector3 point in GridPoints())
        {
            Gizmos.DrawSphere(point + new Vector3(transform.position.x, transform.position.y), gridPointSphereRadius);
        }
    }

    private void Start()
    {
        if (pointPrefab)
        {
            foreach (Vector3 point in GridPoints())
            {
                gridPoints.Add(Instantiate(pointPrefab, point + new Vector3(transform.position.x, transform.position.y), transform.rotation, transform));
                gridPoints[gridPoints.Count - 1].SetActive(displayGrid);
            }
        }
        else
        {
            Debug.LogWarning("No prefab present for pointPrefab");
        }
    }

    private void Update()
    {
        foreach (var point in gridPoints)
        {
            point.SetActive(displayGrid);
        }
    }

    private List<Vector2> GridPoints()
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
