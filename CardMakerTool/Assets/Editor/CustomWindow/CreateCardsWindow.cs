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
        Debug.Log("Create window custom");
        EditorWindow window = GetWindow<CreateCardsWindow>("Create Card");
        window.minSize = new Vector2(700, 600);
    }

    CardAssetInfo[] selectedCards;
    Editor selectedCardEditor;
    int index = 0;


    void OnGUI() //Called every frame in Editor window
    {
        selectedCards = Resources.LoadAll<CardAssetInfo>("CardsTemplate/" + "Ella");
        selectedCardEditor = Editor.CreateEditor(selectedCards[index]);

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
            Debug.Log(selectedCards[index].cardName);
        }
    }

    void DisplayNextButton()
    {
        if (GUILayout.Button("Next"))
        {
            index = index + 1 % selectedCards.Length;
            selectedCardEditor = Editor.CreateEditor(selectedCards[index]);
            Debug.Log("Next button clicked");
            Debug.Log(selectedCards[index].cardName);
        }
    }

    void DisplayLoadButton()
    {
        if (GUILayout.Button("Load"))
        {

            Debug.Log("Load button clicked");
        }
    }

    void DisplayGoToFileButton()
    {
        if (GUILayout.Button("Go To File"))
        {

            Debug.Log("GoToFile button clicked");
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
    //Diviser en 3 grands rectangles : Gauche, milieu, Droite

    //GAUCHE : haut en bas
    //File Name : displaying name of the card
    //Button : Previous card


    //DROITE : 
    //Button : Load
    //Button : Go to file 
    //Button : Next Card

}

