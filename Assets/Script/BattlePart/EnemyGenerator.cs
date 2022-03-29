using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    private bool dieState = true;
    [SerializeField]
    private GameObject[] enemys = new GameObject[3];
    [SerializeField]
    private GameObject fightEnemy;
    public Animator instantiateAnimator;
    [SerializeField]
    private Text enemyNameText;//出現個体の名前
    [SerializeField]
    private Text enemyHPText;//出現個体のHP

    void Start()
    {
        Interlock();
    }


    void Update()
    {
        Die();
        enemyHPText.text = "HP:" +Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP.ToString();
    }

    void Die()
    {
        if (Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP <= 0 && dieState)
        {
            dieState = false;
            Destroy(fightEnemy);
        }
    }
    /// <summary>
    /// ランダムに出現する敵と敵のステータスの連動化
    /// </summary>
    void Interlock()
    {
        //出現個体ランダム
        Database.instance.enemyStatus.EnemyNo = Random.Range(0, 2);
        switch (Database.instance.enemyStatus.EnemyNo)
        {//出現した個体にステータス内容紐づけ
            case 0:
                Database.instance.enemyStatus.Enemy1Status();
                break;

            case 1:
                Database.instance.enemyStatus.Enemy2Status();
                break;
        }
        if (Database.instance.enemyStatus.isLastBossFlag == true)
        {
            Database.instance.enemyStatus.BossStatus();
            Database.instance.enemyStatus.EnemyNo = 2;
            fightEnemy = Instantiate(enemys[Database.instance.enemyStatus.EnemyNo], transform.position, Quaternion.identity);
            instantiateAnimator = fightEnemy.GetComponent<Animator>();
            Database.instance.IsBossFlag = true;
            enemyNameText.text = enemys[Database.instance.enemyStatus.EnemyNo].name + "出現！";
            enemyHPText.text ="HP:"+ Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP.ToString();
            return;
        }
        else
        {
            //出現させる
            fightEnemy = Instantiate(enemys[Database.instance.enemyStatus.EnemyNo], transform.position, Quaternion.identity);
            instantiateAnimator = fightEnemy.GetComponent<Animator>();//型が分からなくなった場合はvarで確認した方が早い
            enemyNameText.text = enemys[Database.instance.enemyStatus.EnemyNo].name + "出現！";
            enemyHPText.text = "HP:" + Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP.ToString();
        }
    }
}