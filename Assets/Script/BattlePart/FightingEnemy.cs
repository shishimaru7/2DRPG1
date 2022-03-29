using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingEnemy : MonoBehaviour
{
   public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}