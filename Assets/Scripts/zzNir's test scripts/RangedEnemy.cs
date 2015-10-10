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
        speed = 2;
        zDiff = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!isStunned)
            {
                if (distL > attackRange || distR > attackRange)
                {
                    Act(classification);
                }
                else
                {
                    zDiff = targetPos.z - transform.position.z;
                    if(Math.Abs(zDiff) > 0.5f)
                    {
                        Move(new Vector3(0, 0, zDiff), speed);
                    }
                    else
                    {
                        dir = new Vector3(targetPos.x - transform.position.x, 0, 0);
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

        invTime -= Time.deltaTime;
    }

    private void Attack()
    {
        isStunned = true;
        stunTimer = 1f;
        shot = Instantiate(shotObj, transform.position, transform.rotation) as Projectile;
        shot.Shoot(dir);
    }

}
