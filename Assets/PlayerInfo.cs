using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : SingletonPersistent<PlayerInfo>
{
    public List<Attack> attackList = new();
    public Player player;

    public int playerHP;
    public StatusEffect statusEffect;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        if(playerHP <= 0)
        {
            playerHP = player.maxHealth;
        }
    }
}
