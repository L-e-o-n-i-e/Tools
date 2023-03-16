using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

// Custom serializable class
[Serializable]
public class Ingredient
{
    public string ingredientName;
    public int amount = 1;
    public IngredientUnit unit;
    public GameObject tool;
}

[Serializable]
public class Tool
{
    public string toolName;
    public int power = 3;
    public Color color;
    [SerializeField] private Vector2 position;
    public AnimationCurve smash;
}

public class Recipe : MonoBehaviour
{
    public Ingredient potionResult;
    public Ingredient[] potionIngredients;
    public Tool tool;
    
}

