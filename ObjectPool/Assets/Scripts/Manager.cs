using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType>
{
    HashSet<ObjectType> collection;
    Factory<EnumType, ObjectType, DataType> factory;

    public void Initialize(Factory<EnumType, ObjectType, DataType> factory)
    {
        this.factory = factory;
    }

    public void Refresh()
    {

    }

    public void Add()
    {

    }

    public void Remove()
    {

    }

    private void SpawnEnemy<T>(T ennemyType, int nbEnemies = 1)
    {
        for (int i = 0; i < nbEnemies; i++)
        {
            //Create Enemies by asking the factory
        }
    }

}
