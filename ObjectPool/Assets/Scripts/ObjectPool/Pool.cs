using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<EnumType, ObjectType, DataType> where ObjectType : MonoBehaviour, IManagable<DataType, ObjectType, EnumType>
{
  
}
