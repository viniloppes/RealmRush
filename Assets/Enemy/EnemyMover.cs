using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0, 5f)] float speed = 2f;
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    void FindPath()
    {
        path.Clear();
        GameObject[] arrayWaypoint = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject waypoint in arrayWaypoint)
        {
            Debug.Log(waypoint.name);
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        gameObject.transform.position = path[0].transform.position;
    }
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
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
        gameObject.active = false;
    }
}
