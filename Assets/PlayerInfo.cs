using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : SingletonPersistent<PlayerInfo>
{
    public List<Attack> attackList = new();

    private void Start()
    {

    }
}
