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

    //Do not set Strength Agility or Intelligence below 1, it will cause problems when they are multiplied
    //with starting values of the ares they are used in.
    public string Player_Name;
    public int Stamina = 10;
    public int Strength = 10;
    public int Agility = 10;
    public int Intelligence = 10;
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
    private bool isPlayerDown = false;
    private bool isGrounded = true;
    private bool isMoving = false;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float horizontalMoveSpeed = 6;
    public float verticalMoveSpeed = 10;

    public int playerId; // The Rewired player id of this character

    public AudioClip jumpAudio;
    public AudioClip attackAudio;

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
    private float knockBackResistance, knockBackReset, knockBackCounter;
    private float flinchResistance, flinchReset, flinchCounter;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private float velocityZSmoothing;
    private MoveController controller;
    private AttackController attackController;
    private CrowdControllable crowdControllable;
    private Health health;
    private Mana mana;
    private DealDamageToEnemy attack;
    private Defense defense;
    private DealDamage dealDamage;
    //These are for primarily calculating damages and to queu the stats for buffs
    private float basePhysicalDamage;
    private float baseMagicalDamage;
    private float bonusPhysicalDamage;
    private float bonusMagicalDamage;

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
        attack = GetComponentInChildren<DealDamageToEnemy>();
        defense = GetComponent<Defense>();
        attackController = GetComponent<AttackController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        initialRegenTime = 6;
        regenTick = 2;

        knockBackResistance = 10;
        knockBackCounter = 0;
        knockBackReset = 0;

        flinchResistance = 10;
        flinchCounter = 0;
        flinchReset = 0;
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



        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (initialRegenTime > 6)
        {
            if (regenTick > 2)
            {
                regenTick = 0;
                health.Regen();
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

        HandleInput();
        Vector2 input = new Vector2(playerRewired.GetAxisRaw("MoveHorizontal"), playerRewired.GetAxisRaw("MoveVertical"));

        if ((input.x == 0 && input.y == 0))
        {
            isMoving = false;
        }
        else if (attackController.getIsAttack())
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (attackController.getIsAttack())
        {
            input = new Vector2(0, 0);
        }

        if (isDown == false)
        {
            if (!crowdControllable.getStun())
            {

                ReadyMove(input);
            }
        }
        if (knockBackCounter > 0)
        {
            knockBackReset += Time.unscaledDeltaTime;
            if (knockBackReset >= 5)
            {
                knockBackReset = 0;
                knockBackCounter = 0;
            }
        }

        if (flinchCounter > 0)
        {
            flinchReset += Time.unscaledDeltaTime;
            if (flinchReset >= 2)
            {
                // flinchReset = 0;
                // flinchCounter = 0;
            }
        }

        initialRegenTime += Time.unscaledDeltaTime;
        regenTick += Time.unscaledDeltaTime;


        UpdateState();


        //  if (Input.GetButtonDown("UseSkill1"))
        if (playerRewired.GetButtonDown("UseSkill1"))
        {
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

    public void AddStrength(int value)
    {
        Strength = Strength + value;
    }


    public int GetStrength()
    {
        return Strength;
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

    public void AddStamina(int value)
    {
        Stamina = Stamina + value;
    }

    public int GetStamina()
    {
        return Stamina;
    }

    public void SetAgility(int agility)
    {
        if (agility > 0)
        {
            Agility = agility;
        }
        else
        {
            Agility = 1;
        }
    }

    public void AddAgility(int value)
    {
        Agility = Agility + value;
    }

    public int GetAgility()
    {
        return Agility;
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

    public void AddIntelligence(int value)
    {
        Intelligence = Intelligence + value;
    }

    public int GetIntelligence()
    {
        return Intelligence;

    }

    public void AddDefense(int value)
    {
        defense.AddDefense(value);
    }

    public void Fully_Update()
    {
        //health.Updata_Maxhp_withFullRegen();
        health.SetMaxHP(Stamina * 5 + Strength + Agility + Intelligence);
        health.Full_Regen();
        mana.UpdateMaxMP_And_Regen();
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
        Debug.Log(AttackCollider.GetComponent<DealDamage>().getDamage());
        

    }

    public float GetKBResist()
    {
        return knockBackResistance;
    }

    public void ModifyKBCount(float set, float multiplier = 1)
    {
        knockBackCounter += set;
        knockBackCounter *= multiplier;
    }

    public bool GetKnockable()
    {
        if (knockBackCounter >= knockBackResistance)
        {
            return true;
        }
        return false;
    }

    public void ResetKB()
    {
        knockBackReset = 0;
    }

    public float GetFlinchResist()
    {
        return flinchResistance;
    }

    public void ModifyFlinchCount(float set, float multiplier = 1)
    {
        flinchCounter += set;
        flinchCounter *= multiplier;
    }

    public bool GetFlinchable()
    {
        if (flinchCounter >= flinchResistance)
        {
            return true;
        }
        return false;
    }

    public void ResetFlinch()
    {
        flinchReset = 0;
    }

    private void HandleInput()
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

    private void UpdateState()
    {
        state.UpdateState(this);
        attackState.UpdateState(this);
    }

    private void ReadyMove(Vector2 input)
    {
        velocity.y += gravity * Time.unscaledDeltaTime;

        float targetVelocityX = input.x * (horizontalMoveSpeed + Agility) * crowdControllable.getSlow();
        float targetVelocityZ = input.y * (verticalMoveSpeed + Agility) * crowdControllable.getSlow();
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, ((controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne) * Time.unscaledDeltaTime);
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, ((controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne) * Time.unscaledDeltaTime);
        controller.Move(velocity * Time.unscaledDeltaTime, input);
    }

    public void SetIsGrounded(bool isPlayerOnGround)
    {
        isGrounded = isPlayerOnGround;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
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
        velocity.y = 0;
    }

    public void Jump()
    {
        velocity.y = jumpVelocity;
    }

    public GameObject GetAttackCollider()
    {
        return AttackCollider;
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        Debug.Log("Initialized...");
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
        for (int i = 0; i < skill.Length; i++)
        {
            skill[i] = null;
        }
        skillManager.Reset();
        setDamage(0);
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
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Ranged)
                ret += sk.value;
        }

        return ret;
    }

    public float GetMelee()
    {
        float ret = 0;

        foreach (Skill sk in skill)
        {
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Melee)
                ret += sk.value;
        }

        return ret;
    }

    public float GetSupport()
    {
        float ret = 0;

        foreach (Skill sk in skill)
        {
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Support)
                ret += sk.value;
        }

        return ret;
    }

    public float GetOther()
    {
        float ret = 0;

        foreach (Skill sk in skill)
        {
            Skill.Type f = sk.skillType;
            if (f == Skill.Type.Other)
                ret += sk.value;
        }

        return ret;
    }

    public void setDamage(float f)
    {
        damageDealt = f;
    }

    public void setDown(bool t)
    {
        isDown = t;
    }

    public bool getDown()
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

    public float getPhysicalDamage()
    {
        return basePhysicalDamage + bonusPhysicalDamage + CCI.Class_info[class_id].weapon[weapon_level].patk; ;
    }
    public float getMagicalDamage()
    {
        return baseMagicalDamage + bonusMagicalDamage + +CCI.Class_info[class_id].weapon[weapon_level].matk;
    }
    public float getBasePhysicalDamage()
    {
        return basePhysicalDamage;
    }
    public float getBaseMagicalDamage()
    {
        return baseMagicalDamage;
    }
    public float getBonusPhysicalDamage()
    {
        return bonusPhysicalDamage;
    }
    public float getBonusMagicalDamage()
    {
        return bonusMagicalDamage + CCI.Class_info[class_id].weapon[weapon_level].matk; ;
    }
    public void addBonusPhysicalDamage(float i)
    {
        bonusPhysicalDamage += i;
    }
    public void addBonusMagicalDamage(float i)
    {
        bonusMagicalDamage += i;
    }
}
