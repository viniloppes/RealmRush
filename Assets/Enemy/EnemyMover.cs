using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0, 5f)] float speed = 2f;
    Enemy enemy;
    List<Node> path = new List<Node>();

    GridManager gridManager;
    PathFind pathFinder;
    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        StartCoroutine(FollowPath());

    }
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFind>();
    }

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    void RecalculatePath(bool resets)
    {


        Vector2Int coordinates = new Vector2Int();
        if (resets)
        {
            coordinates = pathFinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();

        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
        //GameObject parent = GameObject.FindGameObjectWithTag("Path");
        //foreach (Transform child in parent.transform)
        //{
        //    //Debug.Log(child.name);
        //    Tile waypoint = child.GetComponent<Tile>();
        //    if (waypoint != null)
        //    {
        //        path.Add(waypoint);
        //    }
        //}
        //GameObject[] arrayWaypoint = GameObject.FindGameObjectsWithTag("Path").OrderBy(x => x.gameObject.name).ToArray();
        //foreach (GameObject waypoint in arrayWaypoint)
        //{
        //    Debug.Log(waypoint.name);
        //    path.Add(waypoint.GetComponent<Waypoint>());
        //}
    }

    void ReturnToStart()
    {
        gameObject.transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
        //transform.LookAt(path[1].transform.position);

    }
    IEnumerator FollowPath()
    {
        //List<Waypoint> listaOrdenada =  path.OrderBy(x => x.name).ToList();
        for(int i = 1; i < path.Count; i++) 
        {
            //gameObject.transform.position = waypoint.transform.position;
            //yield return new WaitForSeconds(waitTime);

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.LookAt(endPosition);
                // transform.rotation = waypoint.transform.rotation;
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
