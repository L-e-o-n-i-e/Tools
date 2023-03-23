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

    private void Start()
    {
        //Empty object in hierarchy to receive the enemies to be pooled and help debugging
        GameObject pool = new GameObject("Pool");
        poolParent = pool.transform;
        //Empty objects to receive active objects during the game
        GameObject enemyGO = new GameObject("Active Enemies");
        enemyParent = enemyGO.transform;

        factory = new Factory<EnemyType, Test, ObjStats>();
        factory.Instantiate();

        PoolGeneric<EnemyType, Test, ObjStats>.Instance.Instantiate();

        enemyManager = new Manager<EnemyType, Test, ObjStats>();
        enemyManager.Initialize(factory, worldBounds, poolParent, enemyParent);

        InstantiateEnemies(nbObEnemies);
    }
    private void Update()
    {
        enemyManager.Refresh();
    }

    private void InstantiateEnemies(int nbOfEnemies)
    {
        int enenyTypeIndex = 0;
        for (int i = 0; i < nbOfEnemies; i++)
        {
            enemyManager.SpawnEnemy((EnemyType)enenyTypeIndex, new ObjStats(2 + i, enemyParent));
            //Switch in between different types of enemies in the enum:
            enenyTypeIndex = ++enenyTypeIndex % Enum.GetNames(typeof(EnemyType)).Length;
        }
    }

}
