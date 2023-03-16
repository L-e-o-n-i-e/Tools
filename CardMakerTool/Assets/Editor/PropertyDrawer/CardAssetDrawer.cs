using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Attribute Inspector
[CustomEditor(typeof(CardAssetInfo))]
public class CardAssetDrawer : Editor
{
    private SerializedObject cardAsset;  

    private SerializedProperty cardName;
    private SerializedProperty mana;
    private SerializedProperty color;
    private SerializedProperty sprite;
    private SerializedProperty description;
    private SerializedProperty power;

    public void OnEnable()
    {
        cardAsset = new SerializedObject(target);          //target is MovementController
        cardName = cardAsset.FindProperty("cardName");   //Use reflection to find a variable (property) in the target project
        mana = cardAsset.FindProperty("mana");
        color = cardAsset.FindProperty("color");
        sprite = cardAsset.FindProperty("sprite");
        Sprite spriteText = (Sprite)sprite.objectReferenceValue;
        if(spriteText != null)
        {
            Texture2D texture = new Texture2D((int)spriteText.rect.width, (int)spriteText.rect.height, TextureFormat.RGBA32, false);
            texture.SetPixels(spriteText.texture.GetPixels((int)spriteText.rect.x, (int)spriteText.rect.y, (int)spriteText.rect.width, (int)spriteText.rect.height));
            texture.Apply();
        }
        description = cardAsset.FindProperty("description");
        power = cardAsset.FindProperty("power"); 
    }

    //On inspector
    public override void OnInspectorGUI()
    {
        Debug.Log("Override Inspector Card Asset");


        GUILayout.Label("Card Assets", EditorStyles.boldLabel);
        
        EditorGUILayout.PropertyField(cardName);                        
        EditorGUILayout.PropertyField(mana);                        
        EditorGUILayout.PropertyField(color);                        
        EditorGUILayout.PropertyField(sprite);                        
        EditorGUILayout.PropertyField(description);                         
        EditorGUILayout.PropertyField(power);

        
    }

}
