using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class Test : MonoBehaviour, IManagable<ObjStats, Test, EnemyType>
{
    ObjStats stats;
    private EnemyType enemyType;
    bool isActive;
    

    public void Delete()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void FixedRefresh()
    {
        //TODO

    }

    public Test GetObjType()
    {
        return this;
    }

    public EnemyType GetEnumType()
    {
        return this.enemyType;
    }
     
    

    public void Initialize(ObjStats stats, EnemyType enumType)
    {
        this.stats = stats;
        isActive = true;
        gameObject.SetActive(true);
        this.enemyType = enumType;
        this.transform.SetParent(this.stats.parent);
        transform.position = this.stats.position.ToVector3();
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void Refresh()
    {
        //TODO
        this.stats.position = transform.position.ToFloatArr();

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.CompareTo("Player") == 0)
        {
            Delete();
            Debug.Log("Enemy " + transform.name + " was hit by player");
        }
    }

    public ObjStats GetStats()
    {
        return this.stats;
    }
}

public class ObjStats
{
    int hp;
    public Transform parent;
    public float[] position;
    

    public ObjStats(int hp, Transform parent, float[] position)
    {
        this.hp = hp;
        this.parent = parent;
        this.position = position;
    }
}
