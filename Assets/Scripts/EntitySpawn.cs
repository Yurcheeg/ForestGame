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
        System.Random rnd = new();
        int prefabIndex;
        for (int i = 1; i < 10; i++)
        {
            prefabIndex = rnd.Next(0, _prefabs.Count);
            float offset = rnd.Next(15, 20);
            Debug.Log(offset);
            currentObjectPos = new Vector2(offset + lastObjectPos.x, _prefabs[prefabIndex].transform.position.y);
            Instantiate(_prefabs[prefabIndex], currentObjectPos, Quaternion.identity);

            lastObjectPos = currentObjectPos;

            prefabIndex = rnd.Next(0, _prefabs.Count);
            currentObjectPos = new Vector2(-(offset + lastObjectPos.x), _prefabs[prefabIndex].transform.position.y);
            Instantiate(_prefabs[prefabIndex], currentObjectPos, Quaternion.identity);

            lastObjectPos = -currentObjectPos;
        }
    }
}
