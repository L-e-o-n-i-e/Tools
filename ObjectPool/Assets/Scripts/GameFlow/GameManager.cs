using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { FishLamp, Shark, Ray }

public class GameManager : MonoBehaviour
{
    public int nbObEnemies = 3;
    public Transform worldBounds;
    private Transform enemyParent;
    private Transform poolParent;

    private Factory<EnemyType, Test, ObjStats> factory;
    private Manager<EnemyType, Test, ObjStats> enemyManager;
    private int enenyTypeIndex = 0;

    #region Timer
    float timeToSpawnEnemy = 0;
    float waitBeforeSpawnEnemy = 5.0f;
    #endregion

    private void Start()
    {
        #region Parents
        //Empty object in hierarchy to receive the enemies to be pooled and help debugging
        GameObject pool = new GameObject("Pool");
        poolParent = pool.transform;
        //Empty objects to receive active objects during the game
        GameObject enemyGO = new GameObject("Active Enemies");
        enemyParent = enemyGO.transform;
        #endregion

        #region Object Pool Trio
        factory = new Factory<EnemyType, Test, ObjStats>();
        factory.Instantiate();

        PoolGeneric<EnemyType, Test, ObjStats>.Instance.Instantiate();

        enemyManager = new Manager<EnemyType, Test, ObjStats>();
        enemyManager.Initialize(factory, worldBounds, poolParent, enemyParent);
        #endregion

        timeToSpawnEnemy = Time.time + waitBeforeSpawnEnemy;
        InstantiateEnemies(nbObEnemies);
    }
    private void Update()
    {
        enemyManager.Refresh();
        //After a certain time, spawn new enemies
        if (Time.time >= timeToSpawnEnemy)
        {
            InstantiateEnemies(1);
            timeToSpawnEnemy = Time.time + waitBeforeSpawnEnemy;
        }
    }

    private void InstantiateEnemies(int nbOfEnemies =1)
    {
        
        for (int i = 0; i < nbOfEnemies; i++)
        {
            enemyManager.SpawnEnemy((EnemyType)enenyTypeIndex, new ObjStats(2 + i, enemyParent));
            //Switch in between different types of enemies in the enum:
            enenyTypeIndex = ++enenyTypeIndex % Enum.GetNames(typeof(EnemyType)).Length;
        }
    }

}
