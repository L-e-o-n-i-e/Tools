using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Factory<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType>
{
    Dictionary<EnumType, ObjectType> ressourcesDict;

    public void Instantiate()
    {
        Debug.Log("Instantiate Factory");
        //Initialize the resources dictionary
        ressourcesDict = new Dictionary<EnumType, ObjectType>();
        //Declare all different types of object in the dictionary
        List<EnumType> enums = System.Enum.GetValues(typeof(EnumType)).Cast<EnumType>().ToList();

        foreach (EnumType type in enums)
        {
            ObjectType obj =  Resources.Load<ObjectType>("Prefabs/Enemy/" + type.ToString());
            ressourcesDict.Add(type, obj);
            foreach (var item in ressourcesDict)
            {
                Debug.Log("Dict key : " + item.Key + " value : " + item.Value);
            }
        }
    }

    public ObjectType CreateObject(EnumType type, DataType stats)
    {
        //Ask Pool if there is an object in the pool
        ObjectType obj = PoolGeneric<EnumType, ObjectType, DataType>.Instance.DePool(type);
        if(obj == null)
        {
            //If not, create one
           obj = GameObject.Instantiate<ObjectType>(ressourcesDict[type]);    
        }

        //Initialize it
        obj.Initialize(stats);

        return obj;
    }
}
