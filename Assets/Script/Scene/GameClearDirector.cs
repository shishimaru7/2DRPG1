using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearDirector : MonoBehaviour
{
    public SCENE_TYPE sceneType;

    public void GoTitle()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        SceneChange.instance.SceneChangeType(sceneType);
    }
}