using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IManagable<ObjStats>
{
    ObjStats stats;

    public void Delete()
    {
        throw new System.NotImplementedException();
    }

    public void FixedRefresh()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(ObjStats stats)
    {
        throw new System.NotImplementedException();
    }

    public void Refresh()
    {
        throw new System.NotImplementedException();
    }
    
}

public class ObjStats
{
    int hp;

    public ObjStats(int hp)
    {
        this.hp = hp;
    }
}
