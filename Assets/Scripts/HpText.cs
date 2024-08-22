using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    Player player;
    Enemy enemy;
    [SerializeField] TMP_Text playerHpText;
    [SerializeField] TMP_Text enemyHpText;
    Slider playerSlider;
    Slider enemySlider;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();
        playerSlider = playerHpText.GetComponentInParent<Slider>();
        enemySlider = enemyHpText.GetComponentInParent<Slider>();
        
        playerSlider.maxValue = player.maxHealth;


        enemySlider.maxValue = enemy.maxHealth;
    }
    private void Update()
    {
        //player txt
        playerHpText.text = $"{player.currentHealth}/{player.maxHealth}";
        playerSlider.value = player.maxHealth - player.currentHealth;

        //enemy txt
        enemyHpText.text = $"{enemy.currentHealth}/{enemy.maxHealth}";
        enemySlider.value = enemy.maxHealth - enemy.currentHealth;
    }
}
