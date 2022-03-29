using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    [SerializeField]
    private SCENE_TYPE sceneType;
    [SerializeField]
    private SaveData saveData;
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button roadDataButton;
    void Start()
    {
        saveData = GetComponent<SaveData>();
        newGameButton.onClick.AddListener(NewGame);
        roadDataButton.onClick.AddListener(Road);
       // roadDataButton.onClick.AddListener(saveData.RoadData);機能は動くが今回は使用しない
    }
    public void NewGame()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        SceneChange.instance.SceneChangeType(sceneType);
        Database.instance.SetUpParameter();
    }
    public void Road()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        saveData.RoadData();
    }
}
