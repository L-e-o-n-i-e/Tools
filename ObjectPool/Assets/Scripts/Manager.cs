using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class Manager<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType, ObjectType, EnumType>
{
    HashSet<IManagable<DataType, ObjectType, EnumType>> collection;
    List<IManagable<DataType, ObjectType, EnumType>> objToRemove;
    Factory<EnumType, ObjectType, DataType> factory;
    private Transform worldbounds, poolParent, enemyParent;

    public void Initialize(Factory<EnumType, ObjectType, DataType> factory, Transform worldbounds, Transform poolParent, Transform enemyParent)
    {
        this.factory = factory;
        collection = new HashSet<IManagable<DataType, ObjectType, EnumType>>();
        objToRemove = new List<IManagable<DataType, ObjectType, EnumType>>();
        this.worldbounds = worldbounds;
        this.poolParent = poolParent;
        this.enemyParent = enemyParent;

    }

    public void Refresh()
    {
        //Clear the waiting list of objects to remove
        if (objToRemove.Count > 0)
        {
            for (int i = objToRemove.Count - 1; i >= 0; i--)
            {
                if (collection.Contains(objToRemove[i]))
                    collection.Remove(objToRemove[i]);
            }
            objToRemove.Clear();
        }

        //Making a waiting list of objects to be removed from the refresh list
        foreach (IManagable<DataType, ObjectType, EnumType> obj in collection)
        {
            if (obj.IsActive())
                obj.Refresh();

            else
            {
                objToRemove.Add(obj);
                PoolGeneric<EnumType, ObjectType, DataType>.Instance.Pool(obj.GetEnumType(), obj.GetObjType());
            }
        }
    }


    public void SpawnEnemy(EnumType enumType, DataType dataType, int nbEnemies = 1)
    {
        for (int i = 0; i < nbEnemies; i++)
        {
            //Create Enemies by asking the factory
            ObjectType newObj = factory.CreateObject(enumType, dataType);
            //Initialize the enemy
            newObj.Initialize(dataType, enumType);
            collection.Add(newObj);
        }
    }

}
