using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPlay : MonoBehaviour
{
    public static DebugPlay instance;

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

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("PでPlayer最強、OでPlayer最弱、IでPlayerHPSP1、Uで少額、Yでアイテム所持のみ最大、Qで敵最強、"+"\n"+ "Zでbossの前、Xで街の前、");
            if (Input.GetKeyDown(KeyCode.P))
            {
               Database.instance.playerStatus.Level = 99;
                Database.instance.playerStatus.HP = 999;
                Database.instance.playerStatus.MaxHP = 999;
                Database.instance.playerStatus.SP = 999;
                Database.instance.playerStatus.MaxSP = 999;
                Database.instance.playerStatus.AttackPower = 999;
                Database.instance.playerStatus.DefensePower = 999;
                Database.instance.playerStatus.GoldStock = 9999;
                Database.instance.playerStatus.getSkillList[0].SkillGet = true;
                Database.instance.playerStatus.getSkillList[1].SkillGet = true;

                foreach (var items in Database.instance.playerStatus.getHaveItemList)
                {
                    items.HaveItem = 99;
                }
                Debug.Log("Player最強");
            }
            else if(Input.GetKeyDown(KeyCode.O))
            {
                Database.instance.playerStatus.Level = 1;
                Database.instance.playerStatus.HP = 1;
                Database.instance.playerStatus.MaxHP = 1;
                Database.instance.playerStatus.SP = 1;
                Database.instance.playerStatus.MaxSP = 1;
                Database.instance.playerStatus.AttackPower = 1;
                Database.instance.playerStatus.DefensePower = 1;
                Database.instance.playerStatus.GoldStock = 1;
                //TODO スキル
                Debug.Log("Player最弱");
            }
            else if(Input.GetKeyDown(KeyCode.I))
            {
                Database.instance.playerStatus.HP = 1;
                Database.instance.playerStatus.SP = 1;
                Debug.Log("PlayerHPSP1");
            }
            else if(Input.GetKeyDown(KeyCode.U))
            {
                Database.instance.playerStatus.GoldStock = 1000;
                Debug.Log("少額");
            }
            else if(Input.GetKeyDown(KeyCode.Y))
            {
                foreach (var items in Database.instance.playerStatus.getHaveItemList)
                {
                    items.HaveItem = 99;
                    Debug.Log("アイテム所持のみ最大");
                }
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
                foreach (var enemys in Database.instance.enemyStatus.getEnemyList)
                {
                    enemys.HP = 999;
                    enemys.MaxHP = 999;
                    enemys.AttackPower = 999;
                    enemys.DefensePower = 999;
                    Debug.Log("敵最強");
                }
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                if (SceneManager.GetActiveScene().name == "ActionStage")
                {
                    GameObject.Find("Player").transform.position = new Vector2(92,-1);
                }
                    Debug.Log("bossの前");
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                if (SceneManager.GetActiveScene().name == "ActionStage")
                {
                    GameObject.Find("Player").transform.position = new Vector2(-90,-1);
                }
                    Debug.Log("街の前");
            }
        }
    }
}