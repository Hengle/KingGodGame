﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour {
    public static EnemyEffect instance = null;

    List<GameObject> hitcloneList = new List<GameObject>();
    List<GameObject> usedhitcloneList = new List<GameObject>();
    GameObject raphael;

    public IEnumerator PlayEffect(int stateNum)
    {
        usedhitcloneList.Add(hitcloneList[0]);
        hitcloneList[0].SetActive(true);
        hitcloneList[0].transform.position = transform.position + Vector3.up*0.7f;
        hitcloneList[0].transform.localScale = new Vector3(1f, 1f, 1f);
        hitcloneList[0].transform.LookAt(raphael.transform);
        hitcloneList[0].transform.Rotate(new Vector3(0, 0, Random.Range(-30,30)));

        if(stateNum == 2)
        {
            hitcloneList[0].transform.Rotate(new Vector3(0, 0, 180));
        }

        if(stateNum == 4)
        {
            hitcloneList[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        if(stateNum == 5)
        {
            hitcloneList[0].transform.localScale = new Vector3(2f, 2f, 2f);
        }

        hitcloneList.RemoveAt(0);

        yield return new WaitForSeconds(1f);

        hitcloneList.Add(usedhitcloneList[0]);
        usedhitcloneList[0].SetActive(false);
        usedhitcloneList.RemoveAt(0);

        yield break;
    }

    // Use this for initialization
    void Awake()
    {
        instance = this;
        raphael = GameObject.FindWithTag("Player");
        InitEffect();
    }

    public void InitEffect()
    {
        for (int i = 0; i < 5; ++i)
            hitcloneList.Add(SpawnEffect());
    }

    public GameObject SpawnEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/SlashHitEffect")) as GameObject;

        return effectClone;
    }
}