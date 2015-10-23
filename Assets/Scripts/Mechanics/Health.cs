using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

     public float startingHealth, regenAmount;
     public float currentHealth;
     private Player player;
     private bool canKnock = true;
    private MoveController moveController;
    public Vector3 damageTextOffset;
     //Create hp bars for players and bosses


     // Use this for initialization
     void Start()
     {
          player = GetComponent<Player>();
        moveController = GetComponent<MoveController>();
          currentHealth = startingHealth;
        damageTextOffset = new Vector3(0, 2, 0);
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
            if (!player.GetInvincible())
            {
                currentHealth -= dmg;
                GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;

                floatText.GetComponent<TextMesh>().text = "" + dmg;
                floatText.transform.position = gameObject.transform.position + damageTextOffset;
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
