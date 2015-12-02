using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public int ExperinceAmount = 0;
    public float startingHealth;
    public float RegenAmount;
    private float currentHealth=0;
    private float maxhp;
    private Player player;
    private DealDamageToEnemy attack;
    private bool canKnock = true;
    private MoveController moveController;
    private bool isPlayerDown = false;
    public Vector3 damageTextOffset;
    public AudioClip hitSound;


    // Use this for initialization
    void Start()
    {
        hitSound = Resources.Load("hurt2") as AudioClip;
        player = GetComponent<Player>();
        attack = GetComponentInChildren<DealDamageToEnemy>();
        moveController = GetComponent<MoveController>();
        currentHealth = startingHealth;
        maxhp = startingHealth;
        damageTextOffset = new Vector3(0, 2, 0);
        maxhp = startingHealth;
        if (player)
            maxhp = startingHealth + player.GetStrength() * 10;
        
    }

    void Update()
    {

    }

    public void Update_Maxhp()
    {
        if (player)
        {
            maxhp = startingHealth + player.GetStrength() * 10 + player.GetStamina() * 30 + player.CCI.Class_info[player.GetClassID()].accessory[player.GetAccessoriesLV()].maxhp;
            Debug.Log("UPDATE_MAXHP SET HP TO " + maxhp);
        }
        else
            maxhp = startingHealth;
        
    }

    public void Updata_Maxhp_withFullRegen()
    {
        Update_Maxhp();
        Full_Regen();
    }

    public void Full_Regen()
    {
        currentHealth = maxhp;
    }

    public void Regen()
    {
        currentHealth += (RegenAmount + player.GetStamina()*3);
        if (currentHealth > maxhp)
        {
            currentHealth = maxhp;
        }
    }


    public virtual void takeDamage(float dmg, float knockback = 4, float flinch = 5)
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);
        if (player)
        {
            if (!player.GetInvincible())
            {
                currentHealth -= dmg;
                createFloatingText(dmg);

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
                        moveController.SetKnockback(true);
                        player.ModifyKBCount(0, 0);
                    }
                    else if (player.GetFlinchable())
                    {
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
            createFloatingText(dmg);

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
        GetComponent<Player>().setDown(true);
        //use other object to check if all players down, if so then Death() + lose level
        isPlayerDown = true;
        GameManager.Notifications.PostNotification(new Message(this.gameObject, MessageTypes.PLAYER_DEATH));
        //Death();
    }

    public virtual void Death()
    {
        //death animation
        //end level
        if (GetComponent<DropLoot>())
        {
            GetComponent<DropLoot>().DropItem();
        }

        Destroy(gameObject);
    }

    void createFloatingText(float f)
    {

        GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;
        floatText.GetComponent<TextMesh>().text = "" + f;
        floatText.transform.position = gameObject.transform.position + damageTextOffset;
    }

    public float GetStartingHealth()
    {
        return startingHealth;
    }

    public virtual float GetCurrentHealth()
    {
        return gameObject.GetComponent<Health>().currentHealth;
    }

    public void AddHealth(float healthAmount)
    {
        currentHealth = currentHealth + healthAmount;
        if(currentHealth > maxhp)
        {
            currentHealth = maxhp;
        }
    }

    public float GetMaxHP()
    {
        return maxhp;
    }

    public bool GetIsPlayerDown()
    {
        return isPlayerDown;
    }

    public void SetMaxHP(float f)
    {
        maxhp = f;
    }

    public void SetCurrentHP(float f)
    {
        currentHealth = f;
    }
}
