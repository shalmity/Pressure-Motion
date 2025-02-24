using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataCsv : MonoBehaviour
{
    public PressureSensorDataReceiver dataReceiver;
    public string fileName = "TestData.csv"; //TODO: change filename
    public bool isRecording = false;

    private string filePath;
    private float maxPressureInGrams = 10000f;

    int num = 0;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, fileName);

        string header = "Num, Sensor1 (g), Sensor2 (g), Sensor3 (g), Sensor4 (g), Sensor5 (g), Sensor6 (g), Sensor7 (g), Sensor8 (g)";
        File.WriteAllText(filePath, header + "\n");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRecording = !isRecording;
        }
    }
    public void LogData()
    {
        num++;
        string dataRow = num.ToString();
        foreach (int sensorValue in dataReceiver.sensorValues)
        {
            float pressureInGrams = sensorValue * (maxPressureInGrams / 1023f);
            dataRow += $", {pressureInGrams:F2}";
        }

        File.AppendAllText(filePath, dataRow + "\n");
        Debug.Log("데이터 기록: " + dataRow);
    }
}
