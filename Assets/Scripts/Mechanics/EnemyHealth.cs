using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    public float startingHealth, regenAmount;
    private float currentHealth;
    private bool canKnock = true;
    private MoveController moveController;
    //Create hp bars for players and bosses


    // Use this for initialization
    void Start()
    {
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

        currentHealth -= dmg;

        if (moveController)
        {
            moveController.SetKnockback(true);
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        //death animation
        //end level
        if(GetComponent<DropLoot>())
        {
            GetComponent<DropLoot>().DropItem();
        }

        Destroy(gameObject);
    }

    public float getCurrentHp()
    {
        return currentHealth;
    }

}
