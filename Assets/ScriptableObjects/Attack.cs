using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackScriptable",menuName = "ScriptableObjects/AttackScriptable")]
public class Attack : ScriptableObject
{
    public string description;
    public Sprite icon;
    public int damageMultiplier;
    public int attackCount;
    public StatusEffect appliesStatusEffect;

    public int currentCooldown;
    public int cooldown;
}
