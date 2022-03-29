using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDirector : MonoBehaviour
{
    [SerializeField]
    private SaveData saveData;
    [SerializeField]
    private Button goTitleButton;
    [SerializeField]
    private Button roadButton;

    void Start()
    {
        saveData = GetComponent<SaveData>();
        goTitleButton.onClick.AddListener(GoTitle);
        roadButton.onClick.AddListener(Road);
    }

    public void GoTitle()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        SceneChange.instance.SceneChangeType(SCENE_TYPE.Title);
    }
    public void Road()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        saveData.RoadData();
    }
}