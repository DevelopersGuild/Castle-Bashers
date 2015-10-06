using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
     private IPlayerState state;

     public void HandleInput(Input input)
    {
        IPlayerState newState = state.HandleInput(this);
        if(newState != null)
        {
            state = newState;
        }

    }

    public void UpdateState()
    {
        state.UpdateState(this);
    }

    private bool isGrounded = true;

     public float jumpHeight = 4;
     public float timeToJumpApex = .4f;
     float accelerationTimeAirborne = .2f;
     float accelerationTimeGrounded = .1f;
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

     float gravity;
     float jumpVelocity;
     Vector3 velocity;
     float velocityXSmoothing;
     float velocityZSmoothing;

     MoveController controller;
     PlayerHealth hp;

     void Start()
     {
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
          if (controller.collisions.above || controller.collisions.below)
          {
               velocity.y = 0;
                isGrounded = true;
               
          }

          if (Input.GetKey(KeyCode.Space) && controller.collisions.below && isNotStunned)
          {
               velocity.y = jumpVelocity;
                isGrounded = false;
          }


          if (isNotStunned)
          {
            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
          }

          

          knockReset += Time.deltaTime;
          hitReset += Time.deltaTime;
     }

     public void Knock(Vector3 dir, float amt, float force)
     {
          kbDir = new Vector3(dir.x, dir.y - 0.3f, dir.z);
          kbCounter += amt;
          kbForce = force;
          knockReset = 0;
          controller.knockback(kbDir, kbForce/2);
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

    public void Move(Vector2 input)
    {
        velocity.y += gravity * Time.deltaTime;
        float targetVelocityX = input.x * horizontalMoveSpeed;
        float targetVelocityZ = input.y * verticalMoveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        controller.Move(velocity * Time.deltaTime, input);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public MoveController GetMoveController()
    {
        return controller;
    }

    public void AssignJumpVolicity(float jumpVelocity)
    {
        velocity.y = jumpVelocity;
    }
}
