using System.Collections.Generic;
using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject pointPrefab;
    public Transform graphParent;

    private List<GameObject> spawnedPoints = new List<GameObject>();
    private LineRenderer lineRenderer;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private GameObject meshHolder;
    private List<GameObject> axisLines = new List<GameObject>();

    public void ClearGraph()
    {
        foreach (var point in spawnedPoints)
            Destroy(point);
        spawnedPoints.Clear();

        if (lineRenderer != null)
            Destroy(lineRenderer.gameObject);

        if (meshHolder != null)
            Destroy(meshHolder);

        foreach (var line in axisLines)
            Destroy(line);
        axisLines.Clear();
    }

    public void GenerateLineGraph() => Generate2DGraph(x => x);
    public void GenerateParabolaGraph() => Generate2DGraph(x => x * x / 10f);
    public void GenerateSineGraph() => Generate2DGraph(Mathf.Sin);
    public void GenerateCosineGraph() => Generate2DGraph(Mathf.Cos);
    public void GenerateTangentGraph() => Generate2DGraph(x => Mathf.Tan(x) * 0.1f);
    public void GenerateExponentialGraph() => Generate2DGraph(x => Mathf.Exp(x / 10f) * 0.1f);
    public void GenerateAbsoluteGraph() => Generate2DGraph(Mathf.Abs);
    public void GenerateSquareRootGraph() => Generate2DGraph(x => Mathf.Sqrt(Mathf.Abs(x)));
    public void Generate3DWaveMesh() => Generate3DWave();

    private void Generate2DGraph(System.Func<float, float> func)
    {
        ClearGraph();

        float range = 10f;
        float step = 0.2f;
        int pointCount = Mathf.CeilToInt((range * 2f) / step) + 1;

        Vector3[] positions = new Vector3[pointCount];
        int index = 0;

        for (float x = -range; x <= range; x += step)
        {
            float y = func(x);
            positions[index++] = new Vector3(x, y, 0);
        }

        GameObject lrObj = new GameObject("LineGraph");
        lrObj.transform.parent = graphParent;
        lineRenderer = lrObj.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = positions.Length;
        lineRenderer.useWorldSpace = false;
        lineRenderer.numCapVertices = 5;
        lineRenderer.SetPositions(positions);

        DrawAxes2D(range);
    }

    private void Generate3DWave()
    {
        ClearGraph();

        float range = 5f;
        float step = 0.5f;
        int gridSize = Mathf.CeilToInt((range * 2f) / step) + 1;

        Vector3[] vertices = new Vector3[gridSize * gridSize];
        int[] triangles = new int[(gridSize - 1) * (gridSize - 1) * 6];
        int triIndex = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                float xCoord = -range + x * step;
                float zCoord = -range + z * step;
                float yCoord = Mathf.Sin(xCoord) * Mathf.Cos(zCoord);

                int i = x * gridSize + z;
                vertices[i] = new Vector3(xCoord, yCoord, zCoord);

                if (x < gridSize - 1 && z < gridSize - 1)
                {
                    int topLeft = i;
                    int topRight = i + 1;
                    int bottomLeft = i + gridSize;
                    int bottomRight = i + gridSize + 1;

                    triangles[triIndex++] = topLeft;
                    triangles[triIndex++] = bottomLeft;
                    triangles[triIndex++] = topRight;

                    triangles[triIndex++] = topRight;
                    triangles[triIndex++] = bottomLeft;
                    triangles[triIndex++] = bottomRight;
                }
            }
        }

        meshHolder = new GameObject("WaveMesh");
        meshHolder.transform.parent = graphParent;
        meshFilter = meshHolder.AddComponent<MeshFilter>();
        meshRenderer = meshHolder.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        DrawAxes3D(range);
    }

    private void DrawAxes2D(float length)
    {
        DrawAxis(Vector3.left * length, Vector3.right * length, Color.red);     // X-axis
        DrawAxis(Vector3.down * length, Vector3.up * length, Color.green);     // Y-axis

        float gridSpacing = 1f;
        for (float i = -length; i <= length; i += gridSpacing)
        {
            DrawAxis(new Vector3(i, -length, 0), new Vector3(i, length, 0), Color.gray * 0.5f); // Vertical lines
            DrawAxis(new Vector3(-length, i, 0), new Vector3(length, i, 0), Color.gray * 0.5f); // Horizontal lines
        }
    }

    private void DrawAxes3D(float length)
    {
        DrawAxis(Vector3.left * length, Vector3.right * length, Color.red);     // X-axis
        DrawAxis(Vector3.down * length, Vector3.up * length, Color.green);     // Y-axis
        DrawAxis(Vector3.back * length, Vector3.forward * length, Color.blue); // Z-axis

        float gridSpacing = 1f;
        for (float i = -length; i <= length; i += gridSpacing)
        {
            DrawAxis(new Vector3(i, 0, -length), new Vector3(i, 0, length), Color.gray * 0.5f); // X lines
            DrawAxis(new Vector3(-length, 0, i), new Vector3(length, 0, i), Color.gray * 0.5f); // Z lines
        }
    }

    private void DrawAxis(Vector3 start, Vector3 end, Color color)
    {
        GameObject axisObj = new GameObject("AxisLine");
        axisObj.transform.parent = graphParent;
        var lr = axisObj.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.widthMultiplier = 0.03f;
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { start, end });
        lr.startColor = color;
        lr.endColor = color;
        lr.useWorldSpace = false;
        axisLines.Add(axisObj);
    }
}
