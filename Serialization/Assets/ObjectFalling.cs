using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;

public class ObjectFalling : MonoBehaviour
{
    private GameObjectData stats;
    private Rigidbody rb;
    public int maxSpeed = 3;

    public GameObjectData Stats { get => stats; }
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }
    public void Instantiate(PrimitiveType shape, Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        stats = new GameObjectData(shape, position, rotation, velocity);
    }

    void Update()
    {
        ExtensionFuncs.ClampSpeed(rb, maxSpeed);
        Quaternion q = rb.rotation;
        if (Stats != null)
            stats.Refresh(transform.position, q, rb.velocity);
    }

}

[System.Serializable]
public class GameObjectData
{
    public PrimitiveType shape;
    public float[] position;
    public float[] rotation;
    public float[] velocity;

    public GameObjectData(PrimitiveType shape, Vector3 position, Quaternion q, Vector3 velocity)
    {
        this.shape = shape;
        this.position = position.ToFloatArr();
        this.rotation = new Vector3(q.x, q.y, q.z).ToFloatArr(); ;
        this.velocity = velocity.ToFloatArr();
    }

    public void Refresh(Vector3 position, Quaternion q, Vector3 velocity)
    {
        this.position = position.ToFloatArr();
        this.rotation = new Vector3(q.x, q.y, q.z).ToFloatArr(); ;
        this.velocity = velocity.ToFloatArr();
    }
}

