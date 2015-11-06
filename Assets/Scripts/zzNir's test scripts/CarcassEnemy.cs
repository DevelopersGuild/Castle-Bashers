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


        }

        if(TimeToLive <= 0)
        {
            //explosion animation
            Instantiate(Explosion, transform.position + offset, Explosion.transform.rotation);
            Destroy(gameObject);
        }

        attack_CD += Time.deltaTime;
        TimeToLive -= Time.deltaTime;
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

}
