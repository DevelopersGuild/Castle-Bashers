using UnityEngine;
using System.Collections;

public class BossHealth : Health
{
    private float currentHealth;
    private Player player;
    private Boss enemy;
    private bool canKnock = false;
    private MoveController moveController;
    //Create hp bars for players and bosses


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>(); //find player
        enemy = GetComponent<Boss>();
        moveController = GetComponent<MoveController>();
        damageTextOffset = new Vector3(0, 2, 0);
        currentHealth = startingHealth; //+ player.getLevel()/3
        Update_Maxhp();
    }

    void Update()
    {

    }

    public override float GetCurrentHealth()
    {
        return currentHealth;
    }

    public override void takeDamage(float dmg, float knockback = 0, float flinch = 0)
    {
        if (!enemy.GetInvincible())
        {
            currentHealth -= dmg;
            GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;
            floatText.GetComponent<TextMesh>().text = "" + dmg;
            floatText.transform.position = gameObject.transform.position + damageTextOffset;
            enemy.setInvTime(2);
            enemy.TurnOnBlink();
            if (currentHealth <= 0)
            {
                //Player can be revived by teammates
                Death();
            }
        }
    }

    public override void Death()
    {
        //death animation
        //end level
        Destroy(gameObject);
    }
}
