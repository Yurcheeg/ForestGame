using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]private Rigidbody2D player;
    private void Awake()
    {
        if (player == null)
        {
            Debug.Log("No player Rigidbody found");
        }
    }
}
