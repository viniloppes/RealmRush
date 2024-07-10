using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] private bool isPlaceable = true;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    PathFind pathFinder;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFind>();
    }
    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (isPlaceable == false)
            {
                gridManager.BlockNode(coordinates);
            }

        }

    }
    private void OnMouseDown()
    {
        Debug.Log(gridManager.GetNode(coordinates).isWalkable + " - " + pathFinder.WillBlockPath(coordinates));
        if (gridManager.GetNode(coordinates).isWalkable && pathFinder.WillBlockPath(coordinates) == false) 
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            //Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
}
