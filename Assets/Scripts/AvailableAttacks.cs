using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvailableAttacks : MonoBehaviour
{
    [SerializeField]private List<Attack> attacks;
    public List<Attack> AttackList => attacks;
    [SerializeField]private List<Button> buttons;
    private Dictionary<string, Attack> attackNames = new();
    private void Awake()
    {
        AssignButton();
    }
    void AssignButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null && attacks[i] != null)
            {
                buttons[i].GetComponentInChildren<TMP_Text>().text = attacks[i].name;
                attackNames.Add(attacks[i].name, attacks[i]);
                Debug.Log($"added {attacks[i].name}");
               // buttons[i].name = attacks[i].name;
            }
        }
    }
    public Attack GetAttackByName(string name)
    {
        attackNames.ContainsKey(name);
        Debug.Log($"{ attackNames[name]} :)");
        return attackNames[name];
    }
}
