using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    protected Animator avatar;
    public float turnSpeed;
    public float moveSpeed = 1;
    private float turnSpeedTime;

    Vector3 movePos;
    Rigidbody rigidbody;

    int turnDir;
    float turnAngle = 15;

    Vector3 eulerAngleVelocity = new Vector3(0, 100, 0);


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //movePos = movePos.normalized * 2f * Time.deltaTime;       

        if (avatar)
        {
            if (rigidbody /*&& !avatar.GetBool("StartAttack")*/) // 여기다가 키입력을 두니까 공격도중 속도가 안떨어짐 그래서 일일히넣음...
            {
                rigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime * avatar.GetFloat("DashForce"));

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {

                    if (Input.GetKey(KeyCode.A)) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
                        movePos.Set(-1, 0, 1);

                    if (Input.GetKey(KeyCode.D))
                        movePos.Set(1, 0, -1);

                    if(Input.GetKey(KeyCode.W))
                        movePos.Set(1, 0, 1);

                    if(Input.GetKey(KeyCode.S))
                        movePos.Set(-1, 0, -1);

                    if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                        movePos.Set(0, 0, 1);

                    if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                        movePos.Set(1, 0, 0);

                    if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                        movePos.Set(-1, 0, 0);

                    if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                        movePos.Set(0, 0, -1);

                    if (!avatar.GetBool("Combo")) // 공격중이 아니며 키를 누르고 있을때.
                    {
                        Dash(movePos);
                    }
                }
                else
                    StopWalking();
            }
        }

    }

    void Dash(Vector3 movePos)
    {
        Vector3 movePos1 = movePos.normalized * moveSpeed * Time.deltaTime;

        turnDir = Turnjudge(transform.forward, movePos.normalized);
        if (Vector3.Angle(transform.forward, movePos.normalized) > turnAngle)
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, turnDir * turnSpeedTime * 100, 0) * Time.deltaTime));

        else
        {
            rigidbody.rotation = Quaternion.LookRotation(movePos.normalized);
            IsWalking();
        }
    }

    void IsWalking()
    {
        avatar.SetBool("Dash", true);
    }

    void StopWalking()
    {
        avatar.SetBool("Dash", false);
    }

    public void DashEndForce()
    {
        //rigidbody.AddForce(transform.forward.normalized * 400);
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
