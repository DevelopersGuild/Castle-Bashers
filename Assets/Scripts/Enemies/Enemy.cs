using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{

    public float agroRange;
    public float attackRange;
    public float attack_CD;
    public bool attackShakesScreen;
    public int experienceAmount;
    public AudioClip attackSound;

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public Vector3 targetPos, dir, gravity, xhalf, half, vel;
    [HideInInspector]
    public MoveController moveController;
    [HideInInspector]
    public bool isInvincible, isStunned, freeFall;
    [HideInInspector]
    public float invTime, stunTimer;
    [HideInInspector]
    public enum Type { Melee, Ranged, Other };
    [HideInInspector]
    public float distance, speed, zDiff;
    protected bool isAttacking;
    protected float attackTimer;

    //for melee
    [HideInInspector]
    public float distL, distR, targetRefresh, targetRefreshLimit;
    [HideInInspector]
    public bool toLeft;
    [HideInInspector]
    public Vector3 left, right;
    [HideInInspector]
    public SpriteRenderer sprRend;
    [HideInInspector]
    public Health hp;
    public AnimationController animationController;
    protected CameraFollow cameraShake;

    public Type classification;
    private float velocityXSmoothing, velocityZSmoothing;

    [HideInInspector]
    public Actor actor;

    // Use this for initialization
    public void Start()
    {
        //later on make it only target living players, priority on tanks
        target = null;
        moveController = GetComponent<MoveController>();
        sprRend = GetComponent<SpriteRenderer>();
        hp = GetComponent<Health>();
        animationController = GetComponent<AnimationController>();
        cameraShake = FindObjectOfType<CameraFollow>();
        isInvincible = false;
        invTime = 0;
        stunTimer = 0;
        speed = 1;

        targetRefresh = 0;
        targetRefreshLimit = 0.25f;

        distL = distR = 50;
        toLeft = true;
        isStunned = false;
        left = new Vector3(-attackRange, 0, 0);
        right = new Vector3(attackRange, 0, 0);

        gravity = new Vector3(0, -1, 0);

        half = GetComponent<BoxCollider>().size / 2;
        xhalf = new Vector3(half.x, 0, 0);

        attackRange += half.x;
        zDiff = 0;

        vel = gravity;

        actor = GetComponent<Actor>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerManager>().getUpPlayer().gameObject;
            actor.MoveOrder(targetPos);
            targetPos = target.transform.position;
        }
        if (!moveController.collisions.below)
        {
            gravity.y += -0.1f;
            //Move(gravity, Math.Abs(gravity.y));
            freeFall = true;
        }
        else
        {
            gravity.y = -1;
            freeFall = false;
        }

        if (invTime <= 0)
        {
            isInvincible = false;
        }

        invTime -= Time.deltaTime;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject tar)
    {
        target = tar;
    }

    public bool GetInvincible()
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
        velocity.y = gravity.y;

        velocity = velocity.normalized;
        //velocity.x = Mathf.SmoothDamp(velocity.x, 6, ref velocityXSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
        //velocity.z = Mathf.SmoothDamp(velocity.z, 10, ref velocityZSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
        moveController.Move(velocity * Time.deltaTime * force);

    }


    //Next time I work on a game, it should use units for areas in the game and nice values for movements, too late now though
    //Everything would work a lot better if the ground was split into small squares that can be moved onto smoothly or something. Would help for pathfinding a lot too
    public virtual void Act(Type t)
    {
        if (!freeFall)
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

                    if (distL > attackRange && distR > attackRange)
                        Move(dir, speed);

                }
            }
            else if (t == Type.Ranged)
            {
                distance = targetPos.x - transform.position.x;
                if (Math.Abs(distance) > attackRange)
                {
                    //Move(new Vector3(distance, 0, 0), 1);
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



}

