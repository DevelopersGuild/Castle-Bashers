using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveController : MonoBehaviour
{
    public LayerMask collisionMask;

    private bool facingRight = true;

    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    private int count = 0;
    public int verticalRayCount = 4;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    private bool isKnockedBack;
    public float knockbackVelocity;
    public float knockbackTime;
    private float currentKnockbacktime;
    public bool isStunned;

    private Player player;

    [HideInInspector]
    public Vector2 playerInput;
    private Vector2 noMovement = new Vector2(0, 0);

    BoxCollider coll;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    void Start()
    {
        isStunned = false;
        player = GetComponent<Player>();
        coll = GetComponent<BoxCollider>();
        CalculateRaySpacing();
        currentKnockbacktime = knockbackTime;
    }


    public float GetFacing()
    {
        if (facingRight)
            return -1;

        return 1;
    }

    public void Move(Vector3 velocity, Vector2 input = default(Vector2))
    {
        UpdateRaycastOrigins();
        playerInput = input;
        collisions.Reset();

        if (!isKnockedBack)
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

        HandleKnockback(ref velocity);

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.z != 0)
        {
            DepthCollisions(ref velocity);
        }

        transform.Translate(velocity);
       
    }

    private void HandleKnockback(ref Vector3 velocity)
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

    public void SetKnockback(bool knockback)
    {
        isKnockedBack = knockback;
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

