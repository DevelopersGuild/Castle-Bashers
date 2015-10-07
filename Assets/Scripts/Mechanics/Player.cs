using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    public GameObject BasicAttackPrefab;
    private IPlayerState state;
    private IAttack attackState;

    private bool isGrounded = true;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    public float horizontalMoveSpeed = 6;
    public float verticalMoveSpeed = 10;

    public float kbLimit = 3;
    public float kbCounter = 0;
    public float kbForce = 1;
    private Vector3 kbDir;

    private double knockReset, hitReset;

    private bool isNotStunned = true;
    private bool isInvincible = false;

    private float invTime;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private float velocityZSmoothing;

    private MoveController controller;
    private PlayerHealth hp;

    void Start()
    {
        state = new StandingState();
        attackState = new IdleAttackState();
        hp = GetComponent<PlayerHealth>();
        controller = GetComponent<MoveController>();
        knockReset = hitReset = 0;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (kbCounter >= kbLimit)
        {
            controller.knockback(kbDir, kbForce);
            isInvincible = true;
            kbCounter = 0;
            hp.setKnock(false);
        }
        if (knockReset > 10)
        {
            kbCounter = 0;
            knockReset = 0;
        }

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
        controller.checkKnock();
        hp.setKnock(true);

        HandleInput();
        if (isNotStunned)
        {
            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
        UpdateState();
        knockReset += Time.deltaTime;
        hitReset += Time.deltaTime;
    }

    public void Knock(Vector3 dir, float amt, float force)
    {
        kbDir = new Vector3(dir.x, dir.y - 0.3f, dir.z);
        kbCounter += amt;
        kbForce = force;
        knockReset = 0;
        controller.knockback(kbDir, kbForce / 2);
    }

    //Reset hitReset when hit
    public void setInvTime(float time)
    {
        invTime = time;
    }

    public void setAct(bool x)
    {
        isNotStunned = x;
    }

    public bool getInvincible()
    {
        return isInvincible;
    }

    public void setInvincible(bool x)
    {
        isInvincible = x;
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
        if(newAttackState != null)
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

    private void Move(Vector2 input)
    {
        velocity.y += gravity * Time.deltaTime;
        float targetVelocityX = input.x * horizontalMoveSpeed;
        float targetVelocityZ = input.y * verticalMoveSpeed;
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
}
