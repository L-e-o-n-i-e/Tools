using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagable<DataType>
{
    void Initialize(DataType stats);
    void Refresh();
    void FixedRefresh();
    void Delete();

}
