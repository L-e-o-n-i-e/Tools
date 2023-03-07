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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class ObjStats
{

}
