using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    Player player;
    Enemy enemy;
    List<Unit> units = new List<Unit>();
    [SerializeField] TMP_Text playerHpText;
    [SerializeField] TMP_Text enemyHpText;
    Slider playerSlider;
    Slider enemySlider;
    Image playerSliderImage;
    Image enemySliderImage;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();
        units.Add(player);
        units.Add(enemy);
        playerSlider = playerHpText.GetComponentInParent<Slider>();
        enemySlider = enemyHpText.GetComponentInParent<Slider>();
        playerSliderImage = playerSlider.GetComponentInChildren<Image>();
        enemySliderImage = enemySlider.GetComponentInChildren<Image>();
        
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

    public void ChangeSliderColor(Unit unit,Color color)
    {
        if (unit == player)
        {
            playerSliderImage.color = color;
        }
        else if (unit == enemy)
        {
            enemySliderImage.color = color;
        }
    }
}
