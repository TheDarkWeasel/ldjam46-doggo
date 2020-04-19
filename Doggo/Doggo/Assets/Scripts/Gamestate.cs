using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    int sheepCount = 0;

    void Start()
    {
        sheepCount = FindObjectsOfType<SheepController>().Length;
        Debug.Log(sheepCount + " sheep found!");
    }

    void Update()
    {
        
    }

    public bool IsGameOver()
    {
        return sheepCount <= 0;
    }
}
