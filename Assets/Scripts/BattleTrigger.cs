using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]private Transform player;
    private Transform enemy;
    [SerializeField]private SceneChanger sceneChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Enemy>() != null)
        {
            sceneChanger.ChangeScene((int)SceneChanger.Scenes.Battle);
        }
    }

}
