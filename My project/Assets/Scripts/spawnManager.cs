using System;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    //Spawn Manger Variables.
    private GameObject spawnManagerObj;
    private Transform[] spawnPoints;

    //Interval variables.
    private float SpawnRate;
    private float startTime;
    private float lastSpawn;

    //Prefab variables.
    [SerializeField] private GameObject enemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialize spawn manager variables.
        spawnManagerObj = GameObject.Find("SpawnManager");
        spawnPoints = new Transform[spawnManagerObj.transform.childCount];

        //Iterate through the SpawnManger object's children and add their transforms to the spawnPoints array.
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnManagerObj.transform.GetChild(i);
        }

        startTime = Time.time;
        lastSpawn = Time.time;
        SpawnRate = 0.75f;

    }

    // Update is called once per frame
    void Update()
    {
        //Spawner Logic
        if (Time.time - lastSpawn >= SpawnRate)
        {
            //Debug.Log("Spawn");
            lastSpawn = Time.time;

            Instantiate(enemyPrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, spawnManagerObj.transform.rotation);
        }
    }
}
