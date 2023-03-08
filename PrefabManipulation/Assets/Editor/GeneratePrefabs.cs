using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public static class GeneratePrefabs
{
    [MenuItem("Prefabs/Make Prefabs")]
    public static void Generate()
    {
        //TODO : make a list of strings to mix and match


        GameObject go = new GameObject("objectName");

        AddBasicComponents(go);

        AddAIComponent(go);

        SelectaRandomEnumValue(go.GetComponent<Old_AIBase>());

        LinkWithOtherComponents(go.GetComponent<Old_AIBase>(), go.GetComponent<Rigidbody2D>(), go.GetComponent<SpriteRenderer>(), go.GetComponent<BoxCollider2D>());

        //Add a scriptable Assets (randomly)
        //Give it a name : 
        //by having a collection of words and mixing two words together
        //Save the prefab to the project and refresh the asset database!!!!!!!!!!!!



        //if (!Directory.Exists("Assets/Prefabs"))
        //    AssetDatabase.CreateFolder("Assets", "Prefabs");
        //string localPath = "Assets/Prefabs/" + go.name + ".prefab";

        //Resources.Load<AIStats>() (precise that I want to LOAD a scriptable object!
        //Resources.LoadAll<AIStats>()
    }

    private static void AddBasicComponents(GameObject go)
    {
        go.AddComponent<BoxCollider2D>();
        go.AddComponent<Rigidbody2D>();
        go.AddComponent<SpriteRenderer>();
    }

    private static string AddAIComponent(GameObject go)
    {
        string fullPath = "Assets/Scripts/Old_Scripts/Old_AIBase_Children";


        string[] fileEntries = Directory.GetFiles(fullPath, "*.cs");
        for (int i = 0; i < fileEntries.Length; i++)
        {
            fileEntries[i] = Path.GetFileNameWithoutExtension(fileEntries[i]);
            Debug.Log(fileEntries[i]);
        }

        int index = UnityEngine.Random.Range(0, fileEntries.Length - 1);
        string typeOfClass = fileEntries[index].ToString() + ", Assembly-CSharp";
        System.Type t = System.Type.GetType(typeOfClass);
        go.AddComponent(t);

        return typeOfClass;
    }

    public static void SelectaRandomEnumValue(Old_AIBase component)
    {
        int range = System.Enum.GetValues(typeof(EnumReferences.EnemyType)).Length;
        component.enemyType = (EnumReferences.EnemyType) UnityEngine.Random.Range(0, range);           
    }

    public static void LinkWithOtherComponents(Old_AIBase component, Rigidbody2D rb, SpriteRenderer sr, BoxCollider2D bc)
    {
        component.rb = rb;
        component.sr = sr;
        component.coli = bc;
    }
}
