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
    }

    private int nbCol = 10;
    private int nbRows = 10;
    private Color[] colors;
    Texture colorTexture;
    Renderer textureTarget;


    void OnGUI() //Called every frame in Editor window
    {
        GUILayout.BeginHorizontal();        //Have each element below be side by side
        DoControls();
        DoCanvas();
        GUILayout.EndHorizontal();
    }

    void DoControls()
    {
        GUILayout.BeginVertical();                                                      //Start vertical section, all GUI draw code after this will belong to same vertical
        GUILayout.Label("Create a new card", EditorStyles.largeLabel);                            //A label that says "Toolbar"



       //Diviser en 3 grands rectangles : Gauche, milieu, Droite

        //GAUCHE : haut en bas
        //File Name : displaying name of the card
        //Button : Previous card

        //MILIEU :
        //Rectagle : Display the card
        //Button : Create New Card 

        //DROITE : 
        //Button : Load
        //Button : Go to file 
        //Button : Next Card


        //selectedColor = EditorGUILayout.ColorField("Paint Color", selectedColor);       //Make a color field with the text "Paint Color" and have it fill the selectedColor var
        //eraseColor = EditorGUILayout.ColorField("Erase Color", eraseColor);             //Make a color field with the text "Erase Color"
        //if (GUILayout.Button("Fill All"))                                               //A button, if pressed, returns true
        //    colors = colors.Select(c => c = selectedColor).ToArray();                   //Linq expresion, for every color in the color array, sets it to the selected color

        //randomness = EditorGUILayout.Slider("Randomness of color:", randomness, 0, 1);  //A slide controling a float range to set a random value to the colors we paint

        //GUILayout.FlexibleSpace();                                                      //Flexible space uses any left over space in the loadout
        //textureTarget = EditorGUILayout.ObjectField("Output Renderer", textureTarget, typeof(Renderer), true) as Renderer;  //Build an object field that accepts a renderer

        //if (GUILayout.Button("Save to Object"))
        //{
        //    Texture2D t2d = new Texture2D(nbCol, nbRows);                               //Create a new texture
        //    t2d.filterMode = FilterMode.Point;                                          //Simplest non-blend texture mode
        //    Material m = new Material(Shader.Find("Diffuse"));              //Materials require Shaders as an arguement, Diffuse is the most basic type
        //    m.mainTexture = t2d;                             //sharedMaterial is the MAIN RESOURCE MATERIAL. Changing this will change ALL objects using it, .material will give you the local instance

        //    for (int i = 0; i < nbCol; i++)
        //    {
        //        for (int j = 0; j < nbRows; j++)
        //        {
        //            int index = j + i * nbRows;
        //            t2d.SetPixel(i, nbRows - 1 - j, colors[index]);                     //Color every pixel using our color table, the texture is 8x8 pixels large, but strecthes to fit
        //        }
        //    }
        //    t2d.Apply();                                                                //Apply all changes to texture
        //}
        GUILayout.EndVertical();                                                        //end vertical section
    }

    void DoCanvas()
    {
        //Event evt = Event.current;                     //Grab the current event

        //Color oldColor = GUI.color;                    //GUI color uses a static var, need to save the original to reset it
        //GUILayout.BeginHorizontal();                   //All following gui will be on one horizontal line until EndHorizontal is called
        //for (int i = 0; i < nbCol; i++)
        //{
        //    GUILayout.BeginVertical();                //All following gui will be in a vertical line
        //    for (int j = 0; j < nbRows; j++)
        //    {
        //        int index = i + (j * nbRows);           //Rememeber, this is just like a 2D array, but in 1D                          


        //        Rect colorRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)); //Reserve a square, which will autofit to the size given
        //        if ((evt.type == EventType.MouseDown || evt.type == EventType.MouseDrag) && colorRect.Contains(evt.mousePosition)) //Can now paint while dragging update
        //        {
        //            if (evt.button == 0)
        //            {
        //                this.colors[index] = ApplyRandomness(selectedColor);
        //            }
        //            if (evt.button == 2)
        //            {
        //                Color originalColor = colors[index];
        //                ChangeNeighbourghsColor(index, originalColor, selectedColor);
        //            }
        //            if (evt.button == 1)
        //                this.colors[index] = eraseColor;   //Set the color of the index

        //            evt.Use();                               //The event was consumed, if you try to use event after this, it will be non-sensical

        //        }

        //        GUI.color = colors[index];            //Same as a 2D array
        //        GUI.DrawTexture(colorRect, colorTexture); //This is colored by GUI.Color!!!
        //    }
        //    GUILayout.EndVertical();                  //End Vertical Zone
        //}
        //GUILayout.EndHorizontal();                    //End horizontal zone
        //GUI.color = oldColor;                         //Restore the old color
    }
}

