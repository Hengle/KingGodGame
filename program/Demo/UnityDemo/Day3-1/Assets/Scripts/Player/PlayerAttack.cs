﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    protected Animator avatar;
    float f_last_attacktime;
    public bool b_attacking;

    public static int normalDamage = 10;
    public int skillDamage = 30;

    public NormalTarget normalTarget;

    public void NormalAttack()
    {
        print("123");
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);

        foreach(Collider one in targetList)
        {
            SlimeHealth slime = one.GetComponent<SlimeHealth>();
            if (slime != null && !slime.isSinking)
            {
                //slime.TakeDamage(normalDamage, transform.position, 1f, 3f);
                StartCoroutine(slime.StartDamage(normalDamage, transform.position, 1f, 3f));
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        avatar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
                OnAttacking();
    }

    public void OnAttacking()
    {
        avatar.SetBool("Combo", true);
        //avatar.SetBool("StartAttack",true);
    }

    public void StopAttacking()
    {
        avatar.SetBool("Combo", false);
        //avatar.SetBool("StartAttack", false);
    }

    public void NormalAttackEvent()
    {
        NormalAttack();
    }


}
