using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagable<DataType, ObjectType, EnumType>
{
    void Initialize(DataType stats, EnumType enumType);
    void Refresh();
    void FixedRefresh();  
    bool IsActive();
    EnumType GetEnumType();
    ObjectType GetObjType();
    DataType GetStats();
    void Pool();
    void DePool(DataType dataType);
}