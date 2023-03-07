using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum BulletType { Laser, PewPews }
public class SampleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Factory<BulletType, Test, ObjStats> factory = new Factory<BulletType, Test, ObjStats>();
        factory.CreateObject(BulletType.Laser, new ObjStats() { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
