using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    protected Animator avatar;
    public float turnSpeed;
    private float turnSpeedTime;
    int turnDir;

    void Start()
    {
        avatar = GetComponent<Animator>();
        turnSpeedTime = turnSpeed * Time.deltaTime;
    }

    float h, v;
    float turnAngle = 15;
    
    void Update()
    {
        
        if (avatar)
        {
            avatar.SetFloat("Speed", (v * v));

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody /*&& !avatar.GetBool("StartAttack")*/) // ����ٰ� Ű�Է��� �δϱ� ���ݵ��� �ӵ��� �ȶ����� �׷��� ����������...
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    
                    if (Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack")) //Quaternion.LookRotation(new Vector3(-1, 0, 0))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
                            IsWalking();
                        }

                    }

                    if (Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, -1));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(0, 0, 1));
                        if (Vector3.Angle(transform.forward, new Vector3(0, 0, 1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(1, 0, 0));
                        if (Vector3.Angle(transform.forward, new Vector3(1, 0, 0)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(-1, 0, 0));
                        if (Vector3.Angle(transform.forward, new Vector3(-1, 0, 0)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                            IsWalking();
                        }
                    }

                    if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !avatar.GetBool("StartAttack"))
                    {
                        turnDir = Turnjudge(transform.forward, new Vector3(0, 0, -1));
                        if (Vector3.Angle(transform.forward, new Vector3(0, 0, -1)) > turnAngle)
                            transform.Rotate(new Vector3(0, turnDir * turnSpeedTime, 0), Space.World);
                        else
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
                            IsWalking();
                        }
                    }
                }

                else
                    StopWalking();

            }
        }

    }

    void IsWalking()
    {
        v = 1;
    }

    void StopWalking()
    {
        v = 0;
    }

    public int Turnjudge(Vector3 forward, Vector3 dir)
    {
        if (Vector3.Cross(forward, dir).y > 0)
            return 1;
        else
            return -1;
    }
}
