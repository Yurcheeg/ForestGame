using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "ScriptableObjects/ItemScriptable")]

public class Item : ScriptableObject
{
    public string description;
    public Sprite icon;
}
