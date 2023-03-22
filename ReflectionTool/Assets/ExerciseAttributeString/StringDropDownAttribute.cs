using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field)]
public class StringDropDownAttribute : PropertyAttribute
{
    List<string> stringDropdown;    

    public StringDropDownAttribute(params string[] words)
    {
        stringDropdown = new List<string>();

        foreach (string word in words)
        {
            stringDropdown.Add(word);
        }
    }
}