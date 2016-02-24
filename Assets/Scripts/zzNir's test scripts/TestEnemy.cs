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
        targetRefresh = 10;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(targetRefresh > targetRefreshLimit)
        {
            //actor.MoveOrder(targetPos, true);
            //actor.setTarg(target);
            //actor.setZ(half.z);
            //targetRefresh = 0;
        }
        //targetRefresh += Time.deltaTime;
        
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
                            StartCoroutine(Attack());
                            //Attack();
                            Move(new Vector3(0, 0, 0), 0);
                        }
                    }
                }
                else
                {
                    Move(new Vector3(0, 0, 0), 0);
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

            if (invTime <= 0)
                isInvincible = false;
        }

        Move(vel, speed);
        animationController.isAttacking = isAttacking;

       // Debug.Log(moveController.isMoving);
       

        invTime -= Time.deltaTime;
        attack_CD += Time.deltaTime;
    }

    IEnumerator Attack()
    {
        float f = UnityEngine.Random.Range(40, 100) / 100.0f;
        attack_CD = -f;
        // Debug.Log(f);
        isAttacking = true;
        vel = Vector3.zero;
        yield return new WaitForSeconds(f);

        isAttacking = true;
        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;

    }

    private void spawnAttackCollider()
    {
        if (attackSound)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        if(attackShakesScreen)
        {
            camera.startScreenShake(.4f);
        }
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
