using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : SkillOld
{
     private Vector3 offset = new Vector3(1,0,0);
     private List<GameObject> enemies = new List<GameObject>();
     private float distance, tempDist;
     private bool somethingToAttack;
     private Vector3 dir;

     public Projectile shotObj;
     private Projectile shot;

     private float shot_CD;
     //private PlayerHealth health;

     // Use this for initialization
     void Start()
     {
          //health = GetComponent<PlayerHealth>();
          Destroy(gameObject, 15);
          shot_CD = 2;
          distance = 10;
          somethingToAttack = false;
     }

     // Update is called once per frame
     void Update()
     {
          foreach (GameObject enem in enemies)
          {
               tempDist = (transform.position - enem.transform.position).magnitude;
               if (tempDist < distance)
               {
                    distance = tempDist;
                    dir = (transform.position - enem.transform.position).normalized;
               }
          }
          if (enemies.Count < 1)
          {
               somethingToAttack = false;
          }
          else
          {
               somethingToAttack = true;
          }

          if(somethingToAttack)
          {
               if(shot_CD >= 2)
               {
                    shot = Instantiate(shotObj, transform.position, transform.rotation) as Projectile;
                    shot.Shoot(-dir);
                    shot_CD = 0;
               }
          }

          shot_CD += Time.deltaTime;
     }

     public override void SetDir(float dir)
     {
          Instantiate(skill1, transform.position + dir * new Vector3(1f, 0, 0), transform.rotation);
          Destroy(gameObject);
     }

     public override Vector3 getOffset()
     {
          return offset;
     }

     public void addEnemy(GameObject enem)
     {
          if(!enemies.Contains(enem))
          enemies.Add(enem);
     }

     public void removeEnemy(GameObject enem)
     {
          enemies.Remove(enem);
     }

     public override void Init(float facing)
     {
          transform.position += facing * offset;
     }


}