using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent mainLabel)
    {
        EditorGUI.BeginProperty(position, mainLabel, property);
        PropertyDrawerHelper.DrawProperty(position, property, mainLabel,
            new PropertyDrawerHelper.PropertyData("ingredientName", "Name: "),
            new PropertyDrawerHelper.PropertyData("amount", "Amount: "),
            new PropertyDrawerHelper.PropertyData("unit", "Type: "),
            new PropertyDrawerHelper.PropertyData("tool", "Tool: "));
        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer(typeof(Tool))]
public class ToolDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent mainLabel)
    {
        EditorGUI.BeginProperty(position, mainLabel, property);
        PropertyDrawerHelper.DrawProperty(position, property, mainLabel,
            new PropertyDrawerHelper.PropertyData("toolName", "Name: "),
            new PropertyDrawerHelper.PropertyData("color", "Color: "),
            new PropertyDrawerHelper.PropertyData("power", "Power: "),
            new PropertyDrawerHelper.PropertyData("position", "Position: "),
            new PropertyDrawerHelper.PropertyData("smash", "Smash: "));
        EditorGUI.EndProperty();

    }
}
