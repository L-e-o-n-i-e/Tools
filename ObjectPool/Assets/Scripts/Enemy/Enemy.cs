using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class Enemy : MonoBehaviour
{
    private GameObject prefab;
    private Sprite sprite;

    void Start()
    {
        sprite = GetRandomEnemySprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Sprite GetRandomEnemySprite()
    {
        int index = -1;
        string path = "Sprites/Enemies";
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        if (sprites.Length > 0)
            index = Random.Range(0, sprites.Length - 1);
        else
            throw new System.Exception("The sprites did not load.");
        return sprites[index];
    }
}

[System.Serializable]
public class EnemyData
{
    public float[] velocity, position, rotation, angularVelocity;
    public EnemyData(Vector3 velocity, Vector3 position, Quaternion q, Vector3 angularVelocity)
    {

        this.velocity = velocity.ToFloatArr();
        this.position = position.ToFloatArr();
        this.rotation = q.ToArrOf4();
        this.angularVelocity = angularVelocity.ToFloatArr();
    }
}
