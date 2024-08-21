using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private Enemies enemiesScriptable;
    public Dictionary<string, EnemyAttacks> enemyAttacks = new();
    public Dictionary<int, EnemyAttacks> enemyAttackId = new();
    private void Awake()
    {
        if (enemiesScriptable == null)
        {
            Debug.Log("No enemiesScriptable attached");
        }
        maxHealth = enemiesScriptable.health;
        Debug.Log(maxHealth);
    }
    private void Start()
    {
        for(int i=0;i<enemiesScriptable.enemyAttacks.Count; i++)
        {
            enemyAttacks.Add(enemiesScriptable.enemyAttacks[i].name, enemiesScriptable.enemyAttacks[i]);
            enemyAttackId.Add(i, enemiesScriptable.enemyAttacks[i]);
            Debug.Log($"{enemyAttackId[i]},  {i}");
        }
    }
    
}
