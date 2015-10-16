using UnityEngine;
using System.Collections;

public class TestEnemy : Enemy
{
     public GameObject attackCollider;

     private float distL, distR, stunTimer;
     private bool toLeft, isStunned;
     private Vector3 left, right, dir;

     // Use this for initialization
     void Start()
     {
          base.Start();
          stunTimer = 0;
          toLeft = true;
          isStunned = false;
          left = new Vector3(-0.5f, 0, 0);
          right = new Vector3(0.5f, 0, 0);
     }

     // Update is called once per frame
     void Update()
     {


          if (target != null)
          {
               distL = (transform.position - targetPos.position - left).magnitude;
               distR = (transform.position - targetPos.position - right).magnitude;
               toLeft = (0.5f + distL) <= distR;

               if (!isStunned)
               {
                    if (toLeft)
                         dir = (targetPos.position + left - transform.position).normalized;
                    else
                         dir = (targetPos.position + right - transform.position).normalized;

                    if (toLeft)
                         if (distL > 0.6f)
                              Move(dir, 3);
                         else
                              Attack();
                    else
                    {
                         if (distR > 0.6f)
                              Move(dir, 3);
                         else
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

          invTime -= Time.deltaTime;
     }

     private void Attack()
     {
          bool facing = distL <= distR;
          isStunned = true;
          stunTimer = 2f;
          GameObject attCol;
          if (facing)
          {
               attCol = Instantiate(attackCollider, transform.position + right * 2, transform.rotation) as GameObject;
          }
          else
          {
               attCol = Instantiate(attackCollider, transform.position + left * 2, transform.rotation) as GameObject;
          }
          Destroy(attCol, 0.5f);

     }

}
