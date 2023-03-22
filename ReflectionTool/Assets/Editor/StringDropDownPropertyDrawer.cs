using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringDropDownAttribute))]
public class StringDropDownProperty : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var rect = new Rect(position.x, position.y, 30, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("stringDropdown"), GUIContent.none);

        //_choiceIndex = EditorGUI.Popup(position, userIndexProperty.intValue, _choices);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    userIndexProperty.intValue = _choiceIndex;
        //}

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}

