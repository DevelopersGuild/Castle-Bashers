using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

     public float startingHealth, regenAmount;
     public float currentHealth;
     private Player player;
     private bool canKnock = true;
    private MoveController moveController;
     //Create hp bars for players and bosses


     // Use this for initialization
     void Start()
     {
          player = GetComponent<Player>();
        moveController = GetComponent<MoveController>();
          currentHealth = startingHealth;
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
                if(moveController)
                {
                    moveController.SetKnockback(true);
                }
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

     public float getCurrentHp()
     {
          return currentHealth;
     }

}
