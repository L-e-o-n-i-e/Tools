using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public enum EnemyType { FishLamp, Shark, Ray }

public class GameManager : MonoBehaviour
{
    public Transform worldBounds;

    FactoryPoolSystem<EnemyType, Test, ObjStats> enemyPoolSystem;

    private int enenyTypeIndex = 0;
    private int NB_ENEMY_TYPES;

    #region Singleton
    private static GameManager instance = null;
    private GameManager() { }
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                GameObject.DontDestroyOnLoad(instance);
                if (!instance)
                {
                    Debug.LogError("Attempted to grab gameManage instance =  failedKD. Have you tried  grabbing game manager instance outside of main game scene ?");
                }
            }
            return GameManager.instance;
        }
    }
    #endregion

    #region Timer
    float timeToSpawnEnemy = 0;
    public float waitBeforeSpawnEnemy = 5.0f;
    #endregion

    private void Start()
    {
        enemyPoolSystem = new FactoryPoolSystem<EnemyType, Test, ObjStats>();
        enemyPoolSystem.Initialize();

        NB_ENEMY_TYPES = Enum.GetNames(typeof(EnemyType)).Length;
        timeToSpawnEnemy = Time.time + waitBeforeSpawnEnemy;

        InstantiateEnemy((EnemyType)enenyTypeIndex);
    }
    private void Update()
    {
        enemyPoolSystem.Refresh();
        //After a certain time, spawn new enemies
        if (Time.time >= timeToSpawnEnemy)
        {
            InstantiateEnemy((EnemyType)enenyTypeIndex);
            enenyTypeIndex = ++enenyTypeIndex % NB_ENEMY_TYPES;

            timeToSpawnEnemy = Time.time + waitBeforeSpawnEnemy;
        }
    }

    private void FixedUpdate()
    {
        enemyPoolSystem.FixedRefresh();
    }

    private void InstantiateEnemy(EnemyType enemyType)
    {
        //Set Random Position
        Vector3 position = ExtensionFunctions.ExtensionFuncs.RandomStartPosition(worldBounds);
        enemyPoolSystem.Create(enemyType, new ObjStats(2, position.ToFloatArr()));

        //Switch in between different types of enemies in the enum:
        enenyTypeIndex = ++enenyTypeIndex % NB_ENEMY_TYPES;
    }
}