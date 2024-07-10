using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
    Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };
    IDictionary<Vector2Int, Node> grid;
    GridManager gridManager;

    Node currentSearchNode;
    Node startNode;
    Node destinationNode;

    Queue<Node> frontier = new Queue<Node>();

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
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
        //ExploreNeighbors();
        startNode = grid[startCoordinates];// new Node(startCoordinates, true);
        destinationNode = grid[destinationCoordinates];// new Node(destinationCoordinates, true);
        GetNewPath();
    }

    List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
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
                //grid[neighboorsCoordinates].isExplored = true;
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (reached.ContainsKey(neighbor.coordinates) == false && neighbor.isWalkable == true)
            {
                neighbor.connectTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    //AMPLIA PRIMEIRA PESQUISA
    void BreadthFirstSearch()
    {
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {

            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {


                isRunning = false;
            }

        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.isPath = true;
        while (currentNode != null)
        {
            currentNode = currentNode.connectTo;
            if (currentNode != null)
            {
                currentNode.isPath = true;
                path.Add(currentNode);
            }


        }

        path.Reverse();
        return path;
    }


    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> path = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (path.Count <= 1)
            {
                GetNewPath();
                return true;

            }
        }
        return false;
    }
}
