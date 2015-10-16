using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{

    public float agroRange;
    public float attackRange;
    public float attack_CD;

    [HideInInspector] 
    public GameObject target;
    [HideInInspector]
    public Vector3 targetPos, dir;
    private MoveController moveController;
    [HideInInspector]
    public bool isInvincible, isStunned;
    [HideInInspector]
    public float invTime, stunTimer;
    [HideInInspector]
    public enum Type { Melee, Ranged, Other };
    [HideInInspector]
    public float distance, speed;

    //for melee
    [HideInInspector]
    public float distL, distR;
    [HideInInspector]
    public bool toLeft;
    [HideInInspector]
    public Vector3 left, right;


    private float velocityXSmoothing, velocityZSmoothing;

    // Use this for initialization
    public void Start()
    {
        //later on make it only target living players, priority on tanks
        target = FindObjectOfType<Player>().gameObject;
        targetPos = target.transform.position;
        moveController = GetComponent<MoveController>();
        isInvincible = false;
        invTime = 0;
        stunTimer = 0;
        speed = 1;

        distL = distR = 50;
        toLeft = true;
        isStunned = false;
        left = new Vector3(-attackRange, 0, 0);
        right = new Vector3(attackRange, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    public bool getInvincible()
    {
        return isInvincible;
    }

    public void setInvTime(float t)
    {
        invTime = t;
        isInvincible = true;
    }

    public void Move(Vector3 velocity, float force = 1)
    {
        velocity.y = 0f;
        velocity = velocity.normalized;
        //velocity.x = Mathf.SmoothDamp(velocity.x, 6, ref velocityXSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
        //velocity.z = Mathf.SmoothDamp(velocity.z, 10, ref velocityZSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
        moveController.Move(velocity * Time.deltaTime * force);
    }

    public virtual void Act(Type t)
    {
        targetPos = target.transform.position;
        distance = (transform.position - targetPos).magnitude;

        distL = (transform.position - targetPos - left).magnitude;
        distR = (transform.position - targetPos - right).magnitude;
        toLeft = (attackRange + distL) <= distR;

        if (t == Type.Melee)
        {

            if (distance > agroRange)
            {
                Move(new Vector3(targetPos.x - transform.position.x, 0, 0), 1.5f);
            }
            else
            {
                if (toLeft)
                    dir = (targetPos + left - transform.position);
                else
                    dir = (targetPos + right - transform.position);

                if(distL > attackRange && distR > attackRange)
                Move(dir, speed);

            }
        }
        else if (t == Type.Ranged)
        {
            distance = targetPos.x - transform.position.x;
            if(Math.Abs(distance) > attackRange)
            {
                Move(new Vector3(distance, 0, 0), 1);
            }
            else
            {
                Move(targetPos - transform.position, speed);
            }
        }
        else if (t == Type.Other)
        {

        }
        else
        {
            Debug.LogError("Incorrect type of enemy, no movement possible");
        }
    }
}

