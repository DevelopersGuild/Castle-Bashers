using UnityEngine;
using System.Collections;

public class StandingState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.animator.SetBool("IsMoving", false);
    }

    public IPlayerState HandleInput(Player player)
    {
        if(player.playerRewired.GetButtonDown("Jump") && player.GetMoveController().collisions.below)
        {
            return new JumpState();
        }
        if(player.GetIsMoving() == true)
        {
            return new WalkingState();
        }
        if (player.GetComponent<Health>().GetIsPlayerDown() == true)
        {
            return new DeathState();
        }
            return null;
    }

    public void UpdateState(Player player)
    {

    }

    public void ExitState(Player player)
    {

    }
}
