using UnityEngine;
using System.Collections;
using System;

public class RangedEnemy : Enemy
{
    public Projectile shotObj;
    private Projectile shot;


    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 3;
        attack_CD = 3;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (!freeFall)
        {
            if (target != null)
            {
                targetPos = target.transform.position;
                if (!isStunned)
                {
                    if (distL > attackRange && distR > attackRange)
                    {
                        Act(classification);
                    }
                    else
                    {
                        zDiff = targetPos.z - transform.position.z;
                        if (Math.Abs(targetPos.x - transform.position.x) > 0.3)
                        {
                            moveController.OrientFacingLeft((targetPos.x - transform.position.x) < 0, moveController.GetFacing());
                        }
                        if (Math.Abs(zDiff) > 0.25f)
                        {
                            vel = new Vector3(0, 0, zDiff);
                            //Move(new Vector3(0, 0, zDiff), speed);
                        }
                        else
                        {
                            if (attack_CD >= 4)
                                Attack();
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

        Move(vel, speed);
        animationController.isAttacking = isAttacking;
        attack_CD += Time.deltaTime;
       
    }

    private void Attack()
    {
        isAttacking = true;
        isStunned = true;
        stunTimer = 1f;
        attack_CD = 0;
    }

    private void spawnProjectile()
    {

        if (attackSound)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }

        dir = new Vector3(targetPos.x - transform.position.x, 0, 0);
        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;
        bool facing = distL <= distR;
        short faceDir;
        if (facing)
            faceDir = -1;
        else
            faceDir = 1;

 
        shot = Instantiate(shotObj, transform.position + faceDir * xhalf, transform.rotation) as Projectile;
        shot.Shoot(dir.normalized);
    }

    private void FinishedAttack()
    {
        isAttacking = false;
    }

}

