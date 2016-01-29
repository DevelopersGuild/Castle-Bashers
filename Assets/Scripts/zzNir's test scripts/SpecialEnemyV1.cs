using UnityEngine;
using System.Collections;
using System;

public class SpecialEnemyV1 : Enemy
{
    public GameObject attackCollider;
    private GameObject attCol;
    private float dash_CD, dashRange, dashTime;
    private bool isDashing;

    private float zDiff;

    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 3;
        zDiff = 0;
        attack_CD = 2;
        dash_CD = 8;
        dashRange = 8;
        dashTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (!freeFall)
        {
            if (target != null)
            {
                if (!isStunned)
                {
                    zDiff = targetPos.z - transform.position.z;
                    if (!isDashing)
                    {
                        Act(classification);
                    }
                    else
                    {
                        if (dashTime > 0)
                        {
                            Move(new Vector3(dir.x, 0, 0), 40);
                            dashTime -= Time.deltaTime;
                        }
                        else
                        {
                            GetComponent<DealDamageToPlayer>().enabled = false;
                            isDashing = false;
                        }
                    }

                }
            }
            else
            {
                if (target.GetComponent<Player>().getDown())
                    target = FindObjectOfType<PlayerManager>().getUpPlayer().gameObject;
                else
                {
                    //player lost
                    //Destroy(gameObject);
                }
            }
            if (stunTimer > 0)
                stunTimer -= Time.deltaTime;
            else
                isStunned = false;

        }
        attack_CD += Time.deltaTime;
        dash_CD += Time.deltaTime;

    }

    private void Attack()
    {
        bool facing = distL <= distR;
        attack_CD = 0;
        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;

        if (facing)
        {
            attCol = Instantiate(attackCollider, transform.position + xhalf + right, transform.rotation) as GameObject;
        }
        else
        {
            attCol = Instantiate(attackCollider, transform.position + (-1 * xhalf) + left, transform.rotation) as GameObject;
        }
        Destroy(attCol, 0.5f);

    }

    private void Dash()
    {
        isDashing = true;
        dashTime = 0.4f;
        dash_CD = 0;
        dir = new Vector3(targetPos.x - transform.position.x, 0, 0);
        GetComponent<DealDamageToPlayer>().enabled = true;
    }

    public override void Act(Type t)
    {
        targetPos = target.transform.position;
        distance = (transform.position - targetPos).magnitude;

        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;

        
        if (dash_CD > 8)
        {
            distance = targetPos.x - transform.position.x;
            zDiff = targetPos.z - transform.position.z;
            if (Math.Abs(distance) <= dashRange)
            {
                if (Math.Abs(zDiff) > half.z)
                {
                    Move(new Vector3(0, 0, zDiff * 1.2f), speed);
                }
                else
                {
                    Dash();
                }
            }
            else
            {
                Move(new Vector3(distance, 0, 0), 2);
            }
        }
        else
        {
            if (Math.Abs(zDiff) > half.z * 2)
            {
                Move(new Vector3(0, 0, zDiff), speed);
            }
            else if (distL <= attackRange || distR <= attackRange)
            {
                if (attack_CD >= 2)
                    Attack();
            }
            else if (distance > agroRange)
            {
                Move(new Vector3(targetPos.x - transform.position.x, 0, 0), 1.5f);
            }
            else
            {
                if (toLeft)
                    dir = (targetPos + left - transform.position);
                else
                    dir = (targetPos + right - transform.position);

                if (distL > attackRange && distR > attackRange)
                    Move(dir, speed);

            }
        }
    }

}
