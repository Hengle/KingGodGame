﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MoveBase {
    EnemyBase enemyEntity;
    GameObject player;
    NavMeshAgent nav;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        enemyEntity = entity as EnemyBase;
        player = enemyEntity.player;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void MoveUpdate()
    {
        if (enemyEntity.isAgro)
        {
            if (nav.enabled && !enemyEntity.isAttack)
            {
                nav.destination = player.transform.position;

                if (Vector3.Distance(player.transform.position, transform.position) > enemyEntity.attackDistance)
                {
                    StartMove();
                }
                else
                {
                    StopMove();
                }
            }
            else
            {
                StopMove();
            }
        }
    }


    public void StopMove()
    {
        nav.speed = 0;
        enemyEntity.isMove = false;
        anim.SetBool("Walk", false);
    }

    public void StartMove()
    {
        nav.speed = enemyEntity.moveSpeed;
        enemyEntity.isMove = true;
        anim.SetBool("Walk", true);
    }

    public void StopWalk()
    {
        nav.speed = 0;
    }
}
