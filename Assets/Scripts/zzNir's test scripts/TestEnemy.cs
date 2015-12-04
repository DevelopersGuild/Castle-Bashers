using UnityEngine;
using System.Collections;
using System;

public class TestEnemy : Enemy
{
    public GameObject attackCollider;
    private GameObject attCol;
    public Type classification;

    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 4;
        attack_CD = 2;
        
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
                    Act(classification);
                    if (Math.Abs(zDiff) > half.z)
                    {
                        Move(new Vector3(0, 0, zDiff), speed);
                    }
                    else if (distL <= attackRange || distR <= attackRange)
                    {
                        if (attack_CD >= 2)
                            Attack();
                    }
                }
            }
            else
            {
                if (FindObjectOfType<Player>())
                    target = FindObjectOfType<Player>().gameObject;
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

            if (invTime <= 0)
                isInvincible = false;
        }


        animationController.isAttacking = isAttacking;

        invTime -= Time.deltaTime;
        attack_CD += Time.deltaTime;
    }

    private void Attack()
    {
        isAttacking = true;
        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;
    }

    private void spawnAttackCollider()
    {
        bool facing = distL <= distR;
        if (facing)
        {
            attCol = Instantiate(attackCollider, transform.position + xhalf + right, transform.rotation) as GameObject;
        }
        else
        {
            attCol = Instantiate(attackCollider, transform.position + (-1 * xhalf) + left, transform.rotation) as GameObject;
        }
        attack_CD = 0;
        Destroy(attCol, 0.5f);
    }

    private void FinishedAttacking()
    {
        isAttacking = false;
    }


}
