using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpText : MonoBehaviour
{
    Player player;
    Enemy enemy;
    [SerializeField] TMP_Text playerHpText;
    [SerializeField] TMP_Text enemyHpText;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();
    }
    private void Update()
    {
        //player txt
        playerHpText.text = $"{player.name} Health: {player.currentHealth}";

        //enemy txt
        enemyHpText.text = $"{enemy.name} Health: {enemy.currentHealth}";
    }
}
