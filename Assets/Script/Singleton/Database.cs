using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance;

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
    public PlayerStatus playerStatus;
    public EnemyStatus enemyStatus;
    [SerializeField]
    private int sKillCheckConut;
    public int SKillCheckConut { set { sKillCheckConut = value; } get { return sKillCheckConut; } }
    private Vector3 savePointPosition;//セーブ時座標一時保存させる
    public Vector3 SavePointPosition { set { savePointPosition = value; } get { return savePointPosition; } }
   private bool isSavePoint;//セーブ時座標一時保存のチェック
   public bool IsSavePoint { set { isSavePoint = value; } get { return isSavePoint; } }
    private Vector3 enCountPosition;//エンカウント時座標一時保存させる
    public Vector3 EnCountPosition { set { enCountPosition = value; } get { return enCountPosition; } }
    private bool isBossFlag;//ボス戦開始でON
    public bool IsBossFlag { set { isBossFlag = value; } get { return isBossFlag; } }

    [SerializeField]
   private GameObject healEffect;
   public GameObject getHealEffect { get { return healEffect; } }
    [SerializeField]
    private GameObject spEffect;

     public   GameObject getSpEffect { get { return spEffect; } }
    [SerializeField]
    private GameObject rescueEffect;
    public GameObject getRescueEffect { get { return rescueEffect; } }

    void Start()
    {
        SetUpParameter();
    }

    /// <summary>
    /// ステータス初期値
    /// </summary>
    public void SetUpParameter()
    {
        isBossFlag = false;

        playerStatus.Level = 1;
        playerStatus.NextLevelPoint = 5;
        playerStatus.ExpStock = 0;
        playerStatus.GoldStock = 0;
        playerStatus.MaxHP = 15;
        playerStatus.MaxSP = 5;
        playerStatus.AttackPower = 10;
        playerStatus.DefensePower = 10;

        playerStatus.HP = playerStatus.MaxHP;
        playerStatus.SP = playerStatus.MaxSP;
        playerStatus.getSkillList[0].SkillGet = false;
        playerStatus.getSkillList[1].SkillGet = false;
        sKillCheckConut = 0;

        playerStatus.getHaveItemList[0].HaveItem = 0;
        playerStatus.getHaveItemList[1].HaveItem = 0;
        playerStatus.getHaveItemList[2].HaveItem = 0;
    }
}