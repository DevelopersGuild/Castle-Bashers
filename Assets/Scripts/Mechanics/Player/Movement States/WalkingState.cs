using UnityEngine;
using System.Collections;

public class WalkingState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.animator.SetBool("IsMoving", true);
    }

    public IPlayerState HandleInput(Player player)
    {
        if(player.GetComponent<MoveController>().GetKnockedBack() == true)
        {
            return new KnockedBackState();
        }
        if (player.playerRewired.GetButtonDown("Jump") && player.GetMoveController().collisions.below)
        {
            return new JumpState();
        }
        if(player.GetIsMoving() == false)
        {
            return new StandingState();
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
