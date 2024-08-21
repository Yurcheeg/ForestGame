using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    private int turnsAffectedCount = 0;
    public Dictionary<string, Action<Unit>> statusCall = new();
    [SerializeField]private List<StatusEffect> statusEffectList = new();
    private Dictionary<string, StatusEffect> statusName = new();
    private Battle battle;
    private void Awake()
    {
        battle = FindAnyObjectByType<Battle>();
        statusCall.Add("Poison", Poison);
        statusCall.Add("Bleed", Bleed);
        for(int i = 0; i < statusEffectList.Count; i++)
        {
            statusName.Add(statusEffectList[i].name, statusEffectList[i]);
        }
    }
    public void Poison(Unit unit)
    {
        unit.affectedByStatusEffect = statusName["Poison"];
        Debug.Log($"{unit} is poisoned");
    }
    public IEnumerator ApplyPoison(Unit unit)
    {
            turnsAffectedCount++;
            int poisonDamage = 1;
            poisonDamage *= turnsAffectedCount;
            unit.currentHealth -= poisonDamage;
            Debug.Log($"{unit} is hit by poison for:{poisonDamage}");
            Color poisonColor = new Color(0f, 1f, 0f);
            yield return StartCoroutine(battle.HitFlash(unit, poisonColor));
        
    }
    public void Bleed(Unit unit)
    {
        unit.affectedByStatusEffect = statusName["Bleed"];
        Debug.Log($"{unit} is bleeding");
    }
    public IEnumerator ApplyBleed(Unit unit)
    {
            int bleedDamage = 1;
            unit.currentHealth -= bleedDamage;
            Debug.Log($"{unit} is hit by bleed for:{bleedDamage}");
            Color bleedColor = new Color(1f, 0.7f, 0);
            yield return StartCoroutine(battle.HitFlash(unit, bleedColor));
    }

    public void ApplyStatusEffects(object sender, Unit unit)
    {
        if(unit == null || unit.affectedByStatusEffect == null)
        {
            Debug.LogError("no unit or the unit is not affected by an effect");
            return;
        }
        if (unit.affectedByStatusEffect.name == "Poison")
        {
            StartCoroutine(ApplyPoison(unit));
        }
        if (unit.affectedByStatusEffect.name == "Bleed")
        {
            StartCoroutine(ApplyBleed(unit));
        }
        
    }
}
