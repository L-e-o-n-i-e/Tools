using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IManagable<ObjStats>
{
    ObjStats stats;

    public void Delete()
    {
      //TODO
    }

    public void FixedRefresh()
    {
        //TODO

    }

    public void Initialize(ObjStats stats)
    {
        stats = new ObjStats(3);
    }

    public void Refresh()
    {
        //TODO
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
