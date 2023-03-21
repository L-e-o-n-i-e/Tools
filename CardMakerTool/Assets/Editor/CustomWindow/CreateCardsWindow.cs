using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class CreateCardsWindow : EditorWindow
{
    [MenuItem("Custom Tools/ Create Card")]
    public static void CreateShowcase()
    {
        EditorWindow window = GetWindow<CreateCardsWindow>("Create Card");
        window.minSize = new Vector2(700, 600);
    }

    CardAssetInfo[] selectedCards;
    Editor selectedCardEditor;
    int index;
    bool initialized = false;

    void OnGUI()
    {
        if (!initialized)
        {
            //selectedCards = Resources.LoadAll<CardAssetInfo>("CardsTemplate/");
            ResetCardsList();
            index = 0;
            selectedCardEditor = Editor.CreateEditor(selectedCards[index]);
            initialized = true;
        }

        GUILayout.BeginHorizontal();
        DisplayLeftRect();
        DisplayMiddleRect();
        DisplayRightRect();
        GUILayout.EndHorizontal();
    }

    void DisplayLeftRect()
    {
        GUILayout.BeginVertical();
        DisplayEmptyRect(100, 150);
        DisplayPreviousButton();
        GUILayout.EndVertical();
    }

    void DisplayMiddleRect()
    {
        GUILayout.BeginVertical();
        DisplayCard();
        DisplayCreateCardButton();
        GUILayout.EndVertical();

    }

    void DisplayRightRect()
    {
        GUILayout.BeginVertical();
        DisplayLoadButton();
        DisplayGoToFileButton();
        DisplayEmptyRect(100, 150);
        DisplayNextButton();
        GUILayout.EndVertical();
    }

    void DisplayEmptyRect(int width, int height)
    {
        GUILayoutUtility.GetRect(width, height);
    }
    void DisplayPreviousButton()
    {
        if (GUILayout.Button("Previous"))
        {
            index = index == 0 ? selectedCards.Length - 1 : index - 1;
            selectedCardEditor = Editor.CreateEditor(selectedCards[index]);
            Debug.Log("Previous button clicked");
        }
    }

    void DisplayNextButton()
    {
        if (GUILayout.Button("Next"))
        {
            index = (index + 1) % selectedCards.Length;
            selectedCardEditor = Editor.CreateEditor(selectedCards[index]);
            Debug.Log("Next button clicked");
        }
    }

    void DisplayLoadButton()
    {
        if (GUILayout.Button("Load"))
        {
            string path = Application.dataPath + "/Resources/CardsTemplate/";

            string absPath = EditorUtility.OpenFilePanel("Select Scriptable Object", path, "asset");
            Debug.Log("Returning AbsPath" + absPath);
            if (absPath != null)
            {
                string scriptObjName = System.IO.Path.GetFileNameWithoutExtension(absPath);
                CardAssetInfo newScriptObj = Resources.Load<CardAssetInfo>("CardsTemplate/" + scriptObjName);
                index = FindIndexOf(newScriptObj);
                selectedCardEditor = Editor.CreateEditor(selectedCards[index]);
            }
        }
    }

    void DisplayGoToFileButton()
    {
        if (GUILayout.Button("Go To File"))
        {
            CardAssetInfo[] originals = Resources.LoadAll<CardAssetInfo>("CardsTemplate/");
           
            Selection.activeObject = originals[index];
            EditorUtility.FocusProjectWindow();
        }
    }

    void DisplayCreateCardButton()
    {
        if (GUILayout.Button("Create New Card"))
        {
            Debug.Log("CreateNewCard button clicked");
            CardAssetInfo scriptableObject = ScriptableObject.CreateInstance<CardAssetInfo>();

            // Set any properties you want to save
            scriptableObject.cardName = selectedCards[index].cardName;
            scriptableObject.mana = selectedCards[index].mana;
            scriptableObject.color = selectedCards[index].color;
            scriptableObject.sprite = selectedCards[index].sprite;
            scriptableObject.description = selectedCards[index].description;
            scriptableObject.power = selectedCards[index].power;

            // Create a new asset at the specified path
            string assetPath = "Assets/Resources/CardsTemplate/" + selectedCards[index].cardName + ".asset";
            AssetDatabase.CreateAsset(scriptableObject, assetPath);
            AssetDatabase.SaveAssets();

            // Focus the Project window on the new asset
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = scriptableObject;

            //Reset the list of Cards
            //selectedCards = Resources.LoadAll<CardAssetInfo>("CardsTemplate/");
            ResetCardsList();
        }
    }

    void DisplayCard()
    {
        GUILayout.BeginVertical();
        DisplayEmptyRect(200, 50);

        selectedCardEditor.OnInspectorGUI();

        GUILayout.EndVertical();
    }

    int FindIndexOf(CardAssetInfo cardToFind)
    {
        int index = -1;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (cardToFind.cardName.Equals(selectedCards[i].cardName))
                index = i;
        }
        Debug.Log("returning index : " + index);
        return index;
    }

    void ResetCardsList()
    {
        CardAssetInfo[] originals = Resources.LoadAll<CardAssetInfo>("CardsTemplate/");
        selectedCards = new CardAssetInfo[originals.Length];

        if (originals.Length < 0)
            throw new System.Exception("No scriptable objects in the file CardsTemplate. Nothing to display.");
        else
        {
            for (int i = 0; i < originals.Length; i++)
            {
                //Create a copy of the scriptable Object not to modify directly the original
                CardAssetInfo newSO = ScriptableObject.CreateInstance<CardAssetInfo>();

                // Set any properties you want to save
                newSO.cardName = originals[i].cardName;
                newSO.mana = originals[i].mana;
                newSO.color = originals[i].color;
                newSO.sprite = originals[i].sprite;
                newSO.description = originals[i].description;
                newSO.power = originals[i].power;

                selectedCards[i] = newSO;
            }
        }
    }
}