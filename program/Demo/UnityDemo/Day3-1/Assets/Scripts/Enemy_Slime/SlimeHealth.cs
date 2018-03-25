﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;

    public float flashSpeed = 5f; // 맞았을때 번쩍이는 시간
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f); // 맞았을때 테투리가 빨강.

    public float sinkSpeed = 1f;

    private Material slimeMat;
    private Animator anim;
    private NavMeshAgent slimenavMesh;
    private Rigidbody slimeRigidBody;

    bool isDead = false;
    public bool isSinking = false;
    bool damaged = false;
    bool isEXMove = false;

    public void stopMoving()
    {
        slimeRigidBody.Sleep();
        slimenavMesh.speed = 0;
    }

    //public void PushBack(Vector3 playerPosition, float delay, float pushBack)
    //{
    //    Vector3 diff = playerPosition - transform.position;
    //    slimerigidBody.AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 5000f * pushBack);
    //    Invoke("StopMoving", .08f);
    //}

    //public void TakeDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
    //{
    //    damaged = true;
    //    PushBack(playerPosition, delay, pushBack);
    //    currentHealth -= damage;
    //    //print(currentHealth);

    //    if (currentHealth <= 0 && !isDead)
    //    {
    //        Death();
    //    }
    //}

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public IEnumerator StartSkillDamage(int damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        damaged = true;
        isEXMove = true;
        slimeMat.SetColor("_Color", new Vector4(.3f,.3f,.3f,1));
        slimenavMesh.speed = 0;
        anim.speed = 0;
        Vector3 diff = playerPosition - transform.position;

        yield return new WaitForSeconds(0.4f);
        damaged = false;
        isEXMove = false;

        yield return new WaitForSeconds(.2f);
        TakeDamage(damage);
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 700f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(stateNum));

        yield return new WaitForSeconds(.2f);
        slimeRigidBody.Sleep();

        yield return new WaitForSeconds(delay);
        if (!isSinking)
        {
            slimenavMesh.speed = 1;
            anim.speed = 1;
        }

        yield break;
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack, int stateNum)
    {
        slimeMat.SetColor("_Color", Color.black);
        slimenavMesh.speed = 0;
        anim.speed = 0;
        TakeDamage(damage);
        Vector3 diff = playerPosition - transform.position;
        GetComponent<Rigidbody>().AddForce((-new Vector3(diff.x, 0f, diff.z)).normalized * 400f * pushBack);
        StartCoroutine(GetComponent<EnemyEffect>().PlayEffect(stateNum));

        //while (timef + 0.5f > Time.time)
        //{
        //    yield return new WaitForSeconds(0.05f);
        //}

        yield return new WaitForSeconds(.1f);
        slimeRigidBody.Sleep();
        damaged = false;
        //Time.timeScale = 1;

        yield return new WaitForSeconds(delay);
        if (!isSinking)
        {
            slimenavMesh.speed = 1;
            anim.speed = 1;
        }
        yield break;

    }

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        slimenavMesh = GetComponent<NavMeshAgent>();
        slimeRigidBody = GetComponent<Rigidbody>();
        currentHealth = startingHealth;
        slimeMat = transform.GetChild(0).GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (!damaged)
        {
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white, flashSpeed * Time.deltaTime));
        }

        //if (!isEXMove)
        //{
        //    slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white, flashSpeed * Time.deltaTime));
        //}

        if (isEXMove)
        {
            slimeRigidBody.Sleep();
        }

        if (isSinking)            
            slimeMat.SetColor("_Color", Color.Lerp(slimeMat.GetColor("_Color"), Color.white * 40, .6f * Time.deltaTime));
    }

    void Death()
    {
        isDead = true;
      
        slimeMat.SetColor("_Color", Color.black * 0.2f);
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        Destroy(gameObject, .8f);
    }
}
