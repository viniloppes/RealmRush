using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0f, 0f);
    //Waypoint waypoint = null;
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        //waypoint = GetComponentInParent<Waypoint>();

        DisplayCoordinates();

    }
    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying == false)
        {
            UpdateObjectName();
            DisplayCoordinates();

        }
        ToggleLables();
        SetLabelColor();
    }
    void ToggleLables()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }

    }
    void SetLabelColor()
    {
        if (gridManager == null) { return; }
        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }

        if (node.isWalkable == false)
        {
            label.color = blockedColor;
        } else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        } else
        {
            label.color = defaultColor;
        }
        //if (waypoint.IsPlaceable == true)
        //{
        //    label.color = defaultColor;
        //}
        //else
        //{
        //    label.color = blockedColor;
        //}
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        Debug.Log(gridManager.UnityGridSize);
        coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + ", " + coordinates.y;

    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
