using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ExtensionFunctions;
using UnityEngine.UI;
using TMPro;

public class LoadJSON : MonoBehaviour
{
    public Transform plane;
    public RectTransform inputFileName;
    public int nbObjects = 3;
    private List<SaveJSON> goToSave;
    private string filePath = "";
    private string fileName = "SaveJSON.txt";
    private float height = 30;

    private void Awake()
    {
        goToSave = new List<SaveJSON>();

        for (int i = 0; i < nbObjects; i++)
        {
            PrimitiveType type;
            do
            {
                type = (PrimitiveType)Random.Range(0, 5);
            } while (type == (PrimitiveType)4);

            //Randomly chose a primitive, except the plane
            GameObject go = GameObject.CreatePrimitive(type);

            go.transform.SetParent(this.transform);
            go.AddComponent<Rigidbody>();
            go.transform.position = new Vector3(Random.Range(-plane.localScale.x, plane.localScale.x), height, Random.Range(-plane.localScale.z, plane.localScale.z));
            SaveJSON objJSON = go.AddComponent<SaveJSON>();
            objJSON.SetPrimitiveType(type);
            goToSave.Add(go.GetComponent<SaveJSON>());
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Load();
        }
    }

    public void Load()
    {
        Debug.Log("Load JSON");
        ClearScene();

        string jsonDeserialized = File.ReadAllText(filePath);

        WrapperOfObjects<Stats> newClassLoadedFromJson = JsonUtility.FromJson<WrapperOfObjects<Stats>>(jsonDeserialized);

        List<Stats> statsList = newClassLoadedFromJson.GetList();
        for (int i = 0; i < statsList.Count; i++)
        {
            GameObject go = GameObject.CreatePrimitive(statsList[i].primType);
            go.transform.SetParent(this.transform);
            go.transform.position = (statsList[i].position).ToVector3();
            Rigidbody rb = go.AddComponent<Rigidbody>();
            SaveJSON objSave = go.AddComponent<SaveJSON>();
            objSave.SetPrimitiveType(statsList[i].primType);
            Quaternion q = (statsList[i].rotation).ToQuaternion();
            go.transform.rotation = new Quaternion(q.x, q.y, q.z, q.w);
            rb.angularVelocity = statsList[i].angularVelocity.ToVector3();
            rb.velocity = statsList[i].velocity.ToVector3();
            goToSave.Add(objSave);
        }
    }


    public void Save()
    {
        Debug.Log("Save JSON");
        List<Stats> goStats = new List<Stats>();

        foreach (SaveJSON gameObject in goToSave)
        {
            goStats.Add(gameObject.GetStats());
        }

        WrapperOfObjects<Stats> objectsToSave = new WrapperOfObjects<Stats>(goStats);

        string content = JsonUtility.ToJson(objectsToSave);

        //Then we can write it to a file
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "GameData/");  //Path.combine is like just adding them +, but takes care of merging / for you (DO NOT PUT / in fron of JsonExamples)
        if (!Directory.Exists(directoryPath))                                                   //Create path if doesnt exist or error
            Directory.CreateDirectory(directoryPath);
        
        filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllText(filePath, content);
    }

    private void ClearScene()
    {
        for (int i = goToSave.Count - 1; i >= 0; i--)
        {
            SaveJSON objToDel = goToSave[i];
            goToSave.Remove(goToSave[i]);
            GameObject.Destroy(objToDel.gameObject);
        }
    }
    
    public void ReadInput(string s)
    {
        this.fileName = s + ".txt";
        Debug.Log("New file name : " + fileName);
    }    
}


[System.Serializable]
public class WrapperOfObjects<T>
{
    public int a = 5;
    [SerializeField] public List<T> objects;

    public WrapperOfObjects(List<T> objects)
    {
        this.objects = objects;
    }

    public List<T> GetList()
    {
        return objects;
    }
}

[System.Serializable]
public class HardcodedWrapperOfObjects
{
    public int a = 5;
    [SerializeField] public List<Stats> objects;

    public HardcodedWrapperOfObjects(List<Stats> objects)
    {
        this.objects = objects;
    }
}
