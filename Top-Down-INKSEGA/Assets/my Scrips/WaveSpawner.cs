using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int enemyCount;
    public float spawnInterval;
    public GameObject[] enemyTypes;
    
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    private Wave currentwave;
    private int currentWaveIndex;
    
    private bool canSpawn = true;
    [SerializeField] Text WaveCounter;
    private float nextSpawnTime;
    private void Update()
    {
        currentwave = waves[currentWaveIndex];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0 && !canSpawn)
        {
            
            canSpawn = true;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
            }
            else
            {
                endGame();
            }
        }
    }
    
    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            WaveCounter.text = "Wave: " + (currentWaveIndex + 1);
            GameObject randomEnemy = currentwave.enemyTypes[Random.Range(0, currentwave.enemyTypes.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, Quaternion.identity); 
            currentwave.enemyCount--;
            nextSpawnTime = Time.time + currentwave.spawnInterval;
            if (currentwave.enemyCount == 0)
            {
                canSpawn = false;
            }
        }
        
       
    }
    void endGame()
    {
        Debug.Log("Game Over");
    }
}
