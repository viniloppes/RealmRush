using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
   public  Dictionary<Vector2Int, Node> Grid {  get { return grid; } }
    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int position)
    {
        if (grid.ContainsKey(position))
        {
            return grid[position];
        } 
        return null;
    }
    void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {

                Vector2Int coordinates = new Vector2Int(i, j);
                Node node = new Node(coordinates, true);
                grid.Add(coordinates, node);
                Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }
}
