using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    Waypoint waypoint = null;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
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
        ToggleLable();
        PaintCoordinates();
    }
    void ToggleLable()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }

    }
    void PaintCoordinates()
    {
        if (waypoint.IsPlaceable == true)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {

        coordinates.x = Mathf.RoundToInt(transform.position.x / EditorSnapSettings.gridSize.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / EditorSnapSettings.gridSize.z);
        label.text = coordinates.x + ", " + coordinates.y;

    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
