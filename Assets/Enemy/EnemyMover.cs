using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField][Range(0, 5f)] float speed = 2f;
    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());

    }

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }
    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            //Debug.Log(child.name);
            Tile waypoint = child.GetComponent<Tile>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
        //GameObject[] arrayWaypoint = GameObject.FindGameObjectsWithTag("Path").OrderBy(x => x.gameObject.name).ToArray();
        //foreach (GameObject waypoint in arrayWaypoint)
        //{
        //    Debug.Log(waypoint.name);
        //    path.Add(waypoint.GetComponent<Waypoint>());
        //}
    }

    void ReturnToStart()
    {
        gameObject.transform.position = path[0].transform.position;
        transform.LookAt(path[1].transform.position);

    }
    IEnumerator FollowPath()
    {
        //List<Waypoint> listaOrdenada =  path.OrderBy(x => x.name).ToList();
        foreach (Tile waypoint in path)
        {
            //gameObject.transform.position = waypoint.transform.position;
            //yield return new WaitForSeconds(waitTime);

            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
