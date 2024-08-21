using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptable", menuName = "ScriptableObjects/WeaponScriptable")]

public class Weapon : ScriptableObject
{
    public string description;
    public Sprite icon;
}
