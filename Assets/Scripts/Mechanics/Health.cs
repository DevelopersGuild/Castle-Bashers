using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float StartingHealth;
    public float RegenAmount;
     private float currentHealth;
     private Player player;
     private bool canKnock = true;
    private MoveController moveController;
     //Create hp bars for players and bosses


     // Use this for initialization
     void Start()
     {
          player = GetComponent<Player>();
        moveController = GetComponent<MoveController>();
          currentHealth = StartingHealth * player.GetStrength();
     }

     public void Regen()
     {
        StartingHealth += RegenAmount * player.GetStrength();
          if (currentHealth > StartingHealth)
          {
               currentHealth = StartingHealth * player.GetStrength();
          }
     }

     public void TakeDamage(float dmg)
     {
        if (player)
        {
            if (!player.GetInvincible())
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

     public float GetCurrentHealth()
     {
          return currentHealth;
     }

}
