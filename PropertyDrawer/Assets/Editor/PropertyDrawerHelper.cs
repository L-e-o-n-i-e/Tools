using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;

public static class PropertyDrawerHelper 
{
    public static void DrawProperty(Rect position, SerializedProperty property, GUIContent GUIlabel, params PropertyData[] properties)
    {
        //Get the height of a single line
        float LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
        //Get the rect start position
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIlabel);

        for (int i = 0; i < properties.Length; i++)
        {
            float widthOfProperty = position.width / properties.Length;


            Rect r = new Rect(position.x + widthOfProperty*i, position.y, widthOfProperty, LINE_HEIGHT);
            SerializedProperty nameProperty = property.FindPropertyRelative(properties[i].propertyName);
            SerializedPropertyType propertyType = nameProperty.propertyType;

            float lw = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(new GUIContent(properties[i].label)).x;
            EditorGUI.PropertyField(r, nameProperty, new GUIContent(properties[i].label));
            EditorGUIUtility.labelWidth = lw;
            continue;

            switch (propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    nameProperty.intValue = EditorGUI.IntField(r, nameProperty.intValue);
                    break;
                case SerializedPropertyType.Boolean:
                    break;
                case SerializedPropertyType.Float:
                    break;
                case SerializedPropertyType.String:
                    nameProperty.stringValue = EditorGUI.TextField(r, nameProperty.stringValue);
                    break;
                case SerializedPropertyType.Color:
                    break;
                case SerializedPropertyType.ObjectReference:
                    break;
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    break;
                case SerializedPropertyType.Vector2:
                    break;
                case SerializedPropertyType.Vector3:
                    break;
                case SerializedPropertyType.Vector4:
                    break;
                case SerializedPropertyType.Rect:
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    break;
                case SerializedPropertyType.Bounds:
                    break;
                case SerializedPropertyType.Gradient:
                    break;
                case SerializedPropertyType.Quaternion:
                    break;
                case SerializedPropertyType.ExposedReference:
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    break;
                case SerializedPropertyType.Vector3Int:
                    break;
                case SerializedPropertyType.RectInt:
                    break;
                case SerializedPropertyType.BoundsInt:
                    break;
                case SerializedPropertyType.ManagedReference:
                    break;
                case SerializedPropertyType.Hash128:
                    break;
                default:
                    break;
            }
            //EditorGUI.PropertyField
            
        }
        // Draw label

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

    }

    public class PropertyData
    {
        public string propertyName;
        public string label;

        public PropertyData(string propertyName, string label)
        {
            this.propertyName = propertyName;
            this.label = label;
        }
    }
}

