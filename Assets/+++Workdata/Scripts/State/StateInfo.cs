using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateInfo
{
    public string id;

    public string name;

    public int amount;

    public Sprite icon;

    public string description;

    public int stackSize;

    public enum ItemType
    {
        Resource,
        Weapon,
        Tool,
        Armor,
        Consumable,
        Currency
    }

    public ItemType type;
}