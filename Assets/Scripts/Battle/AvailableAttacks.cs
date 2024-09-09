using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AvailableAttacks : MonoBehaviour
{
    private List<Attack> attacks;
    [SerializeField]private List<Button> buttons;
    private Dictionary<string, Attack> attackNames = new();
    private List<Slider> cooldownSlider = new();
    private void Awake()
    {
        attacks = PlayerInfo.instance.attackList;
        AssignButton();
        FillSliderList();

    }
    private void Start()
    {
        ApplyCooldown(this,EventArgs.Empty);
    }
    void AssignButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null)
            {
                if (i >= attacks.Count)
                {
                    buttons[i].gameObject.SetActive(false);
                }
                else
                {
                    buttons[i].GetComponentInChildren<TMP_Text>().text = attacks[i].name;
                    attackNames.Add(attacks[i].name, attacks[i]);
                    Debug.Log($"added {attacks[i].name}");
                }
            }
        }
    }
    public void ApplyCooldown(object sender, EventArgs e)
    {
        Debug.Log("apply cooldown called");
        for (int i = 0; i < attacks.Count; i++) 
        {
            Debug.Log($"this {cooldownSlider[i]}");
            if(attacks[i].cooldown > 0)
            {
                cooldownSlider[i].value = attacks[i].currentCooldown;
            }
        }
    }
    public void FillSliderList()
    {
        for(int i=0;i<attacks.Count;i++)
        {

            cooldownSlider.Add(buttons[i].GetComponentInChildren<Slider>());
            if (attacks[i].cooldown > 0)
            {
                cooldownSlider[i].maxValue = attacks[i].cooldown;
                cooldownSlider[i].minValue = 0;
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
