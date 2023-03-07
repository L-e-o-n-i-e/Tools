using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainScript : MonoBehaviour
{
    public int nbGameObject = 8;
    public Transform parent;
    public Transform plane;
    public RectTransform inputFileName;
    private float height = 100;
    private string fileName = "TestFile";
    GameObjectData goData;
    ObjectFalling[] gameObjects;

    private void Awake()
    {
        SpawnPrimitives(nbGameObject);
    }

    private void SpawnPrimitives(int nbGameObject)
    {
        ObjectFalling[] gameObjects = new ObjectFalling[nbGameObject];

        for (int i = 0; i < nbGameObject; i++)
        {
            PrimitiveType type = (PrimitiveType)Random.Range(0, 5);                                             //Randomly chose a primitive, except the plane
            GameObject go = GameObject.CreatePrimitive(type == (PrimitiveType)4 ? (PrimitiveType)3 : type);
            go.transform.SetParent(parent);

            Rigidbody rb = go.AddComponent<Rigidbody>();
            go.transform.position = new Vector3(Random.Range(-plane.localScale.x, plane.localScale.x), height, Random.Range(-plane.localScale.z, plane.localScale.z));  //set a random position
            ObjectFalling objFall = go.AddComponent<ObjectFalling>();
            objFall.Instantiate(type, go.transform.position, rb.rotation, rb.velocity);


            gameObjects[i] = objFall;      //Save the data of this gameObject into an array of GameObjectData            
        }
    }

    public string ToSerialize()
    {
        string content = "";
        foreach(ObjectFalling obj in gameObjects)
        {

            content += JsonUtility.ToJson(obj.Stats);
        }
        return content;
    }

    public void Load(string filePath)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
        //For the nb of objects in the scene, 
        //We loop over and create a primitive and give it data
        string jsonDeserialized = File.ReadAllText(filePath);

        goData = JsonUtility.FromJson<GameObjectData>(jsonDeserialized);
    }

    public void WriteIntoFile(string content)
    {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "GameData/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllText(filePath, content);
    }
    public void Save()
    {
        //NewFileName();
        WriteIntoFile(ToSerialize());
    }

    public void NewFileName()
    {        
        this.fileName = inputFileName.GetComponent<TextMeshPro>().text ;
    }

   
}
