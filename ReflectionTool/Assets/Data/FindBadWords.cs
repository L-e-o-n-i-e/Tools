using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System;

public class FindBadWords : MonoBehaviour
{
    private string filePath;
    HashSet<string> words;

    private void Start()
    {
      
        InitializeList();
        IsThereABadWordInHere();
    }

    public void IsThereABadWordInHere()
    {
        SearchAllClasses();
        SearchAllFields();
        SearchAllMethods();
    }

    void SearchAllClasses()
    {
        Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (Type classType in allTypes)
        {
            if (words.Contains(classType.ToString().ToLower()))
            {
                Debug.Log("Found banned word in class : " + classType.ToString() + " . Action required to change the name of the class.");
            }
        }
    }

    void SearchAllFields()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                if (words.Contains(field.Name.ToLower()))
                {
                   Debug.Log("Found banned word in field : " + field.Name + " from class : " + type.ToString() + " . Action required to change the name of the field.");
                }
            }
        }
    }
    void SearchAllMethods()
    {
        Debug.Log("Seraching through methods.");
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
            foreach (MethodInfo method in methods)
            {
                if (words.Contains(method.Name.ToLower()))
                {
                    Debug.Log("Found banned word in method : " + method.Name + " from class : " + type.ToString() + " . Action required to change the name of the method.");
                }
            }
        }
    }

    private void InitializeList()
    {

        words = new HashSet<string> {
            "chocolate",
              "sunshine",
              "mountain",
              "ocean",
              "butterfly",
              "rainbow",
              "waterfall",
              "breeze",
              "sparkle",
              "sunset",
              "river",
              "forest",
              "thunder",
              "meadow",
              "desert",
              "flower",
              "jungle",
              "moonlight",
              "snowflake",
              "starlight",
              "beach",
              "birdsong",
              "blossom",
              "brook",
              "campfire",
              "cloud",
              "dolphin",
              "firefly",
              "garden",
              "harmony",
              "hummingbird",
              "lighthouse",
              "orchid",
              "peace",
              "piano",
              "seashell",
              "serenity",
              "smile",
              "snowy",
              "sunflower",
              "trout",
              "tulip",
              "water",
              "watermelon",
              "whale",
              "wind",
              "yoga",
              "zebra"
        };

    }
}