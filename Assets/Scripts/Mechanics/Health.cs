using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float RegenAmount;
    public float currentHealth=0;
    public float maxhp;
    public GameObject deathObject;
    public float bonusHP = 0;
    public float bonusPercentHP = 0;
    public bool isInvincible;
    private float invincilityTimer;
    private Player player;
    private MoveController moveController;
    private CrowdControllable crowdControllable;
    private bool isDead;
    
    public Vector3 damageTextOffset;
    public AudioClip hitSound;
    public GameObject hitParticle;


    // Use this for initialization
    void Start()
    {
        isInvincible = false;
        hitSound = Resources.Load("hurt2") as AudioClip;
        player = GetComponent<Player>();
        moveController = GetComponent<MoveController>();
        crowdControllable = GetComponent<CrowdControllable>();
        currentHealth = maxhp; 
        damageTextOffset = new Vector3(0, 2, 0);
    }

    
    public void Update_Maxhp()
    {
        Debug.Log("You are using Update_Maxhp() which is an outdated function");
        /*
        if (player)
        {
            maxhp = startingHealth + player.GetStrength() * 10 + player.GetStamina() * 30 + player.CCI.Class_info[player.GetClassID()].accessory[player.GetAccessoriesLV()].maxhp;
            Debug.Log("UPDATE_MAXHP SET HP TO " + maxhp);
        }
        else
            maxhp = startingHealth;
        */
    }

    public void Updata_Maxhp_withFullRegen()
    {
        //Update_Maxhp();
        Full_Regen();
    }

    public void Full_Regen()
    {
        currentHealth = GetMaxHP();
    }

    public void Regen()
    {
        currentHealth += (player.GetStamina() / RegenAmount);
        if (currentHealth > GetMaxHP())
        {
            currentHealth = GetMaxHP();
        }
    }


    public virtual void takeDamage(float dmg, int flinch = 4)
    {
        if (!isInvincible)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);

            if (hitParticle)
            {
                Destroy(Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity), 2f);
            }
            //Rounding damage up to the nearest int for a clean display. It may make some situations easier in the early game
            //but considering the nature of a hack and slash, that shouldn't be an issue. Will keep an eye on the effects.
            dmg = Mathf.CeilToInt(dmg);
            currentHealth -= dmg;
            createFloatingText(dmg);

            if (moveController)
            {
                moveController.handleFlinch(flinch);
                if (!player)
                {
                    GetComponent<Enemy>().setIsAttacking(false);
                }
            }
        }


        if (currentHealth <= 0)
            Death();
    }


    public bool getInvincibility()
    {
        return isInvincible;
    }

    public void setInvincility(bool invincibility)
    {
        isInvincible = invincibility;
    }

    public void setToInvincible()
    {
        isInvincible = true;
        if(player)
           player.DisableInput();
    }

    public void setToNotInvincible()
    {
        isInvincible = false;
        if(player)
            player.enableInput();
    }

    public void StartInvincibilityTimer(float time = 1.5f)
    {
        isInvincible = true;
        StartCoroutine(InvincilityTimerCoroutine(time));
    }

    IEnumerator InvincilityTimerCoroutine(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            isInvincible = false;
        }
    }

    public void PlayerDown()
    {
        GetComponent<Player>().setDown(true);
        isDead = true;
        //use other object to check if all players down, if so then Death() + lose level

        // GameManager.Notifications.PostNotification(new Message(this.gameObject, MessageTypes.PLAYER_DEATH));
        //Death();
    }

    public bool PlayerRevive(int percentHealth)
    {
        Player player = GetComponent<Player>();
        if (player.getDown() == false)
        {
            return false;
        }
        if(percentHealth > 100)
        {
            percentHealth = 100;
        }
        player.setDown(false);
        AddHealth((percentHealth / 100) * maxhp);
        player.enableInput();
        player.GetMoveController().SetFlinch(false);
        return true;
    }

    public virtual void Death()
    {
        //death animation
        //end level

        // Down the player if it was a player that died
        if(GetComponent<Player>())
        {
            PlayerDown();
            return;
        }

        // Reward all players with experience if an enemy died
        if (GetComponent<TestEnemy>())
        {
            TestEnemy enemy = GetComponent<TestEnemy>();
            if (currentHealth <= 0)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject character in players)
                {
                    character.GetComponent<Experience>().AddExperience(enemy.experienceAmount);
                }
            }
        }

        // Drop loot
        if (GetComponent<DropLoot>())
        {
            GetComponent<DropLoot>().DropItem();
        }

        if(deathObject)
        {
            Instantiate(deathObject, new Vector3(transform.position.x, 3.7f, transform.position.z), Quaternion.identity);
        }
        transform.position = new Vector3(transform.position.x, 100, transform.position.z);
        StartCoroutine(DestroyCountDown());
    }

    IEnumerator DestroyCountDown()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public bool getIsDead()
    {
        return isDead;
    }

    void createFloatingText(float f)
    {

        GameObject floatText = Instantiate(Resources.Load("FloatingText")) as GameObject;
        floatText.GetComponent<TextMesh>().text = "" + f;
        floatText.transform.position = gameObject.transform.position + damageTextOffset;
    }
/*
    public float GetStartingHealth()
    {
        return startingHealth;
    }
    */
    public virtual float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddHealth(float healthAmount)
    {
        currentHealth = currentHealth + healthAmount;
        if(currentHealth > GetMaxHP())
        {
            currentHealth = maxhp;
        }
    }

    public float GetMaxHP()
    {
        return maxhp * (1+bonusPercentHP*0.01f) + bonusHP;
    }

    public float getBonusHP()
    {
        return bonusHP;
    }
    public float getBonusPercentHP()
    {
        return bonusPercentHP;
    }

    public void SetMaxHP(float f)
    {
        maxhp = f;
    }

    public void SetCurrentHP(float f)
    {
        currentHealth = f;
    }

    public void addBonusHP(float f)
    {
        bonusHP += f;
    }
    public void addBonusPercentHP(float f)
    {
        bonusPercentHP += f;
    }
}
