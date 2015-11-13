using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    //private static Player player;
    public Animator animator;
    public GameObject AttackCollider;
    public SkillManager skillManager;
    public Skill[] Skills = new Skill[4];
    //Do not set Strength Agility or Intelligence below 1, it will cause problems when they are multiplied
    //with starting values of the ares they are used in.
    public string Player_Name;
    //The stats should remain public to allow them to be set in the editor.
    public int Stamina;
    public int Strength;
    public int Agility;
    public int Intelligence;

    private int class_id = 0;
    private int weapon_level=0;
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

    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private bool isNotStunned = true;
    private bool isInvincible = false;
    private bool isPoly = false;
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
    private CrowdControllable crowdControllable;
    private Health health;
    private Mana mana;
    private DealDamageToEnemy attack;
    private Defense defense;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;
    private Rewired.Player playerRewired;

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
                invTime -= Time.deltaTime;
            }
        }
        else
        {
            isInvincible = false;
            invTime = 0;
        }

        HandleInput();
        Vector2 input = new Vector2(playerRewired.GetAxisRaw("MoveHorizontal"), playerRewired.GetAxisRaw("MoveVertical"));

        if (input.x == 0 && input.y == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        if (!crowdControllable.getStun())
        {
 
            ReadyMove(input);
        }

        if (knockBackCounter > 0)
        {
            knockBackReset += Time.deltaTime;
            if (knockBackReset >= 5)
            {
                knockBackReset = 0;
                knockBackCounter = 0;
            }
        }

        if (flinchCounter > 0)
        {
            flinchReset += Time.deltaTime;
            if (flinchReset >= 2)
            {
               // flinchReset = 0;
               // flinchCounter = 0;
            }
        }

        initialRegenTime += Time.deltaTime;
        regenTick += Time.deltaTime;
        UpdateState();
        
        if(Input.GetButtonDown("UseSkill1"))
        {
            skillManager.UseSkill1();
        }

        if (Input.GetButtonDown("UseSkill2"))
        {
            skillManager.UseSkill2();
        }

        if (Input.GetButtonDown("UseSkill3"))
        {
            skillManager.UseSkill3();
        }

        if (Input.GetButtonDown("UseSkill4"))
        {
            skillManager.UseSkill4();
        }
    }

    public void setPoly(float val, float time)
    {
        crowdControllable.addSlow(val, time);
        polyTime = time;
        //do not let state attack for polyTime time, ask joseph how to do that while still keeping it neat
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
        if(value>0)
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
        if(agility > 0)
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
        if(intelligence > 0)
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
        health.Updata_Maxhp_withFullRegen();
        mana.UpdateMaxMP_And_Regen();
        attack.UpdateDamage(5 * Strength + Agility, 2 * Strength + 5 * Intelligence);
        attack.UpdateChange(Strength * 0.1f + Agility, Intelligence * 0.15f + Agility);
        attack.SetCriticalChance(Agility * 0.001f);
        blockchance = Agility * 0.001f;
        defense.Update_Defense();

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
        velocity.y += gravity * Time.deltaTime;

        float targetVelocityX = input.x * (horizontalMoveSpeed + Agility) * crowdControllable.getSlow();
        float targetVelocityZ = input.y * (verticalMoveSpeed + Agility) * crowdControllable.getSlow();
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        controller.Move(velocity * Time.deltaTime, input);
    }
    public void SetIsGrounded(bool isPlayerOnGround)
    {
        isGrounded = isPlayerOnGround;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
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
        playerRewired = ReInput.players.GetPlayer(playerId);
        initialized = true;
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
}
