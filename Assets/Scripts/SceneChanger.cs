using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : SingletonPersistent<SceneChanger>
{
    public enum Scenes
    {
        Start,
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
