using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainScript : MonoBehaviour
{
    public int nbGameObject = 8;
    public Transform parent;
    public Transform plane;
    private float height = 100;
    private string fileName = "defaultFile.txt";
    GameObjectData goData;
    GameObjectData[] gameObjects;

    private void Awake()
    {
        SpawnPrimitives(nbGameObject);
    }

    private void SpawnPrimitives(int nbGameObject)
    {
        GameObjectData[] gameObjects = new GameObjectData[nbGameObject];

        for (int i = 0; i < nbGameObject; i++)
        {
            PrimitiveType type = (PrimitiveType)Random.Range(0, 5);                                             //Randomly chose a primitive, except the plane
            GameObject go = GameObject.CreatePrimitive(type == (PrimitiveType)4 ? (PrimitiveType)3 : type);
            go.transform.SetParent(parent);

            go.AddComponent<Rigidbody>();                                                                       //Add Rigidbody so it can use physics
            Rigidbody rb = go.transform.GetComponent<Rigidbody>();
            go.transform.position = new Vector3(Random.Range(-plane.localScale.x, plane.localScale.x), height, Random.Range(-plane.localScale.z, plane.localScale.z));  //set a random position
            Vector3 rotation = new Vector3(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z);

            gameObjects[i] = new GameObjectData(type, go.transform.position, rotation, rb.velocity);      //Save the data of this gameObject into an array of GameObjectData

        }
    }

    public string ToSerialize()
    {
        return JsonUtility.ToJson(goData);
    }

    public void Load(string jsonData)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
       
           

        goData = JsonUtility.FromJson<GameObjectData>(jsonData);
    }

    public void WriteIntoFile(string content)
    {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "GameData/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllText(filePath, content);
    }

    public void NewFileName(string newFileName)
    {
        this.fileName = newFileName;
    }

    [System.Serializable]
    public class GameObjectData
    {
        PrimitiveType shape;
        Vector3 position;
        Vector3 rotation;
        Vector3 velocity;

        public GameObjectData(PrimitiveType shape, Vector3 position, Vector3 rotation, Vector3 velocity)
        {
            this.shape = shape;
            this.position = position;
            this.rotation = rotation;
            this.velocity = velocity;
        }
    }
}
