using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy",menuName ="ScriptableObjects/EnemyScriptable")]
public class Enemies : ScriptableObject
{
    
    public int health;
    public List<EnemyAttacks> enemyAttacks = new List<EnemyAttacks>();
}
