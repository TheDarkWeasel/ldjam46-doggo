using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    [SerializeField]
    private float millisTillSpawn = 3000;

    [SerializeField]
    private GameObject enemyPrefab = null;

    private float accumulatedDelta = 0;

    private WolfSpawn[] wolfSpawns;

    private void Start()
    {
        wolfSpawns = FindObjectsOfType<WolfSpawn>();
    }

    void Update()
    {
        float timeChangeInMillis = Time.deltaTime * 1000;
        accumulatedDelta += timeChangeInMillis;

        if (accumulatedDelta > millisTillSpawn)
        {
            accumulatedDelta = 0;
            GameObject instantiatedObject = Instantiate(enemyPrefab);

            Vector3 spawn = wolfSpawns[Random.Range(0, wolfSpawns.Length)].gameObject.transform.position;
            instantiatedObject.transform.position = spawn;
        }
    }
}
