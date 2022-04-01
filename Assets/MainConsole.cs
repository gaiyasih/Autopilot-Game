using System.IO;
using UnityEngine;

public class MainConsole : MonoBehaviour {
    [Header("探索車輛")]
    public GameObject[] CarEnable;

    [Header("目前探索車輛總數")]
    [Range(0, 20)]
    public int currentlyCarEnable;

    [Header("執行時間設定")]
    [Range(0.01f, 100)]
    public float timeSetting;

    [Header("最好的評價函數")]
    public float bestFitnessValue;

    void ReadBestFitnessValueOnCSV() {
        StreamReader r = new StreamReader("./Assets/Data/BestNeurals.csv");
        bestFitnessValue = float.Parse(r.ReadLine());
        r.Close();
    }

    void Update() {
        Time.timeScale = Mathf.Clamp(timeSetting, 0.01f, 100);

        currentlyCarEnable = Mathf.Clamp(currentlyCarEnable, 0, 20);
        for (int i = 0; i < currentlyCarEnable; i++) {
            CarEnable[i].SetActive(true);
        }
        for (int i = currentlyCarEnable; i < CarEnable.Length; i++) {
            CarEnable[i].SetActive(false);
        }
    }

    void Start() {
        currentlyCarEnable = 0;
        timeSetting = 1;
        CarEnable = GameObject.FindGameObjectsWithTag("Cars");
        for (int i = 1; i < CarEnable.Length; i++) {
            CarEnable[i].SetActive(false);
        }
        InvokeRepeating("ReadBestFitnessValueOnCSV", 60, 60);
    }
}
