using UnityEngine;

public class GraphPlotter : MonoBehaviour
{
    [Header("Graph Settings")]
    public GameObject pointPrefab; // Prefab for points (sphere/cube)
    public int resolution = 100;   // Number of points to plot
    public float scale = 1.0f;     // Scale of the graph
    public float step = 1f;        // Distance between x values (in degrees)

    void Start()
    {
        if (pointPrefab == null)
        {
            Debug.LogError("Point Prefab is not assigned in the Inspector.");
            return;
        }

        GenerateGraph();
    }

    void GenerateGraph()
    {
        float halfRes = resolution / 2f;

        for (int i = 0; i < resolution; i++)
        {
            float x = (i - halfRes) * step;
            float y = Mathf.Sin(x * Mathf.Deg2Rad); // <- Fixed: radians!
            Vector3 position = new Vector3(x, y, 0) * scale;

            GameObject point = Instantiate(pointPrefab, transform.position + position, Quaternion.identity, transform);
            point.transform.localScale = Vector3.one * 0.05f;
        }
    }
}
