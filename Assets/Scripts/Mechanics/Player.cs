using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float horizontalMoveSpeed = 6;
    public float verticalMoveSpeed = 10;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    float velocityZSmoothing;

    MoveController controller;

    void Start()
    {
        controller = GetComponent<MoveController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * horizontalMoveSpeed;
        float targetVelocityZ = input.y * verticalMoveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
