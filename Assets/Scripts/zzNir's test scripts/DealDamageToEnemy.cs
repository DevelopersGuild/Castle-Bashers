using UnityEngine;
using System.Collections;

public class DealDamageToEnemy : MonoBehaviour
{

     private float dmgAmount = 1;
     public float invTime = 0.1f;
     public bool knockOverride = false;
     // Use this for initialization
     void Start()
     {

          //GetComponent Enemy or Hazard
          //get damage amount from Enemy or Hazard
          //Enemy can change his damage amount
     }

     // Update is called once per frame
     void Update()
     {

     }

     //If we make colliders appear on attacks, create OnCollisionEnter and OnTriggerEnter collisions
     //destroy collider after they hit something

     public void OnCollisionStay(Collision other)
     {
          //Check for player collision
          if (other.gameObject.GetComponent<Enemy>())
          {
               //Find components necessary to take damage and knockback
               GameObject enemObj = other.gameObject;
               Enemy enem = enemObj.GetComponent<Enemy>();
               PlayerHealth hp = enemObj.GetComponent<PlayerHealth>();
               //Take damage if the player isnt already currently invincible
               if (!enem.getInvincible())
               {
                    //Deal damage, knockback
                    //get amt (1), dmgAmount(1) from Enemy/Hazard
                    //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                    hp.takeDamage(1);
                    enem.setInvTime(invTime);
               }

               if(GetComponent<Projectile>())
               {
                    Destroy(gameObject);
               }
          }

     }

     //Same code just make sure it happens
     public void OnTriggerStay(Collider other)
     {
          //Check for player collision
          if (other.gameObject.GetComponent<Enemy>())
          {
               //Find components necessary to take damage and knockback
               GameObject enemObj = other.gameObject;
               Enemy enem = enemObj.GetComponent<Enemy>();
               PlayerHealth hp = enemObj.GetComponent<PlayerHealth>();
               //Take damage if the player isnt already currently invincible
               if (!enem.getInvincible())
               {
                    //Deal damage, knockback
                    //get amt (1), dmgAmount(1) from Enemy/Hazard
                    //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                    hp.takeDamage(1);
                    enem.setInvTime(invTime);
               }

               if (GetComponent<Projectile>())
               {
                    if(tag == "DProj")
                    Destroy(gameObject);
               }
          }
     }
}
