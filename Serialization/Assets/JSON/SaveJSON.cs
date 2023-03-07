using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionFunctions;
using System.IO;

public class SaveJSON : MonoBehaviour
{
    private Rigidbody rb;
    private Stats stats;
    private PrimitiveType primType;
    

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();      
        
        
        //Now we will read from the file, and convert back into the original class
        //First we need the text data 
        //string jsonDeserialized = File.ReadAllText(filePath);

        ////Then we deserialize
        //Stats newClassLoadedFromJson = JsonUtility.FromJson<Stats>(jsonDeserialized);

       

    }
    private void Start()
    {
        stats = new Stats(rb.velocity, rb.position, rb.rotation, primType, rb.angularVelocity);
    }

    private void Update()
    {
        ExtensionFunctions.ExtensionFuncs.ClampSpeed(rb, 3);
    }

    public Stats GetStats()
    {
        UpdateStats();
        return stats;
    }

    private void UpdateStats()
    {
        this.stats.primType = this.primType;
        this.stats.position = rb.position.ToFloatArr();
        Quaternion r = rb.rotation;
        this.stats.rotation = r.ToArrOf4();
        this.stats.angularVelocity = rb.angularVelocity.ToFloatArr();
        this.stats.velocity = rb.velocity.ToFloatArr();
    }

    public void SetPrimitiveType(PrimitiveType primType)
    {
        this.primType = primType;
    }

    public PrimitiveType GetPrimitiveType( )
    {
        return primType;
    }

}

[System.Serializable]
public class Stats
{
    public float[] velocity, position, rotation, angularVelocity;
    [SerializeField] public PrimitiveType primType;
    public Stats(Vector3 velocity, Vector3 position, Quaternion q, PrimitiveType primType, Vector3 angularVelocity)
    {

        this.velocity = velocity.ToFloatArr();
        this.position = position.ToFloatArr();
        this.rotation = q.ToArrOf4();
        this.angularVelocity = angularVelocity.ToFloatArr();
        this.primType = primType;
    }
}
