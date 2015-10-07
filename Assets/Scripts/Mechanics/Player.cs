﻿using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{

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

     private bool canAct = true;
     private bool isInvincible = false;

     float gravity;
     float jumpVelocity;
     Vector3 velocity;
     float velocityXSmoothing;
     float velocityZSmoothing;

     MoveController controller;
     PlayerHealth hp;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;
    private Rewired.Player playerRewired;
    public int playerId; // The Rewired player id of this character

    void Start()
     {
          hp = GetComponent<PlayerHealth>();
          controller = GetComponent<MoveController>();
          knockReset = hitReset = 0;
          gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
          jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
          print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        playerRewired = ReInput.players.GetPlayer(playerId);
        Debug.Log("unit");
        initialized = true;
    }

    void Update()
     {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor

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

          controller.checkKnock();
     
          velocity.y += gravity * Time.deltaTime;
          Vector2 input = new Vector2(playerRewired.GetAxis("MoveHorizontal"), playerRewired.GetAxis("MoveVertical"));
          
          hp.setKnock(true);
          if (controller.collisions.above || controller.collisions.below)
          {
               velocity.y = 0;
          }

          if ((playerRewired.GetButton("Jump")) && controller.collisions.below && canAct)
          {
               velocity.y = jumpVelocity;
          }


          if (canAct)
          {
               float targetVelocityX = input.x * horizontalMoveSpeed;
               float targetVelocityZ = input.y * verticalMoveSpeed;
               velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
               velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
          }

          controller.Move(velocity * Time.deltaTime, input);

          knockReset += Time.deltaTime;
          hitReset += Time.deltaTime;
     }

     public void Knock(Vector3 dir, float amt, float force)
     {
          kbDir = new Vector3(dir.x, dir.y -0.3f, dir.z);
          kbCounter += amt;
          kbForce = force;
          knockReset = 0;
     }

     //Reset hitReset when hit

     public void setAct(bool x)
     {
          canAct = x;
     }

     public bool getInvincible()
     {
          return isInvincible;
     }

     public void setInvincible(bool x)
     {
          isInvincible = x;
     }
}
