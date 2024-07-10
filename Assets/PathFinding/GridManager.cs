using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    // Word Grid Size - should match with unity editor grid size
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; }}
   public  Dictionary<Vector2Int, Node> Grid {  get { return grid; } }
    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        } 
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;

        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnityGridSize;
        position.z =coordinates.y * UnityGridSize;
        position.y = 0;
        return position;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid) {

            entry.Value.connectTo = null;
            entry.Value.isExplored = false;
            //entry.Value.isWalkable = false; 
            entry.Value.isPath = false;
        
        }
    }

    void CreateGrid()
    {
        for (int i = -1; i < gridSize.x; i++)
        {
            for (int j = -1; j < gridSize.y; j++)
            {

                Vector2Int coordinates = new Vector2Int(i, j);
                Node node = new Node(coordinates, true);
                grid.Add(coordinates, node);
                //Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }
}
