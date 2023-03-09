using UnityEngine;
using UnityEditor;

public static class SwitchToNewScripts
{
    [MenuItem("Prefabs/Switch to new scripts")]
    public static void DoLogic()
    {
        // Get all prefabs from folder
        string path = "Assets/Prefabs";
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });

        if (guids.Length > 0)
        {
            for (int i = 0; i < guids.Length; i++)
            {
                //Load the gameObject
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);

                //If it has an old script
                if (contentsRoot.GetComponent<Old_AIBase>())
                {
                    // Modify Prefab contents
                    AIBase aiBase = contentsRoot.AddComponent<AIBase>();

                    LinkWithOtherComponents(aiBase, contentsRoot.GetComponent<Rigidbody2D>(), contentsRoot.GetComponent<SpriteRenderer>(), contentsRoot.GetComponent<BoxCollider2D>(), contentsRoot.GetComponent<Old_AIBase>());

                    RemoveOldComponent(contentsRoot, contentsRoot.GetComponent<Old_AIBase>());

                    // Save contents back to Prefab Asset and unload contents.
                    PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
                    PrefabUtility.UnloadPrefabContents(contentsRoot);
                }
            }
        }
        else
            throw new System.Exception("No prefabs were found to process to a modification of the components in SwitchToNewScript / DoLogic");
    }

    public static void RemoveOldComponent(GameObject contentsRoot, Old_AIBase oldAI)
    {
        Old_AIBase oldAI_component = contentsRoot.GetComponent<Old_AIBase>();
        if (oldAI_component != null)
            Object.DestroyImmediate(oldAI_component);
    }

    public static void LinkWithOtherComponents(AIBase component, Rigidbody2D rb, SpriteRenderer sr, BoxCollider2D bc, Old_AIBase oldAI)
    {
        if (rb && sr && bc)
        {
            component.rb = rb;
            component.sr = sr;
            component.coli = bc;
            component.enemyType = oldAI.enemyType;
            component.aiStats = oldAI.aiStats;
        }
        else
            throw new System.Exception("Function LinkWithOtherComponents : one of the components is null in Rigidbody2D | SpriteRenderer | BoxCollider2D | Old_AIBase");
    }
}