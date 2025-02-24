using UnityEngine;

public class HeatmapGenerator : MonoBehaviour
{
    public GameObject[] heatmapCells;
    public PressureSensorDataReceiver dataReceiver;

    void Update()
    {
        for (int i = 0; i < heatmapCells.Length; i++)
        {
            float intensity = Mathf.InverseLerp(0, 1023, dataReceiver.sensorValues[i]);
            heatmapCells[i].GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, intensity);
        }
    }
}
