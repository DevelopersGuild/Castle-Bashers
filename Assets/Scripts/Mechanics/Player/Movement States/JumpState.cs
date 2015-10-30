﻿using UnityEngine;
using System.Collections;
using System;

public class JumpState : IPlayerState
{

    public void EnterState(Player player)
    {
        if (player.GetMoveController().isStunned == false)
        {
            player.Jump();
            player.SetIsGrounded(false);
            player.animator.SetBool("IsJumping", true);
            player.animator.SetBool("IsGrounded", false);
       }
    }

    public IPlayerState HandleInput(Player player)
    {
        if(player.GetIsGrounded() == true)
        {
            return new StandingState();
        }
        return null;
    }

    public void UpdateState(Player player)
    {
        if (player.GetMoveController().collisions.above || player.GetMoveController().collisions.below)
        {
            player.SetIsGrounded(true);
            player.animator.SetBool("IsJumping", false);
        }
    }

    public void ExitState(Player player)
    {
        player.EndJump();
    }




}
