using UnityEngine;
using TMPro;

public class GraphUIManager : MonoBehaviour
{
    public GraphVisualizer graphVisualizer;
    public GraphPlacer graphPlacer;
    public GameObject graphButtonsPanel;
    public TextMeshProUGUI graphTitle;

    public void ShowLineGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateLineGraph();
        graphTitle.text = "y = x";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowParabolaGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateParabolaGraph();
        graphTitle.text = "y = x²";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowSineGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateSineGraph();
        graphTitle.text = "y = sin(x)";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowCosineGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateCosineGraph();
        graphTitle.text = "y = cos(x)";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowTangentGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateTangentGraph();
        graphTitle.text = "y = tan(x)";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowExponentialGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateExponentialGraph();
        graphTitle.text = "y = e^x";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowAbsoluteGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateAbsoluteGraph();
        graphTitle.text = "y = |x|";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ShowSquareRootGraph()
    {
        ResetGraph();
        graphVisualizer.GenerateSquareRootGraph();
        graphTitle.text = "y = √x";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void Show3DWaveMesh()
    {
        ResetGraph();
        graphVisualizer.Generate3DWaveMesh();
        graphTitle.text = "z = sin(x) + cos(y)";
        graphPlacer.PlaceGraphAtCurrentPosition(); // Ensure graph is placed after creation, but only once
    }

    public void ResetGraph()
    {
        graphVisualizer.ClearGraph();              // clear old graph
        graphPlacer.ResetGraphPlacement();         // remove old AR object
        graphButtonsPanel.SetActive(false);        // hide buttons after selection
    }

    // Optionally add a method to re-enable the buttons if you need to select a different graph
    public void EnableGraphButtons()
    {
        graphButtonsPanel.SetActive(true);         // show buttons if you want to reset the graph
    }
}
