using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //Required for MenuItem, means that this is an Editor script, must be placed in an Editor folder, and cannot be compiled!
using System.Linq;  //Used for Select

public class CreateCardsWindow : EditorWindow
{
    [MenuItem("Custom Tools/ Create Card")] //This the function below it as a menu item, which appears in the tool bar
    public static void CreateShowcase() //Menu items can call STATIC functions, does not work for non-static since Editor scripts cannot be attached to objects
    {
        EditorWindow window = GetWindow<CreateCardsWindow>("Create Card");
        window.minSize = new Vector2(700, 600);
    }

    CardAssetInfo[] selectedCards;
    Editor selectedCardEditor;
    int index;
    bool initialized = false;

    void OnGUI() //Called every frame in Editor window
    {
        if (!initialized)
        {
            selectedCards = Resources.LoadAll<CardAssetInfo>("CardsTemplate/");
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
            //Pops up the file explorer, and give you the power to chose a scriptable object, AND displays it.            
            //string path = AssetDatabase.GetAssetPath(selectedCards[index]);
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
            Selection.activeObject = selectedCards[index];            
            EditorUtility.FocusProjectWindow();
        }
    }

    void DisplayCreateCardButton()
    {
        if (GUILayout.Button("Create New Card"))
        {

            Debug.Log("CreateNewCard button clicked");
        }
    }

    void DisplayCard()
    {
        GUILayout.BeginVertical();
        DisplayEmptyRect(200, 50);
        GUILayout.Label("Banane", EditorStyles.largeLabel);
        selectedCardEditor.OnInspectorGUI();


        GUILayout.EndVertical();
    }

    //GAUCHE : haut en bas
    //File Name : displaying name of the card
    //Button : Previous card
    int FindIndexOf(CardAssetInfo cardToFind)
    {
        Debug.Log(cardToFind.cardName);
        int index = -1;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            Debug.Log(selectedCards[i].cardName);

            if (cardToFind.cardName.Equals(selectedCards[i].cardName))
                index = i;

        }
        Debug.Log("returning index : " + index);
        return index;
    }

}

