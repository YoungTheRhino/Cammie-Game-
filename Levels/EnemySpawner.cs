using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : spawnbeacon
{
    //Generic List to store all current enemies
    //public GameObject[] enemylist;
    public GameObject enemy;

    void Start()
    {
        

    }
    public void SpawnEnemy(GameObject enemytoSpawn)
    {
        if (enemytoSpawn != null)
        {
            Debug.Log("SpawnEnemy");
            enemy = Instantiate(enemytoSpawn, this.transform.position, this.transform.rotation) as GameObject;
            enemy.GetComponent<EnemyScript>().AssignLevData(levelData);
        }
        
    }
    public void Destroy()
    {
        Destroy(enemy);
    }
}
