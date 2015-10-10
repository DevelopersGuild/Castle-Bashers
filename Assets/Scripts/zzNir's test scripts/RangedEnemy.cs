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
        if (target != null)
        {
            if (!isStunned)
            {
                if (distL > attackRange && distR > attackRange)
                {
                    Act(classification);
                }
                else
                {
                    zDiff = targetPos.z - transform.position.z;
                    if(Math.Abs(zDiff) > 0.25f)
                    {
                        Move(new Vector3(0, 0, zDiff), speed);
                    }
                    else
                    {
                        dir = new Vector3(targetPos.x - transform.position.x, 0, 0);
                        if(attack_CD >= 4)
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

        attack_CD += Time.deltaTime;
        invTime -= Time.deltaTime;
    }

    private void Attack()
    {
        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;
        attack_CD = 0;

        isStunned = true;
        stunTimer = 1f;
        shot = Instantiate(shotObj, transform.position, transform.rotation) as Projectile;
        shot.Shoot(dir);
    }

}
