using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

     public float startingHealth, regenAmount;
     private float currentHealth;
     private Player player;
     private bool canKnock = true;
     //Create hp bars for players and bosses


     // Use this for initialization
     void Start()
     {
          player = GetComponent<Player>();
          currentHealth = startingHealth;
     }

     // Update is called once per frame
     void Update()
     {

     }

     public void regen()
     {
          startingHealth += regenAmount;
          if (currentHealth > startingHealth)
          {
               currentHealth = startingHealth;
          }
     }

     public void takeDamage(float dmg)
     {
        if (player)
        {
            if (!player.getInvincible())
            {
                currentHealth -= dmg;
                if (currentHealth <= 0)
                {
                    //Player can be revived by teammates
                    PlayerDown();
                }
            }
        }
        else
        {
            currentHealth -= dmg;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
     }

     public void PlayerDown()
     {
          //use other object to check if all players down, if so then Death() + lose level
          Death();
     }

     public void Death()
     {
          //death animation
          //end level
          Destroy(gameObject);
     }

     //for triggers
     public void findKnockback(Collider other, Vector3 currentPos, float amt, float force = 1f, bool overrideKnock = false)
     {
          Vector3 contactPoint = other.transform.position;
          Vector3 center = currentPos;
          if (canKnock || overrideKnock)
          {
               Vector3 pushDirection = new Vector3(contactPoint.x - center.x, 0, 0);
               if (player)
               {
                    player.Knock(pushDirection.normalized, amt, force);
               }
          }

     }

     //for colliders
     public void findKnockback(Collision other, Vector3 currentPos, float amt, float force = 1f, bool overrideKnock = false)
     {
          Vector3 contactPoint = other.transform.position;
          Vector3 center = currentPos;
          if (canKnock || overrideKnock)
          {
               Vector3 pushDirection = new Vector3(contactPoint.x - center.x, center.y, center.z);
               if (player)
               {
                    player.Knock(pushDirection.normalized, amt, force);
               }
          }

     }


     public float getCurrentHp()
     {
          return currentHealth;
     }

     //Same as canAct?
     public void setKnock(bool x)
     {
          canKnock = x;
     }
}
