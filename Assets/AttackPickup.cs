using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : MonoBehaviour
{
    [SerializeField] private Attack attack;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInfo.instance.attackList.Add(attack);
        Destroy(gameObject);
    }
}
