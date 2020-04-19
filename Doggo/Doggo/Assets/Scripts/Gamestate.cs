using System;
using UnityEngine;
using UnityEngine.UI;

public class Gamestate : MonoBehaviour
{
    int sheepCount = 0;

    [SerializeField] Text time;
    [SerializeField] Text sheepLeft;

    float timer = 0.0f;

    void Start()
    {
        sheepCount = FindObjectsOfType<SheepController>().Length;
        Debug.Log(sheepCount + " sheep found!");
    }

    void Update()
    {
        sheepLeft.text = "Sheep left: " + sheepCount;

        timer += Time.deltaTime;
        int seconds = (int) (timer % 60);
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        time.text = "Time: " + timeText;
    }

    public void OnSheepKilled()
    {
        sheepCount--;
    }

    public bool IsGameOver()
    {
        return sheepCount <= 0;
    }
}
