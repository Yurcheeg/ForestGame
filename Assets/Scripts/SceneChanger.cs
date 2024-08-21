using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public enum Scenes
    {
        Title,
        Game,
        Battle
    }

    public void ChangeScene(int sceneId)
    {
        Debug.Log(sceneId);
        Scenes scene = (Scenes)sceneId;
        Debug.Log(scene.ToString());
        SceneManager.LoadScene(scene.ToString());

    }
}
