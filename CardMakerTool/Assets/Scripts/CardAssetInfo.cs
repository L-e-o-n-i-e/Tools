using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CardAssetInfo : ScriptableObject
{
    public Sprite sprite;
    public string cardName;
    public int mana;
    public Color color;
    public string  description;
    [Range(0, 5)]
    public int power;

    public CardAssetInfo()
    {
        Debug.Log("Constructor of CardAssetInfo");
    }
    public CardAssetInfo(string name, int mana, Color color, Sprite sprite, string description, int power)
    {
        this.name = name;
        this.mana = mana;
        this.color = color;
        this.sprite = sprite;
        this.description = description;
        this.power = power;
        Debug.Log("Constructor with args of CardAssetInfo");
    }
}
