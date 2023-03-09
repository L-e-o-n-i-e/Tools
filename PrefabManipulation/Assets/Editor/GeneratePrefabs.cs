using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text.RegularExpressions;

public static class GeneratePrefabs
{

    public static int nbOfGameObjects = 20;

    [MenuItem("Prefabs/Make Prefabs")]
    public static void Generate()
    {
        GameObject parent = new GameObject("MotherOfEnemies");
        for (int i = 0; i < nbOfGameObjects; i++)
        {
            string name = GenerateRandomName();
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent.transform);

            //add rigidbody2D, spriteRenderer, BoxCollider2d
            AddBasicComponents(go);

            //add the component script AI
            AddAIComponent(go);

            //randomly select a value for the enum in AI script
            SelectaRandomEnumValue(go.GetComponent<Old_AIBase>());

            //link the refenrece to other components in the AI script component
            LinkWithOtherComponents(go.GetComponent<Old_AIBase>(), go.GetComponent<Rigidbody2D>(), go.GetComponent<SpriteRenderer>(), go.GetComponent<BoxCollider2D>());

            //link to the AIStats scriptableObject
            AddAIStatsReference(go.GetComponent<Old_AIBase>());

            //Save the prefab in the asset folder / Prefabs
            SaveInAssetsFolder(go);

            //refresh the asset database!
            AssetDatabase.Refresh();
        }
        ClearHierarchie(parent);
    }

    public static void SaveInAssetsFolder(GameObject go)
    {
        if (!Directory.Exists("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        string localPath = "Assets/Prefabs/" + go.name + ".prefab";
        bool prefabSuccess;
        PrefabUtility.SaveAsPrefabAsset(go, localPath, out prefabSuccess);
    }

    public static void ClearHierarchie(GameObject parent)
    {
        int nbChild = parent.transform.childCount;

        for (int i = parent.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(parent.transform.GetChild(i).gameObject);
        }

        GameObject.DestroyImmediate(parent.gameObject);
    }

    private static void AddBasicComponents(GameObject go)
    {
        go.AddComponent<BoxCollider2D>();
        go.AddComponent<Rigidbody2D>();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        AddRandomSpriteReference(sr);
    }

    private static string AddAIComponent(GameObject go)
    {
        string fullPath = "Assets/Scripts/Old_Scripts/Old_AIBase_Children";

        string[] fileEntries = Directory.GetFiles(fullPath, "*.cs");
        for (int i = 0; i < fileEntries.Length; i++)
        {
            fileEntries[i] = Path.GetFileNameWithoutExtension(fileEntries[i]);
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
        component.enemyType = (EnumReferences.EnemyType)UnityEngine.Random.Range(0, range);
    }

    public static void LinkWithOtherComponents(Old_AIBase component, Rigidbody2D rb, SpriteRenderer sr, BoxCollider2D bc)
    {
        component.rb = rb;
        component.sr = sr;
        component.coli = bc;
    }
    public static void AddRandomSpriteReference(SpriteRenderer sr)
    {
        string path = "EnemySprites";
        Sprite[] arr = Resources.LoadAll<Sprite>(path);
        if (arr.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, arr.Length - 1);
            sr.sprite = arr[index];
        }
        else
            throw new Exception("No sprites were found.");
    }

    public static void AddAIStatsReference(Old_AIBase component)
    {
        //Add a scriptable Assets (randomly)
        string path = "Scriptable Assets";
        AIStats[] arr = Resources.LoadAll<AIStats>(path);

        int index = UnityEngine.Random.Range(0, arr.Length - 1);     
        component.aiStats = arr[index];
    }
    public static string GenerateRandomName()
    {
        string variableName = "";
        string folderPath = "Assets/Resources/Prefabs";

        do
        {
            //Randomly make a new name starting with "gameObject_" and adding other alphanum to it.
            variableName = "gameObject_" + Guid.NewGuid().ToString().Replace("-", "");
            //removing all the numbers
            variableName = Regex.Replace(variableName, @"[\d-]", string.Empty);
            //making sure it is not already the name of an abject in the file
        } while (AssetDatabase.FindAssets(Regex.Replace(variableName, @"[\d-]", string.Empty), new[] { folderPath }).Length > 0);

        return variableName;
    }
}
