using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public void Awake()
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

  private  AudioSource[] audioSource;//0がBGM、1がSE

    [SerializeField]
    private AudioClip[] BGM;
    public AudioClip[] gBGM { get { return BGM; } }
    [SerializeField]
    private AudioClip[] SE;
    public AudioClip[] gSE { get { return SE; } }

    private string titleScene = "Title";
    private string townScene = "Town";
    private string actitonScene = "ActionStage";
    private string battleScene = "Battle";
    private string lastBattleScene = "LastBattle";
    private string gameOverScene = "GameOver";
    private string gameClearScene = "GameClear";

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioSource[0].clip = BGM[0];
        audioSource[0].Play();

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    public void ClickSE(AudioClip[] audioClip)
    {
        audioSource[1].PlayOneShot(SE[0]);
    }
    public void PlayerNormalAttackSE(AudioClip[] audioClip)
    {
        audioSource[1].PlayOneShot(SE[1]);
    }
    public void PlayerSkillAttackSE(AudioClip[] audioClip)
    {
        audioSource[1].PlayOneShot(SE[4]);
    }
    public void ItemeEffectSE(AudioClip[] audioClip)
    {
        audioSource[1].PlayOneShot(SE[2]);
    }
    public void EmemySkillSE(AudioClip[] audioClip)
    {
        audioSource[1].PlayOneShot(SE[3]);
    }

    //シーンが切り替わった時に呼ばれるメソッド(自作)
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (titleScene == "Title" && nextScene.name == "Title")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[0];    //流すクリップを切り替える
            audioSource[0].Play();
        }

        if (townScene == "Town" && nextScene.name == "Town")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[1];
            audioSource[0].Play();
        }
        if (actitonScene == "ActionStage" && nextScene.name == "ActionStage")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[2];
            audioSource[0].Play();
        }
        if (battleScene == "Battle" && nextScene.name == "Battle")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[3];
            audioSource[0].Play();
        } if (lastBattleScene == "LastBattle" && nextScene.name == "LastBattle")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[4];
            audioSource[0].Play();
        } if (gameOverScene == "GameOver" && nextScene.name == "GameOver")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[5];
            audioSource[0].Play();
        } if (gameClearScene == "GameClear" && nextScene.name == "GameClear")
        {
            audioSource[0].Stop();
            audioSource[0].clip = BGM[6];
            audioSource[0].Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
       // titleScene = nextScene.name;
    }
}