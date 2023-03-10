using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType { FishLamp, Shark, Ray }

public class GameManager : MonoBehaviour
{
    public int nbObEnemies = 3;

    private Factory<EnemyType, Test, ObjStats> factory;

    private void Start()
    {
        factory = new Factory<EnemyType, Test, ObjStats>();
        factory.Instantiate();

        PoolGeneric<EnemyType, Test, ObjStats>.Instance.Instantiate();
    }

    private void InstantiateEnemies(int nbOfEnemies)
    {
        for (int i = 0; i < nbOfEnemies; i++)
        {
            factory.CreateObject(EnemyType.FishLamp, new ObjStats(2) { });

        }
    }
}
