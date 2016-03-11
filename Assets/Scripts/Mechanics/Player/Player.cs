using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    //private static Player player;
    [HideInInspector]
    public Animator animator;
    public GameObject AttackCollider;
    public SkillManager skillManager;
    public Skill[] Skills = new Skill[4];
    public Health health;

    //Do not set Strength Agility or Intelligence below 1, it will cause problems when they are multiplied
    //with starting values of the ares they are used in.
    public string Player_Name;
    public int Stamina = 10;
    public int Strength = 10;
    public int Agility = 10;
    public int Intelligence = 10;

    public int bonusStamina = 0;
    public int bonusPercentStamina = 0;

    public int bonusStrength = 0;
    public int bonusPercentStrength = 0;

    public int bonusAgility = 0;
    public int bonusPercentAgility = 0;

    public int bonusIntelligence = 0;
    public int bonusPercentIntelligence = 0;

    //The stats should remain public to allow them to be set in the editor.
    [HideInInspector]
    public Character_Class_Info CCI;
    [HideInInspector]
    public Skill_info si;
    private int class_id = 0;
    private int weapon_level = 0;
    private int armor_level = 0;
    private int accessories_level = 0;
    private float blockchance = 0;
    private bool isGrounded = true;
    private bool isMoving = false;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float horizontalMoveSpeed = 6;
    public float verticalMoveSpeed = 10;

    public int playerId; // The Rewired player id of this character

    public AudioClip jumpAudio;
    public AudioClip attackAudio;
    private AudioSource audiosource;

    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private bool isNotStunned = true;
    private bool isInvincible = false;
    private bool isPoly = false;
    private bool isDown = false;
    private float polyTime = 0;
    private IPlayerState state;
    private IAttack attackState;

    private float invTime, initialRegenTime, regenTick;

    private bool disableInput;
    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private float velocityZSmoothing;
    private bool isJumping;
    private MoveController controller;
    private AttackController attackController;
    private CrowdControllable crowdControllable;

    private Mana mana;
    private DealDamage attack;
    private Defense defense;
    private DealDamage dealDamage;

    //These are for primarily calculating damages and to queu the stats for buffs
    private float basePhysicalDamage;
    private float bonusPhysicalDamage;
    private float bonusPercentPhysicalDamage = 0;

    private float baseMagicalDamage;
    private float bonusMagicalDamage;
    private float bonusPercentMagicalDamage = 0;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;
    public Rewired.Player playerRewired;

    private Skill[] skill = new Skill[4];
    private float managerID, priorityID;
    [HideInInspector]
    public float threatLevel, damageDealt;
    private bool[] skill_unlock = new bool[14];
    private int[] skillslot = { -1, -1, -1, -1 };
    private int[] itemslot = { -1, -1, -1 };

    private bool isThrown = false;
    private Vector3 thrownVelocity;
    private float throwCheck = 0.3f;

    /*
    void Awake()
    {
        if (player == null)
        {
            player = gameObject;
        }else if(player != gameObject)
        {
            Destroy(gameObject);
        }
    }*/
    void Start()
    {
        state = new StandingState();
        attackState = new IdleAttackState();
        animator = GetComponent<Animator>();
        skillManager = gameObject.AddComponent<SkillManager>();
        AttackCollider.SetActive(false);
        health = GetComponent<Health>();
        controller = GetComponent<MoveController>();
        crowdControllable = GetComponent<CrowdControllable>();
        mana = GetComponent<Mana>();
        attack = GetComponentInChildren<DealDamage>();
        defense = GetComponent<Defense>();
        attackController = GetComponent<AttackController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        audiosource = GetComponent<AudioSource>();

        initialRegenTime = 6;
        regenTick = 2;

        DontDestroyOnLoad(gameObject);

        skill[0] = null;
        skill[1] = null;
        skill[2] = null;
        skill[3] = null;
        threatLevel = damageDealt = 0;

        GetComponent<ID>().setTime(false);
        CCI = GameObject.Find("Main Process").GetComponentInChildren<Character_Class_Info>();
        si = GameObject.Find("Main Process").GetComponentInChildren<Skill_info>();
        Fully_Update();
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor
        Vector2 input;

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (initialRegenTime > 6)
        {
            if (regenTick > 2)
            {
                regenTick = 0;
                //health.Regen();
            }
        }

        //Invincibility timer
        if (invTime >= 0)
        {
            if (invTime != 0)
            {
                isInvincible = true;
                invTime -= Time.unscaledDeltaTime;
            }
        }
        else
        {
            isInvincible = false;
            invTime = 0;
        }



        if(!GetMoveController().isStunned)
        {
            input = new Vector2(playerRewired.GetAxisRaw("MoveHorizontal"), playerRewired.GetAxisRaw("MoveVertical"));
            HandleInput();
        }
        else
        {
            input = new Vector2(0, 0);
        }

        

        if ((input.x == 0 && input.y == 0))
        {
            isMoving = false;
        }
        else if (attackController.GetIsAttack() && GetMoveController().GetIsGrounded())
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (attackController.GetIsAttack() && GetMoveController().GetIsGrounded())
        {
            input = new Vector2(0, 0);
        }

        if (!isDown && !disableInput)
        {
            if (!crowdControllable.getStun() && !isThrown)
            {

                ReadyMove(input);
            }
            else if (isThrown)
            {
                //Malady can throw, so while in air the player should be stunned and be falling
                //didn't know how the gravity worked and if I could just add a velocity to the rigidbody and it would be okay, so I did this
                //subject to change
                controller.isKnockbackable = false;
                controller.isFlinchable = false;
                controller.Move(thrownVelocity);
                thrownVelocity *= .99f;
                thrownVelocity.y *= 0.98f;
                //check throw only after 0.3s (so it doesn't do something stupid like check before moving initially and says "Oh, you're already on the ground, throw over"
                if (throwCheck <= 0)
                {
                    //when hitting the ground, thrown stops, small stun added, maybe getting up animation? eh
                    if (controller.collisions.below)
                    {
                        isThrown = false;
                        crowdControllable.addStun(0.3f);
                        throwCheck = 0.3f;
                        //maybe after stun?
                        controller.isKnockbackable = true;
                        controller.isFlinchable = true;
                    }
                    throwCheck -= Time.unscaledDeltaTime;
                }
            }
        }

        initialRegenTime += Time.unscaledDeltaTime;
        regenTick += Time.unscaledDeltaTime;


        UpdateState();
        audiosource.volume = Globe.sound_volume;

        //  if (Input.GetButtonDown("UseSkill1"))
        if (playerRewired.GetButtonDown("UseSkill1"))
        {
            health.PlayerRevive(100);
            skillManager.UseSkill1();
        }

        if (playerRewired.GetButtonDown("UseSkill2"))
        {
            skillManager.UseSkill2();
        }

        if (playerRewired.GetButtonDown("UseSkill3"))
        {
            skillManager.UseSkill3();
        }

        if (playerRewired.GetButtonDown("UseSkill4"))
        {
            skillManager.UseSkill4();
        }

    }

    public void throwPlayer(Vector3 v)
    {
        thrownVelocity = v;
        isThrown = true;

    }

    public void setPoly(float val, float time)
    {
        crowdControllable.addSlow(val, time);
        polyTime = time;
        //do not let state attack for polyTime time, ask joseph how to do that while still keeping it neat
        //can attack, but attack does 0 damage and no knockback or flinch.
        //Animations changed
    }

    //Reset hitReset when hit
    public void SetInvTime(float time)
    {
        invTime = time;
        initialRegenTime = 0;
    }

    public void SetAct(bool x)
    {
        isNotStunned = x;
    }
    
    public bool GetInvincible()
    {
        return isInvincible;
    }

    public void SetInvincible(bool x)
    {
        isInvincible = x;
    }

    public void Fully_Update()
    {
        //health.Updata_Maxhp_withFullRegen();
        health.SetMaxHP((Stamina * 5 + Strength + Agility + Intelligence));
        health.Full_Regen();
        mana.SetMaxMana(Stamina * 2 + Intelligence * 3);
        mana.Full_Regen();

        /*These are using the old DealDamageToEnemy script*/
        //In addition, the functions are ambiguous. The player should not be dealing magic damage with the physical attack collider?
        //
        //attack.UpdateDamage(5 * Strength + Agility + CCI.Class_info[class_id].weapon[weapon_level].patk, 2 * Strength + 5 * Intelligence+CCI.Class_info[class_id].weapon[weapon_level].matk);
        //attack.UpdateChange(Strength * 0.1f + Agility, Intelligence * 0.15f + Agility);
        //attack.SetCriticalChance(Agility * 0.001f + CCI.Class_info[class_id].accessory[accessories_level].cri);

        blockchance = Agility * 0.001f;
        //Moved formulas from Update_Defense() to this function for simplicity and to avoid passing extra references
        //defense.Update_Defense();
        defense.SetBasePhysicalDefense((int)(0.3f * Strength + 1.5f * Stamina));
        defense.SetBaseMagicalDefense((int)(1.5f * Stamina));
        basePhysicalDamage = 0.75f * Strength;
        baseMagicalDamage = 1 * Intelligence;
        AttackCollider.GetComponent<DealDamage>().setDamage(basePhysicalDamage);

    }

    private void HandleInput()
    {
        if (!disableInput)
        {
            IPlayerState newState = state.HandleInput(this);
            if (newState != null)
            {
                state.ExitState(this);
                state = newState;
                state.EnterState(this);
            }
            IAttack newAttackState = attackState.HandleInput(this);
            if (newAttackState != null)
            {
                attackState.ExitState(this);
                attackState = newAttackState;
                attackState.EnterState(this);
            }
        }

    }

    public void DisableInput()
    {
        disableInput = true;
    }

    public void enableInput()
    {
        disableInput = false;
    }

    public bool getInputDisabled()
    {
        return disableInput;
    }

    private void UpdateState()
    {
        state.UpdateState(this);
        attackState.UpdateState(this);
    }

    private void ReadyMove(Vector2 input)
    {
        velocity.y += gravity * Time.unscaledDeltaTime;

        float targetVelocityX = input.x * (horizontalMoveSpeed + (Agility / 15)) * crowdControllable.getSlow();
        float targetVelocityZ = input.y * (verticalMoveSpeed + (Agility / 15)) * crowdControllable.getSlow();
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, ((controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne) * Time.unscaledDeltaTime);
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, ((controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne) * Time.unscaledDeltaTime);
        controller.Move(velocity * Time.unscaledDeltaTime, input);
    }

    public void setIsMoving(bool move)
    {
        isMoving = move;
    }


    public bool GetIsMoving()
    {
        return isMoving;
    }

    public MoveController GetMoveController()
    {
        return controller;
    }

    public void EndJump()
    {
        isJumping = false;
        velocity.y = 0;
    }

    public void Jump()
    {
        AudioSource.PlayClipAtPoint(jumpAudio, transform.position);
        isJumping = true;
        velocity.y = jumpVelocity;
    }

    public bool getIsJumping()
    {
        return isJumping;
    }

    public GameObject GetAttackCollider()
    {
        return AttackCollider;
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        //Debug.Log("Initialized...");
        playerRewired = ReInput.players.GetPlayer(playerId);
        initialized = true;
    }

    /// <summary>
    /// For malady stuff, not for setting skills, sorry
    /// </summary>
    public void addSkill(Skill s, int pos)
    {
        skill[pos] = s;
    }

    public void Reset()
    {
        Debug.Log("If this function is getting called, there is a chance Damen broke something");
        for (int i = 0; i < skill.Length; i++)
        {
            skill[i] = null;
        }
        skillManager.Reset();
        //setDamage(0); //And this is the thing he may have broken
        //not actual algorithm
        threatLevel = (Strength + Intelligence) / (Strength + Intelligence + Agility);
    }

    public void setPriorityID(float f)
    {
        priorityID = f;
    }

    public float getPriorityID()
    {
        return priorityID;
    }

    public float GetRanged()
    {
        float ret = 0;

        foreach (Skill sk in skill)
        {
            Skill.Type f;
            //When skills exist
            //f = sk.skillType;
            /*
            if (f == Skill.Type.Ranged)
                ret += sk.value;
                */
        }

        return ret;
    }

    public float GetMelee()
    {
        float ret = 0;

        foreach (Skill sk in skill)
        {
            /* Skill.Type f = sk.skillType;
             if (f == Skill.Type.Melee)
                 ret += sk.value;
      */
        }

        return ret;
    }

    public float GetSupport()
    {
        float ret = 0;
        /*
        foreach (Skill sk in skill)
        {
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Support)
                ret += sk.value;
        }
        */
        return ret;
    }

    public float GetOther()
    {
        float ret = 0;
        /*
        foreach (Skill sk in skill)
        {
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Other)
                ret += sk.value;
        }
        */
        return ret;
    }



    public void setDown(bool t)
    {
        isDown = t;
    }

    public bool getDown() // to business, to defeat the hun
    {
        return isDown;
    }

    public float getThreatLevel()
    {
        //example - 50 str 10 int 15 agility
        //physTotal = 24, magTotal = 3;
        //threat level = 16.4

        //example - 100 str 05 int 05 agility (cause of base stats)
        //physTotal = 50, magTotal = 0;
        //threat level = 45.45

        float statTotal = Strength + Intelligence + Agility;
        //float physTotal = 0;
        //float magTotal = 0;
        //foreach(Skill sk in skill) {
        //magTotal += sk.getMagThreatLevel();
        //physTotal += sk.getPhysThreatLevel();
        //}
        //
        //(physTotal * Strength + Intelligence * magTotal) / statTotal
        return 0;
    }

    public void setManagerID(float f)
    {
        managerID = f;
    }

    public float getManagerID()
    {
        return managerID;
    }

    public void SetClassID(int id)
    {
        class_id = id;
    }

    public int GetClassID()
    {
        return class_id;
    }

    public void SetWeaponLV(int value)
    {
        weapon_level = value;
    }

    public int GetWeaponLV()
    {
        return weapon_level;
    }

    public void SetArmorLV(int level)
    {
        armor_level = level;
    }

    public int GetAmrorLV()
    {
        return armor_level;
    }

    public void SetAccessoriesLV(int level)
    {
        accessories_level = level;
    }

    public int GetAccessoriesLV()
    {
        return accessories_level;
    }

    public float GetBlockChance()
    {
        return blockchance;
    }

    public void SetUnlockSkillList(bool[] list)
    {
        skill_unlock = new bool[list.Length];
        skill_unlock = list;
    }

    public bool[] GetUnlockSkillList()
    {
        return skill_unlock;
    }

    public void SetSkillSlotInit(int id, int skill_index)
    {
        skillslot[id] = skill_index;
        skillManager.ChangeSkill(si.skill[CCI.Class_info[class_id].skillid[skill_index]].skill_script, id);
    }

    public void SetSkillSlot(int id, int skill_index)
    {
        skillslot[id] = skill_index;
        skillManager.FindAndChangeSkill(si.skill[CCI.Class_info[class_id].skillid[skill_index]].skill_script, id);
    }

    public Skill GetSkillSlotScript(int id)
    {
        return skillManager.GetSlotSkill(id);
    }

    public int GetSkillSlotSkillID(int id)
    {
        return skillslot[id - 1];
    }

    public void UpdateSkillSlot()
    {
        for (int i = 0; i <= 3; i++)
            if (skillslot[i] != -1)
                SetSkillSlotInit(i, skillslot[i]);
    }

    public bool GetSkillUnlock(int id)
    {
        return skill_unlock[id];
    }

    public void SetSkillUnlock(int id, bool value)
    {
        skill_unlock[id] = value;
    }



    public float getPhysicalDamage() { return (basePhysicalDamage + bonusPhysicalDamage + CCI.Class_info[class_id].weapon[weapon_level].patk) * (1 + bonusPercentPhysicalDamage*0.01f); }
    public float getBasePhysicalDamage() { return basePhysicalDamage; }
    public float getBonusPhysicalDamage() { return bonusPhysicalDamage; }
    public float getBonusPercentPhysicalDamage() { return bonusPercentPhysicalDamage; }

    public float getMagicalDamage(){ return (baseMagicalDamage + bonusMagicalDamage + CCI.Class_info[class_id].weapon[weapon_level].matk) * (1+ bonusPercentMagicalDamage*0.01f);}
    public float getBaseMagicalDamage() { return baseMagicalDamage;}
    public float getBonusMagicalDamage() { return bonusMagicalDamage + CCI.Class_info[class_id].weapon[weapon_level].matk;}
    public float getBonusPercentMagicalDamage() { return bonusPercentMagicalDamage; }

    public void addBonusPhysicalDamage(float i) { bonusPhysicalDamage += i; }
    public void addBonusMagicalDamage(float i) { bonusMagicalDamage += i; }
    public void addBonusPercentPhysicalDamage(float i) { bonusPercentPhysicalDamage += i; }
    public void addBonusPercentMagicalDamage(float i) { bonusPercentMagicalDamage += i; }

    public void AddDefense(int value) { defense.AddDefense(value); }

    public void AddStrength(int value) { Strength += value; }
    public void AddBonusStrength(int value) { bonusStrength += value; }
    public void AddBonusPercentStrength(int value) { bonusPercentStrength += value; }

    public void AddStamina(int value) { Stamina += value; }
    public void AddBonusStamina(int value) { bonusStamina += value; }
    public void AddBonusPercentStamina(int value) { bonusPercentStamina += value; }

    public void AddAgility(int value) { Agility += value; }
    public void AddBonusAgility(int value) { bonusAgility += value; }
    public void AddBonusPercentAgility(int value) { bonusPercentAgility += value; }

    public void AddIntelligence(int value) { Intelligence += value; }
    public void AddBonusIntelligence(int value) { bonusIntelligence += value; }
    public void AddBonusPercentIntelligence(int value) { bonusPercentIntelligence += value; }

    public int GetStrength() { return Strength; }
    public int GetTotalStrength() { return (int)(Strength * 1 + bonusPercentStrength * 0.01f) + bonusStrength; }
    public int GetStamina() { return Stamina; }
    public int GetTotalStamina() { return (int)(Stamina * 1 + bonusPercentStamina * 0.01f) + bonusStamina; }
    public int GetAgility() { return Agility; }
    public int GetTotalAgility() { return (int)(Agility * 1 + bonusPercentAgility * 0.01f) + bonusAgility; }
    public int GetIntelligence() { return Intelligence; }
    public int GetTotalIntelligence() { return (int)(Intelligence * 1 + bonusPercentIntelligence * 0.01f) + bonusIntelligence; }



    /*
    public void setDamage(float f)
    {
        damageDealt = f;
    }*/


    public void SetStrength(int strength)
    {
        if (strength > 0)
        {
            Strength = strength;
        }
        else
        {
            Strength = 1;
        }
    }

    public void SetStamina(int value)
    {
        if (value > 0)
        {
            Stamina = value;
        }
        else
        {
            Stamina = 1;
        }

    }

    public void SetAgility(int agility) {
        if (agility > 0)
        {
            Agility = agility;
        }
        else
        {
            Agility = 1;
        }
    }


    public void SetIntelligence(int intelligence)
    {
        if (intelligence > 0)
        {
            Intelligence = intelligence;
        }
        else
        {
            Intelligence = 1;
        }
    }





}


