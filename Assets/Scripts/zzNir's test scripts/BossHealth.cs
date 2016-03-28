using UnityEngine;
using System.Collections;

public class BossHealth : Health
{
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
        currentHealth = maxhp; //+ player.getLevel()/3
        Update_Maxhp();
    }

    public override float GetCurrentHealth()
    {
        return currentHealth;
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
