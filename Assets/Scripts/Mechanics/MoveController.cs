using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveController : MonoBehaviour
{
    public LayerMask collisionMask;
    public AudioClip walkSound;
    private AudioSource source;
    private CrowdControllable crowdControllable;
    private Animator animator;

    private bool facingRight = true;
    public bool isMoving;

    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    private int count = 0;
    public int verticalRayCount = 4;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    // TODO: Make these things private after testing
    public bool isKnockbackable, isFlinchable;
    private bool isKnockedBack, isFlinched;
    private bool isKnockedDown;
    public float knockbackVelocity;
    public float flinchVelocity;
    private int flinchCount;
    private float knockbackTime = .075f, flinchTime = 0.75f;
    private float currentKnockbacktime, currentFlinchTime;
    public bool isStunned;

    private float height;
    private float screenWidthInPoints;

    private Player player;

    [HideInInspector]
    public Vector2 playerInput;
    private Vector2 noMovement = new Vector2(0, 0);
    private bool isMovementDisabled;

    BoxCollider coll;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;
    private bool isGrounded;

    void Start()
    {
        flinchCount = 0;
        isStunned = false;
        player = GetComponent<Player>();
        coll = GetComponent<BoxCollider>();
        crowdControllable = GetComponent<CrowdControllable>();
        animator = GetComponent<Animator>();

        CalculateRaySpacing();
        currentKnockbacktime = knockbackTime;
        currentFlinchTime = flinchTime;
        height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        source = new AudioSource();
        source = GetComponent<AudioSource>();
        isMovementDisabled = false;
    }

    public float GetFacing()
    {
        if (facingRight)
            return -1;

        return 1;
    }

    // true for right, false for left
    public void OrientFacingLeft(bool set, float lookDir)
    {
        float temp = 1;
        if (!set)
            temp = -1;

        if (lookDir != temp)
        {
            facingRight = !set;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

    }

    public bool getCanMove()
    {
        return isMovementDisabled;
    }

    public void Move(Vector3 velocity, Vector2 input = default(Vector2))
    {
        // Debug.Log(isMovementDisabled + " " + isKnockedDown + " " + gameObject.name);

        if (!isMovementDisabled && !isKnockedDown)
        {
            UpdateRaycastOrigins();
            playerInput = input;
            collisions.Reset();

            if (!isKnockedBack || !isKnockedDown)
            {
                if (velocity.x < 0 && facingRight)
                {
                    Flip();
                }
                else if (velocity.x > 0 && !facingRight)
                {
                    Flip();
                }
            }
            if (velocity.y != 0)
            {
                VerticalCollisions(ref velocity);
            }

            updateGrounded();
            updateKnockback(ref velocity);
            updateFlinch(ref velocity);
            // updateKnockedDown();

            if (velocity.x != 0)
            {
                HorizontalCollisions(ref velocity);
            }

            if (velocity.z != 0)
            {
                DepthCollisions(ref velocity);
            }

            // Only move if the player isnt flinched or knocked down
            // if (!isFlinched && !isKnockedDown)
            transform.Translate(velocity);

            clampPosition(ref velocity);

            if (velocity.x == 0 && velocity.z == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
        }
        updateKnockedDown();
    }

    private void updateGrounded()
    {
        if (collisions.below)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void disableMovement()
    {
        isMovementDisabled = true;
    }

    public void enableMovement()
    {
        isMovementDisabled = false;
    }

    private void clampPosition(ref Vector3 veloctity)
    {
        if (GetComponent<Player>())
        {
            if (transform.position.x > Camera.main.transform.position.x + screenWidthInPoints / 2 || transform.position.x < Camera.main.transform.position.x - screenWidthInPoints / 2)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, Camera.main.transform.position.x - screenWidthInPoints / 2, Camera.main.transform.position.x + (screenWidthInPoints / 2)), transform.position.y, transform.position.z);
            }
        }
    }

    private void updateKnockback(ref Vector3 velocity)
    {
        if (isKnockbackable)
        {
            if (isKnockedBack)
            {
                isStunned = true;

                if (!facingRight)
                {
                    velocity.x = knockbackVelocity;
                }
                else
                {
                    velocity.x = -knockbackVelocity;
                }

                if (GetComponent<ID>() && !GetComponent<Player>())
                {
                    if (GetComponent<ID>().getTime())
                        currentKnockbacktime -= Time.unscaledDeltaTime;
                }
                else
                {
                    currentKnockbacktime -= Time.deltaTime;
                }

                // Stop pushing the player after knockbacktime and after hes hit the floor
                if (currentKnockbacktime <= 0 && collisions.below == true)
                {
                    isStunned = false;
                    isKnockedBack = false;
                    currentKnockbacktime = knockbackTime;
                }
            }
        }
    }

    private void updateKnockedDown()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("KnockedDown"))
        {
            isKnockedDown = true;
            isStunned = true;
        }
        else if (isKnockedDown)
        {
            isKnockedDown = false;
            isStunned = false;
        }
    }

    private void updateFlinch(ref Vector3 velocity)
    {
        if (isFlinched)
        {

            isStunned = true;

            if (!facingRight)
            {
                velocity.x = flinchVelocity;
            }
            else
            {
                velocity.x = -flinchVelocity;
            }

            // Knockback
            if (flinchCount >= 10)
            {
                resetFlinchCount();
                isKnockedBack = true;
            }

            //if (GetComponent<ID>() && !GetComponent<Player>())
            //{
            //    if (GetComponent<ID>().getTime())
            //        currentFlinchTime -= Time.unscaledDeltaTime;
            //}
            //else
                currentFlinchTime -= Time.deltaTime;

            Debug.Log(currentFlinchTime);

            // Stop flinching after timer has passed
            if (currentFlinchTime <= 0) // && collisions.below == true)
            {
                resetFlinchCount();
            }
        }
    }

    public void handleFlinch(int flinchPower)
    {
        if (isFlinchable)
        {
            isFlinched = true;
            flinchCount += flinchPower;
            resetToFlinchTime();
        }
    }

    private void resetToFlinchTime()
    {
        currentFlinchTime = flinchTime;
    }

    private void resetFlinchCount()
    {
        flinchCount = 0;
        isFlinched = false;
        isStunned = false;
    }

    public void SetKnockback(bool knockback)
    {
        isKnockedBack = knockback;
    }

    public bool GetKnockedBack()
    {
        return isKnockedBack;
    }

    public void SetFlinch(bool flinch)
    {
        isFlinched = flinch;
    }

    public bool GetFlinched()
    {
        return isFlinched;
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector3 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector3.up * (horizontalRaySpacing * i);
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(rayOrigin, Vector3.right * directionX, out hitInfo, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector3.right * directionX * rayLength, Color.red);
            if (hit)
            {
                velocity.x = (hitInfo.distance - skinWidth) * directionX;
                rayLength = hitInfo.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }


    }

    void DepthCollisions(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        float rayLength = Mathf.Abs(velocity.z) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionZ == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomLeft;
            rayOrigin += Vector3.right * (verticalRaySpacing * i);
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(rayOrigin, Vector3.forward * directionZ, out hitInfo, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector3.forward * directionZ * rayLength, Color.red);
            if (hit)
            {
                velocity.z = (hitInfo.distance - skinWidth) * directionZ;
                rayLength = hitInfo.distance;

                collisions.backward = directionZ == -1;
                collisions.forward = directionZ == 1;
            }
        }

    }

    void VerticalCollisions(ref Vector3 velocity)
    {

        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector3.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(rayOrigin, Vector2.up * directionY, out hitInfo, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector3.up * directionY * rayLength, Color.red);
            if (hit)
            {
                velocity.y = (hitInfo.distance - skinWidth) * directionY;
                rayLength = hitInfo.distance;
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector3(bounds.min.x, bounds.min.y, transform.position.z);
        raycastOrigins.bottomRight = new Vector3(bounds.max.x, bounds.min.y, transform.position.z);
        raycastOrigins.topLeft = new Vector3(bounds.min.x, bounds.max.y, transform.position.z);
        raycastOrigins.topRight = new Vector3(bounds.max.x, bounds.max.y, transform.position.z);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void playWalkSound()
    {
        if (walkSound)
        {
            source.pitch = Random.Range(0.4f, .6f);
            source.clip = walkSound;
            source.Play();
        }
    }

    struct RaycastOrigins
    {
        public Vector3 topLeft, topRight;
        public Vector3 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool forward, backward;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            forward = backward = false;
        }
    }

}

