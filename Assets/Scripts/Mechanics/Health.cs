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
          CurrentHealth += RegenAmount * player.GetStrength();
          if (CurrentHealth > StartingHealth)
          {
               CurrentHealth = StartingHealth * player.GetStrength();
          }
     }

     public void takeDamage(float dmg, float knockback = 4, float flinch = 5)
     {
        if (player)
        {
            if (!player.GetInvincible())
            {
                currentHealth -= dmg;
                player.ModifyKBCount(knockback);
                if (knockback > 0)
                    player.ResetKB();

                player.ModifyFlinchCount(flinch);
                if (flinch > 0)
                    player.ResetFlinch();

                if(moveController)
                {
                    if (player.GetKnockable())
                    {
                        Debug.Log("Hey");
                        moveController.SetKnockback(true);
                        player.ModifyKBCount(0, 0);
                    }
                    else if(player.GetFlinchable())
                    {
                        Debug.Log("Ho");
                        moveController.SetFlinch(true);
                        player.ModifyFlinchCount(0, 0);
                    }
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
