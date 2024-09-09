using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect",menuName ="ScriptableObjects/StatusEffectScriptable")]

public class StatusEffect : ScriptableObject
{
    public string description;
    public int priority;
    public Sprite icon;
}
