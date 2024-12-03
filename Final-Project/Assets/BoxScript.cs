using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
  [Header("Track Border Settings")]
    public Transform trackCenter; // Center of the track
    public float trackWidth = 100f; // Width of the track
    public float trackHeight = 100f; // Increased height of the barriers
    public float borderThickness = 100f; // Thickness of the borders
    public float borderYPosition = 100f; // Height offset for the borders

    void Start()
    {
        if (trackCenter == null) Debug.LogWarning("Track center not set. Defaulting to (0,0,0).");
        CreateBorders();
    }

    void CreateBorders()
    {
        float halfWidth = trackWidth / 2;

        // Create the four borders with adjusted Y position
        CreateBorder("LeftBorder", new Vector3(-halfWidth, borderYPosition, 0), new Vector3(borderThickness, trackHeight, trackWidth));
        CreateBorder("RightBorder", new Vector3(halfWidth, borderYPosition, 0), new Vector3(borderThickness, trackHeight, trackWidth));
        CreateBorder("TopBorder", new Vector3(0, borderYPosition, halfWidth), new Vector3(trackWidth, trackHeight, borderThickness));
        CreateBorder("BottomBorder", new Vector3(0, borderYPosition, -halfWidth), new Vector3(trackWidth, trackHeight, borderThickness));
    }

    void CreateBorder(string name, Vector3 position, Vector3 size)
    {
        GameObject border = new GameObject(name);
        border.transform.SetParent(transform);

        if (trackCenter != null)
            border.transform.position = position + trackCenter.position;
        else
            border.transform.position = position;

        BoxCollider collider = border.AddComponent<BoxCollider>();
        collider.size = size;

        Debug.Log($"Created border: {name} at {border.transform.position} with size {size}");

        // Optional visualization
        MeshRenderer renderer = border.AddComponent<MeshRenderer>();
        renderer.material = new Material(Shader.Find("Standard"));
        renderer.material.color = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red
    }

}
