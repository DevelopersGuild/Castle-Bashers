using UnityEngine;
using System.Collections;
using System;

public class RangedEnemy : Enemy
{
    public Projectile shotObj;
    private Projectile shot;
    public Type classification;

    private float zDiff;

    // Use this for initialization
    void Start()
    {
        base.Start();
        speed = 3;
        zDiff = 0;
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
                        //Debug.Log(targetPos.x - transform.position.x);
                        if (Math.Abs(targetPos.x - transform.position.x) > 0.3)
                        {
                            moveController.OrientFacingLeft((targetPos.x - transform.position.x) < 0, moveController.GetFacing());
                        }
                        if (Math.Abs(zDiff) > 0.25f)
                        {
                            Move(new Vector3(0, 0, zDiff), speed);
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
        attack_CD += Time.deltaTime;
        invTime -= Time.deltaTime;
    }

    private void Attack()
    {
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

            attack_CD = 0;

        isStunned = true;
        stunTimer = 1f;
        shot = Instantiate(shotObj, transform.position + faceDir * half, transform.rotation) as Projectile;
        shot.Shoot(dir.normalized);
    }

}
