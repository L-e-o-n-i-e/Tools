using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGeneric<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType, ObjectType, EnumType>
{
    Dictionary<EnumType, Queue<ObjectType>> objDict;

    #region Singleton
    private static PoolGeneric<EnumType, ObjectType, DataType> instance = null;
    private PoolGeneric() { }
    public static PoolGeneric<EnumType, ObjectType, DataType> Instance
    {
        get
        {
            if (PoolGeneric<EnumType, ObjectType, DataType>.instance == null) PoolGeneric<EnumType, ObjectType, DataType>.instance = new PoolGeneric<EnumType, ObjectType, DataType>();
            return PoolGeneric<EnumType, ObjectType, DataType>.instance;
        }
    }
    #endregion

    public void Instantiate()
    {
        objDict = new Dictionary<EnumType, Queue<ObjectType>>();
    }

    public void Pool(EnumType type, ObjectType obj)
    {
        Debug.Log($"Taking object of type : {type} to the pool.");
        if (!objDict.ContainsKey(type))
        {
            objDict.Add(type, new Queue<ObjectType>());
        }
        objDict[type].Enqueue(obj);

    }

    public ObjectType DePool(EnumType type)
    {
        return (objDict.Count > 0) ? objDict[type].Dequeue() : null;
    }
}
