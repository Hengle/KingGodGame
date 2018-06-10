﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : ObjectBase {
    public static PlayerBase instance = null;
    public float energy, attackSpeed, attackRange, devilGage, etere;
    public bool isExmove, isChargeAttack;

    protected override void Init()
    {
        if (instance)//인스턴스가 생성되어있는가?
        {
            DestroyImmediate(gameObject);//생성되어있다면 중복되지 않도록 삭제
            return;
        }
        else//인스턴스가 null일 때
        {
            instance = this;//인스턴스가 생성되어있지 않으므로 지금 이 오브젝트를 인스턴스로
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 계속 유지하도록 설정
        }

        base.Init();
        isInvincibility = false;

        maxHP =         5;
        curHP =         5;
        attackPower =   30;
        attackSpeed =   1;
        attackRange =   1;
        moveSpeed =     8;
        pushBack  =     5;
        energy =        3;
        etere =         0;
        devilGage =     30;

        isExmove = false;
        isChargeAttack = false;
    }

    public override void Attack()
    {
        
    }

    public override void Damaged(DamageNode damageNode)
    {
        GetComponent<PlayerHealth>().Damaged(damageNode);
    }

    public override void Dead()
    {

    }

    public void ExMoveAttack()
    {
        GetComponent<PlayerAttack>().skillAttack(4);
    }

    public void ChargeAttack()
    {

    }

    public void PlayerEnable()
    {
        transform.GetComponent<PlayerMovement>().enabled = true;
        transform.GetComponent<PlayerHealth>().enabled = true;
        transform.GetComponent<PlayerAttack>().enabled = true;
        transform.GetComponent<EXMove>().enabled = true;
        transform.GetComponent<ExplosionAttack>().enabled = true;
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Collider>().isTrigger = false;
    }

    public void PlayerDisable()
    {
        transform.GetComponent<PlayerMovement>().enabled = false;
        transform.GetComponent<PlayerHealth>().enabled = false;
        transform.GetComponent<PlayerAttack>().enabled = false;
        transform.GetComponent<EXMove>().enabled = false;
        transform.GetComponent<ExplosionAttack>().enabled = false;
        //transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Collider>().isTrigger = true;
    }

    // Use this for initialization
    void Awake () {
        Init();
	}
}
