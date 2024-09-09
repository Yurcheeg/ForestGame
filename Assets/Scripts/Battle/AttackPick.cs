using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackPick : MonoBehaviour
{
    [SerializeField]private List<Button> attackButtons = new();
    private AvailableAttacks availableAttacks;
    private Battle battle;
    private void Awake()
    {
        availableAttacks = FindObjectOfType<AvailableAttacks>(true);
        battle = FindObjectOfType<Battle>(true);
        if (attackButtons.Count <= 0)
        {
            Debug.Log("No attack buttons assigned");
        }

        foreach (var button in attackButtons)
        {
            var localButton = button;
            //add text to buttons

            //assign attack to each buttons
            localButton.onClick.AddListener(() => battle.StartCoroutine(battle.Attack(availableAttacks.GetAttackByName(button.GetComponentInChildren<TMP_Text>().text))));
        }
    }
}
