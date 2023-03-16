using UnityEditor;
using UnityEngine;

// IngredientDrawer
[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent mainLabel)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, mainLabel, property);
        PropertyDrawerHelper.DrawProperty(position, property, mainLabel,
            new PropertyDrawerHelper.PropertyData("ingredientName", "Name: "),
            new PropertyDrawerHelper.PropertyData("amount", "Amount: "),
            new PropertyDrawerHelper.PropertyData("unit", "Type: "),
            new PropertyDrawerHelper.PropertyData("tool", "Tool: "));
        EditorGUI.EndProperty();
        //EditorGUI.PropertyField()
        //    public string name;
        //public int amount = 1;
        //public IngredientUnit unit;
    }
}

[CustomPropertyDrawer(typeof(Tool))]
public class ToolDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent mainLabel)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, mainLabel, property);
        PropertyDrawerHelper.DrawProperty(position, property, mainLabel,
            new PropertyDrawerHelper.PropertyData("toolName", "Name: "),
            new PropertyDrawerHelper.PropertyData("color", "Color: "),
            new PropertyDrawerHelper.PropertyData("power", "Power: "),
            new PropertyDrawerHelper.PropertyData("position", "Position: "),
            new PropertyDrawerHelper.PropertyData("smash", "Smash: "));
        EditorGUI.EndProperty();
        //EditorGUI.PropertyField()
        //    public string name;
        //public int amount = 1;
        //public IngredientUnit unit;
    }
}
