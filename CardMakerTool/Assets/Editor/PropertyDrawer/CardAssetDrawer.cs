using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardAssetInfo))]
public class CardAssetDrawer : Editor
{
    private SerializedObject cardAsset;

    private SerializedProperty cardName;
    private SerializedProperty mana;
    private SerializedProperty color;
    private SerializedProperty spriteProperty;
    private SerializedProperty description;
    private SerializedProperty power;

    Rect texturePos;

    public void OnEnable()
    {
        cardAsset = new SerializedObject(target);          //target is MovementController
        color = cardAsset.FindProperty("color");
        cardName = cardAsset.FindProperty("cardName");   //Use reflection to find a variable (property) in the target project
        mana = cardAsset.FindProperty("mana");
        spriteProperty = cardAsset.FindProperty("sprite");

        description = cardAsset.FindProperty("description");
        power = cardAsset.FindProperty("power");
    }
    
    public override void OnInspectorGUI()
    {
        Debug.Log("Override Inspector Card Asset");
        cardAsset.Update();
        
        GUILayout.Label("Card Assets", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(mana);
        EditorGUILayout.PropertyField(color);
        
        Sprite sprite = (Sprite)spriteProperty.objectReferenceValue;
        if (sprite != null)
        {
            GUILayout.Label(sprite.texture, GUILayout.MaxHeight(300f));
            Rect previewRect = GUILayoutUtility.GetLastRect();

            Event e = Event.current;
            if (e.type == EventType.MouseDown && previewRect.Contains(e.mousePosition))
            {
                string path = AssetDatabase.GetAssetPath(sprite);
                string absPath = EditorUtility.OpenFilePanel("Select Asset", path, "png,jpg,jpeg");
                if (absPath.StartsWith(Application.dataPath))
                {
                    //TODO
                }
            }
        }


        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(power);
        EditorGUILayout.EndVertical();

        cardAsset.ApplyModifiedProperties();

    }

}
