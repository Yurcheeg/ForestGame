using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public StatusEffect affectedByStatusEffect;
    public bool canSlide;
}
