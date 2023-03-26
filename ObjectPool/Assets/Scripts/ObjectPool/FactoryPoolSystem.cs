using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPoolSystem<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType, ObjectType, EnumType>
{
    PoolGeneric<EnumType, ObjectType, DataType> pool;    
    Factory<EnumType, ObjectType, DataType> factory;    
    Manager<EnumType, ObjectType, DataType> manager;

    public void Initialize()
    {
        pool = new PoolGeneric<EnumType, ObjectType, DataType>();
        factory = new Factory<EnumType, ObjectType, DataType>();
        manager = new Manager<EnumType, ObjectType, DataType>();
        pool.Instantiate();
        factory.Instantiate(pool);
        manager.Initialize(factory, pool);
    }

    public void Refresh()
    {
        manager.Refresh();
    }

    public void FixedRefresh()
    {
        manager.FixedRefresh();
    }

    public ObjectType Create(EnumType enumType, DataType dataType)
    {
        return manager.Spawn(enumType, dataType);
    }
}