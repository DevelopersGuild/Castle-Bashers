using UnityEngine;
using System.Collections;
using CreativeSpore;

public class PlayerMoveController : MonoBehaviour
{
     // Components
     private Animator animator;
     public AttackController attackController;
     public ParticleSystem dashParticle;
     private ParticleSystem dashParticleInstance;
     // Speed of object
     [Range(0, 10)]
     public float speed = 10;
     public float dashSpeed;
     // Direction of object
     internal Vector2 direction;
     public Vector2 facing;
     // Actual movement
     internal Vector2 movementVector = new Vector2(0, 0);
     // Flags for state checking
     public bool isMoving;
     public bool isPressingMultiple;
     public bool isDashing;
     public bool canDash;
     public float dashIn;
     private bool dashCooldown;
     public bool canMove;
     public bool gotAttacked;
     public bool IsInMenu;
     //Knockback flags
     public bool isKnockedBack;
     public float knockbackForce;
     private float knockBackTime;
     private float timeSpentKnockedBack;
     private Vector2 knockbackDirection;
     //Player Progression
     public bool IsDashUnlocked;
     public float DashSpeedUpgrade = 0;
     //Pathfinding
     private static float jiggleMax = 0.05f;
     private static Vector3 UL = new Vector3(-jiggleMax,  jiggleMax);
     private static Vector3 UR = new Vector3( jiggleMax,  jiggleMax);
     private static Vector3 DR = new Vector3( jiggleMax, -jiggleMax);
     private static Vector3 DL = new Vector3(-jiggleMax, -jiggleMax);


     void Awake()
     {
          attackController = GetComponent<AttackController>();
          animator = GetComponent<Animator>();

          isMoving = false;
          isDashing = false;
          movementVector = new Vector2(0, 0);
          facing = new Vector2(0, -1);

          canDash = true;
          canMove = true;
          dashIn = 0;
          IsInMenu = false;

          knockbackForce = 8;
          isKnockedBack = false;
          timeSpentKnockedBack = 0;
          knockBackTime = 0.12f;
          GameManager.Notifications.AddListener(this, "PlayerExitMenu");
          GameManager.Notifications.AddListener(this, "PlayerInMenu");
     }

     // Update is called once per frame
     void Update()
     {
          if (IsInMenu)
          {
               return;
          }

          if (Time.deltaTime == 0)
          {
               //Game is paused.
               return;
          }

          //Subtract the cooldown for dashing and check when the player can dash again and whether or not its finished dashing
          dashIn -= Time.deltaTime;

          //The player can move after the dash cool down
          if (dashIn < -0.1)
          {
               canDash = true;
               isDashing = false;
               dashCooldown = false;
          }

          //Dash Cooldown
          if (dashIn < 0.1)
          {
               dashCooldown = false;
               ToWalkPhysics();
          }

          if (dashParticleInstance != null)
          {
               dashParticleInstance.transform.position = DashParticlePosition();
          }

          //The player can move after the dash cool down
          if (dashIn < -0.3)
          {
               canDash = true;
               isDashing = false;
               dashCooldown = false;
          }

          //Check the players state. If its already doing something, prevent the player from being able to move
          canMove = attackController.isAttacking == false &&
                    dashCooldown == false;

          // Calculate movement amount
          if (isDashing)
          {
               // Use facing so that we can't dash diagonally.
               movementVector = facing * dashSpeed;
          }
          else
          {
               movementVector = direction * speed;
          }

          isMoving = movementVector != Vector2.zero;

          //Change the movement vector to the knockbackvector if they are being knocked back
          if (isKnockedBack)
          {
               timeSpentKnockedBack += Time.deltaTime;
               movementVector = knockbackDirection * knockbackForce;
               if (timeSpentKnockedBack >= knockBackTime)
               {
                    isKnockedBack = false;
                    timeSpentKnockedBack = 0;
               }
          }

          if (animator != null)
          {
               animator.SetFloat("facing_x", facing.x);
               animator.SetFloat("facing_y", facing.y);
               animator.SetFloat("movement_x", movementVector.x);
               animator.SetFloat("movement_y", movementVector.y);
               //Play walking animations
               animator.SetBool("IsMoving", isMoving);
               //Play dashing animations
               animator.SetBool("IsDashing", isDashing);
          }
     }

     void FixedUpdate()
     {
          // Apply the movement to the rigidbody
          //rigidbody2D.AddForce(movementVector);

          if (canMove)
          {
               GetComponent<Rigidbody2D>().velocity = JiggleMovement();
          }

          //Debug.Log("canDash:" + canDash + "   canAttack:" + attackController.CanAttack());
          //Debug.Log("speed:" + speed + "direction:" + direction + "movementVector" + movementVector);
     }

