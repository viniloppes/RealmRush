using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    Vector2Int coordinates = new Vector2Int();
    TextMeshPro label;
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
    }
    // Update is called once per frame
    void Update()
    {
        DisplayCoordinates();
        if (Application.isPlaying == false)
        {
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.gridSize.x); ;
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.gridSize.z);
        label.text = coordinates.x + ", " + coordinates.y;

    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
