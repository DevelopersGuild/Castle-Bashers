using UnityEngine;
using System.Collections;
using System;

public class JumpState : IPlayerState
{

    public void EnterState(Player player)
    {
        if (!player.GetMoveController().isStunned && !player.attackController.GetIsAttack())
        {
            player.Jump();
            //player.SetIsGrounded(false);
            
            //player.animator.SetBool("IsJumping", true);
            //player.animator.SetBool("IsGrounded", false);
       }
    }

    public IPlayerState HandleInput(Player player)
    {
        if(player.GetMoveController().GetIsGrounded() == true)
        {
            return new StandingState();
        }
        return null;
    }

    public void UpdateState(Player player)
    {
        if (player.GetMoveController().collisions.below)
        {
            player.EndJump();
            player.attackController.resetTap();
            player.animator.SetBool("JumpAttack", false);
            player.attackController.ResetTap();
        }
    }

    public void ExitState(Player player)
    {
        player.EndJump();
    }




}
