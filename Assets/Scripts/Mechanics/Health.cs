using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public int ExperinceAmount = 0;
    public float startingHealth;
    public float RegenAmount;
    private float currentHealth;
    private Player player;
    private bool canKnock = true;
    private MoveController moveController;
    public Vector3 damageTextOffset;
<<<<<<< HEAD
=======
    
    //Create hp bars for players and bosses
>>>>>>> 745840507f66fe00ca452fa1318c17e17b3f14d6


    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        moveController = GetComponent<MoveController>();
        currentHealth = startingHealth;
        damageTextOffset = new Vector3(0, 2, 0);

        if (player)
            currentHealth = startingHealth + player.GetStrength();
        else
            currentHealth = startingHealth;
    }

    void Update()
    {
 
    }

    public void Regen()
    {
        currentHealth += (RegenAmount + player.GetStrength());
        if (currentHealth > startingHealth + player.GetStrength())
        {
            currentHealth = (startingHealth + player.GetStrength());
        }
    }

    public virtual void takeDamage(float dmg, float knockback = 4, float flinch = 5)
    {
        Debug.Log(currentHealth);
        if (player)
        {
            if (!player.GetInvincible())
            {
                currentHealth -= dmg;
                GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;
                floatText.GetComponent<TextMesh>().text = "" + dmg;
                floatText.transform.position = gameObject.transform.position + damageTextOffset;

                player.ModifyKBCount(knockback);
                if (knockback > 0)
                    player.ResetKB();

                player.ModifyFlinchCount(flinch);
                if (flinch > 0)
                    player.ResetFlinch();

                if (moveController)
                {
                    if (player.GetKnockable())
                    {
                        Debug.Log("Hey");
                        moveController.SetKnockback(true);
                        player.ModifyKBCount(0, 0);
                    }
                    else if (player.GetFlinchable())
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
            GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;
            floatText.GetComponent<TextMesh>().text = "" + dmg;
            floatText.transform.position = gameObject.transform.position + damageTextOffset;

            if (currentHealth <= 0)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach(GameObject character in players)
                {
                    character.GetComponent<Experience>().AddExperince(ExperinceAmount);
                }
                Death();
            }
        }
    }

    public void PlayerDown()
    {
        //use other object to check if all players down, if so then Death() + lose level
        Death();
    }

    public virtual void Death()
    {
        //death animation
        //end level
        Destroy(gameObject);
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddHealth(float healthAmount)
    {
        currentHealth = currentHealth + healthAmount;
        if(currentHealth > startingHealth + player.GetStrength())
        {
            currentHealth = startingHealth + player.GetStrength();
        }
    }

}
