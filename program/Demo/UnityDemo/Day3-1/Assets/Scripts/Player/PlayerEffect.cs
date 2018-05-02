﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    public static PlayerEffect instance = null;
    public GameObject SpawnCloneList;

    List<GameObject> slashcloneList = new List<GameObject>();
    List<GameObject> usedslashcloneList = new List<GameObject>();

    List<GameObject> blackWaveEffectList = new List<GameObject>();
    List<GameObject> usedBlackWaveEffectList = new List<GameObject>();

    GameObject blinkclone;
    GameObject slashRingEffect;
    GameObject EXMoveSlashEffect;
    GameObject EXmoveVanishEffect;
    GameObject EXMoveVanishFlowerEffect;
    GameObject EXMoveRingEffectBack;
    GameObject ExMoveRingEffectFront;
    GameObject explosionAttackEffect;

    public GameObject Effect1Pos;
    public GameObject Effect2Pos;
    public GameObject Effect3Pos;
    public GameObject Effect4Pos;
    public GameObject blinkEffectPos;
    public GameObject EXMovePos;


    public IEnumerator PlayEffect(int stateNum)
    {
        //state = transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);    
        // 이거 first 어택은 idle 다음에 읽히지 않고                                    
        // second는 아예안읽힘   state.IsName("Base Layer.attack_second")
        // 왜인지 모르겠음;;;;;;

        //slashRingEffect.SetActive(true);
        //slashRingEffect.transform.position =
        //    new Vector3(transform.position.x, slashRingEffect.transform.position.y, transform.position.z);

        //slashRingEffect.GetComponent<ParticleSystem>().Play();
        if (GetComponent<PlayerAttack>().b_attacking)
        {
            usedslashcloneList.Add(slashcloneList[0]);
            slashcloneList[0].SetActive(true);
            slashcloneList[0].transform.localScale = new Vector3(1f, 1f, 1f);

            if (stateNum == 1)
            {
                slashcloneList[0].transform.position = Effect1Pos.transform.position;
                slashcloneList[0].transform.rotation =
                    Quaternion.Euler(-90, 180 + transform.rotation.eulerAngles.y, 205);
            }

            else if (stateNum == 2)
            {
                slashcloneList[0].transform.position = Effect2Pos.transform.position;
                slashcloneList[0].transform.rotation =
                    Quaternion.Euler(-110 - 180, 270 + transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 3)
            {
                slashcloneList[0].transform.position = Effect3Pos.transform.position;                
                slashcloneList[0].transform.rotation =
                    Quaternion.Euler(101 - 180, 270 + transform.rotation.eulerAngles.y, 110);
            }

            else if (stateNum == 4)
            {
                slashcloneList[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                slashcloneList[0].transform.position = Effect4Pos.transform.position;
                slashcloneList[0].transform.rotation =
                    Quaternion.Euler(-90, 180 + transform.rotation.eulerAngles.y, 205);
            }

            slashcloneList.RemoveAt(0);


            yield return new WaitForSeconds(1f);

            slashcloneList.Add(usedslashcloneList[0]);
            usedslashcloneList[0].SetActive(false);
            usedslashcloneList.RemoveAt(0);
        }

        yield break;
    }

    public void PlayBlackWaveEffect(int stateNum)
    {
        if (GetComponent<PlayerAttack>().b_attacking)
        {
            usedBlackWaveEffectList.Add(blackWaveEffectList[0]);
            blackWaveEffectList[0].SetActive(true);
            blackWaveEffectList[0].transform.localScale = new Vector3(1f, 1f, 1f);

            if (stateNum == 1)
            {
                blackWaveEffectList[0].transform.position = Effect1Pos.transform.position - transform.forward * 1.5f;
                
                blackWaveEffectList[0].transform.rotation =
                    Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }

            else if (stateNum == 2)
            {
                blackWaveEffectList[0].transform.position = Effect2Pos.transform.position - transform.forward * 1.5f;
                blackWaveEffectList[0].transform.rotation =
                    Quaternion.Euler(0, transform.rotation.eulerAngles.y, -20);
            }

            else if (stateNum == 3)
            {
                blackWaveEffectList[0].transform.position = Effect3Pos.transform.position - transform.forward * 1.5f;
                blackWaveEffectList[0].transform.rotation =
                    Quaternion.Euler(0, transform.rotation.eulerAngles.y, 20);
            }

            else if (stateNum == 4)
            {
                blackWaveEffectList[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                blackWaveEffectList[0].transform.position = Effect4Pos.transform.position - transform.forward * 1.5f;
                blackWaveEffectList[0].transform.rotation =
                    Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            blackWaveEffectList[0].GetComponent<Rigidbody>().AddForce(transform.forward * 700);
            blackWaveEffectList.RemoveAt(0);
        }
    }

    public void BlackWaveEffectVanising(GameObject effect)
    {
        effect.GetComponent<Rigidbody>().Sleep();
        effect.SetActive(false);
        blackWaveEffectList.Add(effect);
        usedBlackWaveEffectList.Remove(effect);
    }

    public void playBlinkEffect()
    {
        if (transform.CompareTag("Player"))
        {
            blinkclone.SetActive(true);
            blinkclone.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playEXMoveSlashEffect()
    {
        Vector3 EXRotation = EXMovePos.GetComponent<SkillTarget>().movePos.normalized;

        if (transform.CompareTag("Player"))
        {
            EXMoveSlashEffect.SetActive(true);
            EXMoveSlashEffect.transform.position = EXMovePos.transform.position + EXRotation * 0.5f;
            EXMoveSlashEffect.transform.rotation = Quaternion.LookRotation(EXRotation);
            EXMoveSlashEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playEXMoveVanishEffect()
    {
        if (transform.CompareTag("Player"))
        {
            EXmoveVanishEffect.SetActive(true);
            EXmoveVanishEffect.transform.position = transform.position;
            EXmoveVanishEffect.transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
            EXmoveVanishEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playEXmoveVanishFlowerEffect()
    {
        if (transform.CompareTag("Player"))
        {
            EXMoveVanishFlowerEffect.SetActive(true);
            EXMoveVanishFlowerEffect.transform.position = transform.position + Vector3.up;
            EXMoveVanishFlowerEffect.transform.rotation = Quaternion.Euler(-142f, transform.rotation.eulerAngles.y, 0);
            EXMoveVanishFlowerEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playExMoveRingEffectFront()
    {
        if (transform.CompareTag("Player"))
        {
            ExMoveRingEffectFront.SetActive(true);
            ExMoveRingEffectFront.transform.position = transform.position;
            ExMoveRingEffectFront.transform.rotation = Quaternion.Euler(0, EXMovePos.transform.rotation.eulerAngles.y, 0);
            ExMoveRingEffectFront.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playExMoveRingEffectBack()
    {
        if (transform.CompareTag("Player"))
        {
            EXMoveRingEffectBack.SetActive(true);
            EXMoveRingEffectBack.transform.position = transform.position;
            EXMoveRingEffectBack.transform.rotation = Quaternion.Euler(0, EXMovePos.transform.rotation.eulerAngles.y, 0);
            EXMoveRingEffectBack.GetComponent<ParticleSystem>().Play();
        }
    }

    public void playExplosionAttackEffect()
    {
        if (transform.CompareTag("Player"))
        {
            explosionAttackEffect.SetActive(true);
            explosionAttackEffect.transform.position = transform.position;
            explosionAttackEffect.transform.rotation = Quaternion.Euler(0, EXMovePos.transform.rotation.eulerAngles.y, 0);
            explosionAttackEffect.GetComponent<ParticleSystem>().Play();
        }
    }

    void Awake() {
        if (transform.CompareTag("Player"))
        {
            instance = this;
            blinkclone = Instantiate(Resources.Load("Prefabs/Effect/BlinkEffect"), blinkEffectPos.transform) as GameObject;
            EXmoveVanishEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveVanishEffect"), SpawnCloneList.transform) as GameObject;
            EXMoveVanishFlowerEffect = Instantiate(Resources.Load("Prefabs/Effect/VanishFlowerEffect"), SpawnCloneList.transform) as GameObject;
            EXMoveRingEffectBack = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectBack"), SpawnCloneList.transform) as GameObject;
            ExMoveRingEffectFront = Instantiate(Resources.Load("Prefabs/Effect/EXMoveRingEffectFront"), SpawnCloneList.transform) as GameObject;
            EXMoveSlashEffect = Instantiate(Resources.Load("Prefabs/Effect/EXmoveSlashEffectSimple"), SpawnCloneList.transform) as GameObject;
            explosionAttackEffect = Instantiate(Resources.Load("Prefabs/Effect/ExplosionAttackEffect"), SpawnCloneList.transform) as GameObject;
            
            InitEffect();

            InitBlackWaveEffect();
        }
    }

    public void InitEffect()
    {
        for (int i = 0; i < 10; ++i)
            slashcloneList.Add(SpawnEffect());
    }

    public void InitBlackWaveEffect()
    {
        for(int i = 0; i < 10; ++i)
            blackWaveEffectList.Add(SpawnBlackWaveEffect());
    }

    public GameObject SpawnEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/slash1"), SpawnCloneList.transform) as GameObject;

        return effectClone;
    }

    public GameObject SpawnBlackWaveEffect()
    {
        GameObject effectClone = Instantiate(Resources.Load("Prefabs/Effect/BlackWave"), SpawnCloneList.transform) as GameObject;

        return effectClone;
    }
}