     /* Move around obstacles in the tile map if they are small */
     private Vector2 JiggleMovement()
     {
          PhysicCharBehaviour physics = GetComponent<PhysicCharBehaviour>();

          switch (physics.CollFlags)
          {
          case PhysicCharBehaviour.eCollFlags.LEFT:
               if (!physics.IsColliding(transform.position + UL))
               {
                    return Vector2.up * speed;
               }
               else if (!physics.IsColliding(transform.position + DL))
               {
                    return Vector2.up * speed * -1;
               }
               break;
          case PhysicCharBehaviour.eCollFlags.RIGHT:
               if (!physics.IsColliding(transform.position + UR))
               {
                    return Vector2.up * speed;
               }
               else if (!physics.IsColliding(transform.position + DR))
               {
                    return Vector2.up * speed * -1;
               }
               break;
          case PhysicCharBehaviour.eCollFlags.UP:
               if (!physics.IsColliding(transform.position + UL))
               {
                    return -1 * Vector2.right * speed;
               }
               else if (!physics.IsColliding(transform.position + UR))
               {
                    return -1 * Vector2.right * speed * -1;
               }
               break;
          case PhysicCharBehaviour.eCollFlags.DOWN:
               if (!physics.IsColliding(transform.position + DL))
               {
                    return -1 * Vector2.right * speed;
               }
               else if (!physics.IsColliding(transform.position + DR))
               {
                    return -1 * Vector2.right * speed * -1;
               }
               break;
          }
          return movementVector;
     }

     /* Moves the object in a direction by a small amount (used for player input) */
     internal void Move(float input_X, float input_Y)
     {
          if (canMove == false)
          {
               return;
          }
          
          direction = new Vector2(input_X, input_Y);
     }

     /* Moves the object towards a destination by a small amount (used for enemy input)*/
     internal void Move(Vector2 _direction)
     {
          if (canMove == false)
          {
               return;
          }
          
          direction = _direction;
     }

     internal void Face(Vector2 _facing)
     {
          if (canMove == false)
          {
               return;
          }

          facing = _facing;

          // Check whether sprite is facing left or right. Flip the sprite based on its direction
          if (facing.x > 0)
          {
               transform.localScale = new Vector3(1, 1, 1);
          }
          else if (facing.x < 0)
          {
               transform.localScale = new Vector3(-1, 1, 1);
          }
     }

     public void Dash()
     {
          // Only let the player dash if the cooldown is < 0. If he can, dash and reset the timer       
          if (canMove == false || canDash == false || IsDashUnlocked == false)
          {
               return;
          }

          // Change these rigidbody parameters so the dashing feels better
          GameManager.Notifications.PostNotification(this, "OnPlayerDash");
          ToDashPhysics();
          GetComponent<Rigidbody2D>().velocity = facing * (dashSpeed + DashSpeedUpgrade);

          // Instantiate particle effects if they exist
          if (dashParticle != null)
          {
               Vector3 position = DashParticlePosition();
               Quaternion rotation = DashParticleRotation();
               dashParticleInstance = Instantiate(dashParticle, position, rotation) as ParticleSystem;
          }

          //Reset dash parameters
          dashIn = .25f;
          isMoving = true;
          isDashing = true;
          canDash = false;
          canMove = true;
     }

     private Vector3 DashParticlePosition()
     {
          float x = transform.position.x;
          float y = transform.position.y;
          float z = transform.position.z;
          return new Vector3(x + facing.x * -0.15f, y - 0.15f, z);
     }

     private Quaternion DashParticleRotation()
     {
          if (facing == Vector2.up)
          {
               return Quaternion.Euler(90, -90, 90);
          }
          else if (facing == Vector2.right)
          {
               return Quaternion.Euler(0, -90, 90);
          }
          else if (facing == -1 * Vector2.up)
          {
               return Quaternion.Euler(-90, 0, 0);
          }
          else
          {
               return Quaternion.Euler(0, 90, -90);
          }
     }

     public void Knockback(Vector2 direction)
     {
          isKnockedBack = true;
          knockbackDirection = direction;
     }

     public void GotAttacked()
     {
          gotAttacked = true;
     }

     // Gets called in the end of the animation
     public void FinishedGettingAttacked()
     {
          gotAttacked = false;
     }

     public void ToDashPhysics()
     {
          GetComponent<Rigidbody2D>().mass = 1;
          GetComponent<Rigidbody2D>().drag = 33;
     }

     public void ToWalkPhysics()
     {
          GetComponent<Rigidbody2D>().drag = 75;
          GetComponent<Rigidbody2D>().mass = 2;
     }

     public float getDashSpeed()
     {
          return DashSpeedUpgrade;
     }

     public void setDashSpeed(float newDashSpeed)
     {
          DashSpeedUpgrade += newDashSpeed;
     }

     public void SetDashLockState(bool value)
     {
          IsDashUnlocked = value;
     }

     public bool GetDashLockState()
     {
          return IsDashUnlocked;
     }

     public void PlayerExitMenu()
     {
          IsInMenu = false;
     }

     public void PlayerInMenu()
     {
          canMove = false;
          IsInMenu = true;
     }
}
