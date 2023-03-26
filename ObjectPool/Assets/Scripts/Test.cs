using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class Test : MonoBehaviour, IManagable<ObjStats, Test, EnemyType>
{
    ObjStats stats;
    private EnemyType enemyType;
    bool isActive;



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
            Pool();
        }
    }

    public ObjStats GetStats()
    {
        return this.stats;
    }

    public void Pool()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    public void DePool(ObjStats type)
    {
        this.stats = type;
        this.stats.position = ExtensionFuncs.RandomStartPosition(GameManager.Instance.worldBounds).ToFloatArr();
        transform.position = this.stats.position.ToVector3();
    }
}

public class ObjStats
{
    int hp;
    public float[] position;


    public ObjStats(int hp, float[] position)
    {
        this.hp = hp;
        this.position = position;
    }
}