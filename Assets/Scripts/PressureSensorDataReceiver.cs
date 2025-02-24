using UnityEngine;

public class PressureSensorDataReceiver : MonoBehaviour
{
    //SerialPort serialPort = new SerialPort("COM3", 9600); //TODO: 
    public int[] sensorValues = new int[8];
    public float interval = 0.2f;
    private float currentTime = 0f;
    public SaveDataCsv saveDataCsv;

    void Start()
    {
        //serialPort.Open();
    }

    void Update()
    {
        //if (serialPort.IsOpen)
        //{
        //    string data = serialPort.ReadLine();
        //    string[] values = data.Split(',');
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        sensorValues[i] = int.Parse(values[i]);
        //    }
        //}
        if (saveDataCsv.isRecording)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= interval)
            {
                for (int i = 0; i < sensorValues.Length; i++)
                {
                    sensorValues[i] = Random.Range(0, 1023);
                }
                saveDataCsv.LogData();
                currentTime = 0f;
            }
        }
    }

    void OnApplicationQuit()
    {
        //if (serialPort.IsOpen) serialPort.Close();
    }
}
