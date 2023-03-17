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

    CardAssetInfo cardAssets; //turn this into a list later


    void OnGUI() //Called every frame in Editor window
    {
        GUILayout.BeginHorizontal();        //Have each element below be side by side
        DisplayEmptyRect();
        DisplayCard();
        DisplayEmptyRect();
        GUILayout.EndHorizontal();
    }
    

    void DisplayEmptyRect()
    {
        GUILayoutUtility.GetRect(100, 600);
    }

    void DisplayCard()
    {
        //MILIEU :
        //Rectagle : Display the card
        //Button : Create New Card 
        GUILayout.BeginVertical();                                                      //Start vertical section, all GUI draw code after this will belong to same vertical
        GUILayout.Label("Create a new card", EditorStyles.largeLabel);                            //A label that says "Toolbar"

        CardAssetInfo selectedCard = Resources.Load<CardAssetInfo>("CardsTemplate/" + "Ella");
        Editor selectedCardEditor = Editor.CreateEditor(selectedCard);
        selectedCardEditor.OnInspectorGUI();


        GUILayout.EndVertical();                                                        //end vertical section
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

