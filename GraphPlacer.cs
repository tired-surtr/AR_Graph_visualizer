using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GraphPlacer : MonoBehaviour
{
    public GameObject graphPrefab; // Not used now but can be kept if you use prefab graphs later
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;

    public Material graphMaterial;
    public int resolution = 100;
    public float scale = 0.2f;

    private GameObject spawnedGraph;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public void PlaceGraphAtCurrentPosition()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // Destroy previous graph
            if (spawnedGraph != null)
                Destroy(spawnedGraph);

            // Create AR anchor at hit position
            ARAnchor anchor = anchorManager.AddAnchor(hitPose);
            if (anchor == null)
            {
                Debug.Log("Failed to create anchor.");
                return;
            }

            // Create a new GameObject for the graph under the anchor
            spawnedGraph = new GameObject("Graph");
            spawnedGraph.transform.parent = anchor.transform;
            spawnedGraph.transform.localPosition = Vector3.zero;
            spawnedGraph.transform.localRotation = Quaternion.identity;

            // Generate sine graph on this new GameObject
            GenerateSineGraph(spawnedGraph.transform);
        }
        else
        {
            Debug.Log("No AR plane found at screen center.");
        }
    }

    public void ResetGraphPlacement()
    {
        if (spawnedGraph != null)
        {
            Destroy(spawnedGraph);
            spawnedGraph = null;
        }
    }

    private void GenerateSineGraph(Transform parent)
    {
        GameObject lineObj = new GameObject("SineGraph");
        lineObj.transform.parent = parent;

        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.material = graphMaterial;
        lineRenderer.widthMultiplier = 0.01f;
        lineRenderer.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            float x = (i / (float)(resolution - 1)) * 2 * Mathf.PI;
            float y = Mathf.Sin(x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) * scale);
        }

        CreateAxes(parent);
    }

    private void CreateAxes(Transform parent)
    {
        CreateAxis(parent, Vector3.right, Color.red, "X-Axis");
        CreateAxis(parent, Vector3.up, Color.green, "Y-Axis");
        CreateAxis(parent, Vector3.forward, Color.blue, "Z-Axis");
    }

    private void CreateAxis(Transform parent, Vector3 direction, Color color, string name)
    {
        GameObject axis = new GameObject(name);
        axis.transform.parent = parent;

        LineRenderer lr = axis.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Unlit/Color"));
        lr.material.color = color;
        lr.widthMultiplier = 0.01f;
        lr.positionCount = 2;
        lr.SetPosition(0, -direction * 0.5f);
        lr.SetPosition(1, direction * 0.5f);
    }
}
