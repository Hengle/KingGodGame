﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject normalAttacCam;
    public GameObject exMoveCam;
    protected Animator avatar;
    public bool b_attacking;

    public static int normalDamage = 10;
    public int skillDamage = 30;

    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(normalTarget.targetList);
        List<Collider> realTargetList = new List<Collider>();

        if (normalTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (normalTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<SlimeHealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                SlimeHealth slime = one.GetComponent<SlimeHealth>();
                if (slime != null && !slime.isSinking)
                {
                    StartCoroutine(slime.StartDamage(normalDamage, transform.position, 0.3f, 3f, stateNum));
                }
            }

        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {
                StartCoroutine(normalAttacCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                SlimeHealth slime = one.GetComponent<SlimeHealth>();
                if (slime != null && !slime.isSinking)
                {
                    StartCoroutine(slime.StartDamage(normalDamage, transform.position, 0.3f, 3f, stateNum));
                }
            }
        }
    }

    public void skillAttack(int stateNum)
    {
        List<Collider> targetList = new List<Collider>(skillTarget.targetList);
        List<Collider> realTargetList = new List<Collider>();

        if (skillTarget.anotherTargetList.Count > 0)
        {
            foreach (Collider one in targetList)
            {
                if (skillTarget.anotherTargetList.Count > 0)
                {
                    if (!one.GetComponent<SlimeHealth>().CheckBtwRapObj())
                    {
                        realTargetList.Add(one);
                    }
                }
            }

            if (realTargetList.Count > 0)
            {
                StartCoroutine(exMoveCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in realTargetList)
            {
                SlimeHealth slime = one.GetComponent<SlimeHealth>();
                if (slime != null && !slime.isSinking)
                {
                    StartCoroutine(slime.StartSkillDamage(normalDamage, transform.position, 0.3f, 3f, stateNum));
                }
            }

        }   //장애물이 있을경우 장애물 뒤에있는 놈들은 리스트에서 제거하는 조건문.

        else
        {
            if (targetList.Count > 0)
            {
                StartCoroutine(exMoveCam.GetComponent<NoiseCameraEvent>().cameraHitEvent(targetList.Count * 0.02f));
            }

            foreach (Collider one in targetList)
            {
                SlimeHealth slime = one.GetComponent<SlimeHealth>();
                if (slime != null && !slime.isSinking)
                {
                    StartCoroutine(slime.StartSkillDamage(normalDamage, transform.position, 0.3f, 3f, stateNum));
                }
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
        //if (transform.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.J))
            {
                OnAttacking();
            }
        }
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

    public void NormalAttackEvent(int stateNum)
    {
        if (transform.CompareTag("Player") || b_attacking)
        {
            StartCoroutine(transform.GetComponent<PlayerEffect>().PlayEffect(stateNum));
            NormalAttack(stateNum);
        }
    }
}
