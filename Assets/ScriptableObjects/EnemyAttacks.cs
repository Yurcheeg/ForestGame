using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack",menuName ="ScriptableObjects/EnemyAttackScriptable")]
public class EnemyAttacks : ScriptableObject
{
    public int damage;
    public int attackCount;
    public bool appliesStatus;
    public StatusEffect statusEffect;
    public Sprite attackSprite;
    public string animationTriggerName;

    public int currentCooldown;
    public int cooldown;
}
