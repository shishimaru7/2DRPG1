using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static SceneChange instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 各シーンへの遷移
    /// </summary>
    /// <param name="sceneType">enum型、登録シーン</param>
  public  void SceneChangeType(SCENE_TYPE sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    }
}