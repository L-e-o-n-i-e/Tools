using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public enum EnemyType { FishLamp, Shark, Ray }

public class GameManager : MonoBehaviour
{
    public int nbObEnemies = 3;
    public Transform worldBounds;

    private Factory<EnemyType, Test, ObjStats> factory;
    private Manager<EnemyType, Test, ObjStats> enemyManager;
    private int enenyTypeIndex = 0;

    #region Timer
    float timeToSpawnEnemy = 0;
    public float waitBeforeSpawnEnemy = 5.0f;
    #endregion

    private void Start()
    {
        #region Object Pool Trio
        factory = new Factory<EnemyType, Test, ObjStats>();
        factory.Instantiate();

        PoolGeneric<EnemyType, Test, ObjStats>.Instance.FirstEverInitialize();

        enemyManager = new Manager<EnemyType, Test, ObjStats>();
        enemyManager.Initialize(factory, worldBounds);
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
            InstantiateEnemies(50);
            timeToSpawnEnemy = Time.time + waitBeforeSpawnEnemy;
        }
    }

    private void FixedUpdate()
    {
        enemyManager.FixedRefresh();
    }

    private void InstantiateEnemies(int nbOfEnemies =1)
    {        
        for (int i = 0; i < nbOfEnemies; i++)
        {
            //Set Random Position
            Vector3 position = ExtensionFunctions.ExtensionFuncs.RandomStartPosition(worldBounds);
            enemyManager.Spawn((EnemyType)enenyTypeIndex, new ObjStats(2 + i, position.ToFloatArr()));
            //Switch in between different types of enemies in the enum:
            enenyTypeIndex = ++enenyTypeIndex % Enum.GetNames(typeof(EnemyType)).Length;
        }
    }

}
