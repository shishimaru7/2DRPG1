using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    public Animator animator;//BattleManagerで動かす

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}