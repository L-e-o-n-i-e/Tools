using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoadPrefabs : MonoBehaviour
{
    [Range(0, 10)] public int nbOfEnemies = 10;
    void Start()
    {
        string path = "Assets/Prefabs";
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
        if (guids.Length < nbOfEnemies)
            throw new System.Exception("The number of enemies is larger than the prefabs we have. Problem with logic in LoadPrefabs / Start");

        if (guids.Length > 0)
        {
            int count = 0;
            Queue<string> guidsToUse = new Queue<string>();
            do
            {
                int ranNum = Random.Range(0, guids.Length - 1);
                if (guids[ranNum] != null)
                {
                    guidsToUse.Enqueue(guids[ranNum]);
                    guids[ranNum] = null;
                    count++;
                }
            } while (count != nbOfEnemies);

            GameObject mother = new GameObject("MotherOfEnemies");

            for (int i = 0; i < count; i++)
            {
                //Load the gameObject
                string assetPath = AssetDatabase.GUIDToAssetPath(guidsToUse.Dequeue());
                GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);
                contentsRoot.transform.position = new Vector2(i, i);
                contentsRoot.transform.SetParent(mother.transform);

            }
        }
        else
        {
            throw new System.Exception("No prefabs were found in the folder Asset / Prefabs");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
