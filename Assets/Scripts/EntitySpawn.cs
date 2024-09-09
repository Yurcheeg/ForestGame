using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntitySpawn : MonoBehaviour
{
    private Vector2 lastObjectPos;
    private Vector2 currentObjectPos;
    [SerializeField]private List<GameObject> _prefabs = new();
    private void Awake()
    {
        lastObjectPos = FindObjectOfType<Player>().transform.position;
    }
    private void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        System.Random rnd = new System.Random();
        int prefabIndex = rnd.Next(0, _prefabs.Count);
        for (int i = 1; i < 10; i++)
        {
            float offset = rnd.Next(20, 25);
            Debug.Log(offset);
            currentObjectPos = new Vector2(offset + lastObjectPos.x, lastObjectPos.y);
            Instantiate(_prefabs[prefabIndex], currentObjectPos, Quaternion.identity);
            Instantiate(_prefabs[prefabIndex], new Vector2(-currentObjectPos.x,currentObjectPos.y), Quaternion.identity);

            lastObjectPos = currentObjectPos;
        }
    }
}
