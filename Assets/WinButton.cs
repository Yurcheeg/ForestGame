using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinButton : MonoBehaviour
{
    [SerializeField]private Button button;
    private SceneChanger sceneChanger;
    private void Awake()
    {
        if(button == null)
        {
            Debug.LogWarning("no button on winscreen");
        }
        sceneChanger = FindObjectOfType<SceneChanger>();
        if (sceneChanger != null)
        {
            SceneChanger.Scenes gameScene = SceneChanger.Scenes.Game;
            button.onClick.AddListener(() => { sceneChanger.ChangeScene((int)gameScene); });
            Debug.Log("button assigned to change scene to game");
        }
    }
}
