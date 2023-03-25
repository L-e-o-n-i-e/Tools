using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class Manager<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType, ObjectType, EnumType>
{
    HashSet<IManagable<DataType, ObjectType, EnumType>> collection;
    List<IManagable<DataType, ObjectType, EnumType>> objectsToRemove;
    Factory<EnumType, ObjectType, DataType> factory;

    private Transform worldbounds, objParent;

    public void Initialize(Factory<EnumType, ObjectType, DataType> factory, Transform worldbounds)
    {
        this.factory = factory;
        collection = new HashSet<IManagable<DataType, ObjectType, EnumType>>();
        objectsToRemove = new List<IManagable<DataType, ObjectType, EnumType>>();
        this.worldbounds = worldbounds;
        this.objParent = new GameObject(typeof(ObjectType).Name + "_Parent").transform;
    }

    public void Refresh()
    {
        //Clear the waiting list of objects to remove
        if (objectsToRemove.Count > 0)
        {
            for (int i = objectsToRemove.Count - 1; i >= 0; i--)
            {
                if (collection.Contains(objectsToRemove[i]))
                    collection.Remove(objectsToRemove[i]);
            }
            objectsToRemove.Clear();
        }

        //Making a waiting list of objects to be removed from the refresh list
        foreach (IManagable<DataType, ObjectType, EnumType> obj in collection)
        {
            if (obj.IsActive())
                obj.Refresh();

            else
            {
                objectsToRemove.Add(obj);
                PoolGeneric<EnumType, ObjectType, DataType>.Instance.Pool(obj.GetEnumType(), obj.GetObjType());
            }
        }
    }

    public void FixedRefresh()
    {
        //To Implement
    }

    public ObjectType Spawn(EnumType enumType, DataType dataType)
    {
            //Create Enemies by asking the factory
            ObjectType newObj = factory.CreateObject(enumType, dataType);
            //Initialize the enemy
            newObj.Initialize(dataType, enumType);
            newObj.transform.SetParent(objParent);
            collection.Add(newObj);

        return newObj;
    }

}
