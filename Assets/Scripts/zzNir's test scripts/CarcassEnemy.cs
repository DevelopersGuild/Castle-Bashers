using UnityEngine;
using System.Collections;
using System;

public class CarcassEnemy : Enemy
{
    public GameObject attackCollider;
    private GameObject attCol;
    public float TimeToLive;
    public GameObject Explosion;

    private Vector3 offset = new Vector3(0, -1.1f, 0);

    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 3;

    }

    void Update()
    {

        base.Update();

        if (targetRefresh > targetRefreshLimit)
        {
            if (getCanMove())
            {
                actor.setMove(false);
            }
            else
            {
                actor.setMove(true);
            }
            actor.MoveOrder(targetPos, true);
            actor.setTarg(target);
            actor.setZ(half.z);
            targetRefresh = 0;
        }
        targetRefresh += Time.deltaTime;

        if (!freeFall)
        {
            if (target != null)
            {
                if (!isStunned && !isAttacking)
                {
                    zDiff = targetPos.z - transform.position.z;
                    Act(classification);
                    if (Math.Abs(zDiff) > half.z)
                    {
                        vel = new Vector3(0, 0, zDiff);
                        // Move(new Vector3(0, 0, zDiff), speed);
                    }
                    else if (distL <= attackRange || distR <= attackRange)
                    {
                        if (attack_CD >= 2)
                        {
                            //StartCoroutine(Attack());
                            //Attack();
                            //Move(new Vector3(0, 0, 0), 0);
                        }
                    }
                }
                else
                {
                    // Move(new Vector3(0, 0, 0), 0);
                }
                if (target.GetComponent<Player>().getDown())
                {
                    if (FindObjectOfType<PlayerManager>().getUpPlayer() != null)
                        target = FindObjectOfType<PlayerManager>().getUpPlayer().gameObject;
                    else
                        Destroy(gameObject);

                }
            }
            else
            {
                if (FindObjectOfType<PlayerManager>().getUpPlayer() != null)
                    target = FindObjectOfType<PlayerManager>().getUpPlayer().gameObject;
                else
                    Destroy(gameObject);
            }

            if (stunTimer > 0)
                stunTimer -= Time.deltaTime;
            else
                isStunned = false;

            if (invTime <= 0)
                isInvincible = false;
        }
        else
        {
            if (moveController)
                Move(vel, speed);
        }

        if (TimeToLive <= 0 || (target.transform.position - transform.position).magnitude < 3f)
        {
            //explosion animation
            Instantiate(Explosion, transform.position + offset, Explosion.transform.rotation);
            Destroy(gameObject);
        }

        // Debug.Log(moveController.isMoving);


        invTime -= Time.deltaTime;
        attack_CD += Time.deltaTime;
        TimeToLive -= Time.deltaTime;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    base.Update();
    //    if (!freeFall)
    //    {
    //        if (target != null)
    //        {


    //            if (!isStunned)
    //            {
    //                zDiff = targetPos.z - transform.position.z;
    //                Act(classification);
    //                if (Math.Abs(zDiff) > half.z)
    //                {
    //                    Move(new Vector3(0, 0, zDiff), speed);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (target.GetComponent<Player>().getDown())
    //                target = FindObjectOfType<PlayerManager>().getUpPlayer().gameObject;
    //            else
    //            {
    //                //player lost
    //                //Destroy(gameObject);
    //            }
    //        }
    //        if (stunTimer > 0)
    //            stunTimer -= Time.deltaTime;
    //        else
    //            isStunned = false;


    //    }

    //    if (TimeToLive <= 0)
    //    {
    //        //explosion animation
    //        Instantiate(Explosion, transform.position + offset, Explosion.transform.rotation);
    //        Destroy(gameObject);
    //    }

    //    attack_CD += Time.deltaTime;
    //    TimeToLive -= Time.deltaTime;
    //}

    private void Attack()
    {
        //Does not attack, only moves
        //Deals damage on touch though


        //bool facing = distL <= distR;
        //attack_CD = 0;
        //distL = (transform.position - targetPos - left).magnitude;
        //distR = (transform.position - targetPos - right).magnitude;
        //toLeft = (attackRange + distL) <= distR;

        //if (facing)
        //{
        //    attCol = Instantiate(attackCollider, transform.position + xhalf + right, transform.rotation) as GameObject;
        //}
        //else
        //{
        //    attCol = Instantiate(attackCollider, transform.position + (-1 * xhalf) + left, transform.rotation) as GameObject;
        //}
        //Destroy(attCol, 0.5f);

    }



}
