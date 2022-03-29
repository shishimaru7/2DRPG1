using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour
{

    private void Start()
    {
       Database.instance.enemyStatus.isLastBossFlag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Database.instance.enemyStatus.isLastBossFlag = true;
            SceneChange.instance.SceneChangeType(SCENE_TYPE.LastBattle);
        }
    }
}