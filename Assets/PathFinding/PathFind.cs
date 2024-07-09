using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    IDictionary<Vector2Int, Node> grid;
    GridManager gridManager;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;

        }
    }
    private void Start()
    {
        ExploreNeighbors();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (var direction in directions)
        {
            Vector2Int neighboorsCoordinates = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighboorsCoordinates))
            {
                neighbors.Add(grid[neighboorsCoordinates]);
                grid[neighboorsCoordinates].isExplored = true;
                grid[currentSearchNode.coordinates].isPath = true;
            }
        }
    }
}
