﻿using UnityEngine;
using System.Collections;

public class DealDamageToPlayer : MonoBehaviour
{

     private float dmgAmount = 1;

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
          if (other.gameObject.GetComponent<Player>())
          {
               //Find components necessary to take damage and knockback
               GameObject playerObject = other.gameObject;
               Player player = playerObject.GetComponent<Player>();
               PlayerHealth hp = playerObject.GetComponent<PlayerHealth>();

               //Take damage if the player isnt already currently invincible
               if (!player.getInvincible())
               {
                    //Deal damage, knockback
                    //get amt (1), dmgAmount(1) from Enemy/Hazard
                    hp.findKnockback(other, transform.position, 1);
                    hp.takeDamage(1);
               }

               //Destroy gameobject if its a projectile
               //  if (GetComponent<Projectile>())
               //    {
               //          Destroy(gameObject);
               //  }
               Destroy(gameObject);
          }
         
     }

     //Same code just make sure it happens
     public void OnTriggerStay(Collider other)
     {
          //Check for player collision
          if (other.gameObject.GetComponent<Player>())
          {
               //Find components necessary to take damage and knockback
               GameObject playerObject = other.gameObject;
               Player player = playerObject.GetComponent<Player>();
               PlayerHealth hp = playerObject.GetComponent<PlayerHealth>();

               //Take damage if the player isnt already currently invincible
               if (!player.getInvincible())
               {
                    //Deal damage, knockback
                    //get amt (1), dmgAmount(1) from Enemy/Hazard
                    hp.findKnockback(other, transform.position, 1);
                    hp.takeDamage(1);
               }

               //Destroy gameobject if its a projectile
               //  if (GetComponent<Projectile>())
               //    {
               //          Destroy(gameObject);
               //  }

               Destroy(gameObject);
          }
     }
}

