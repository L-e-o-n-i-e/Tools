using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType>
{
    HashSet<ObjectType> collection;

    public void Initialize()
    {

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


}
