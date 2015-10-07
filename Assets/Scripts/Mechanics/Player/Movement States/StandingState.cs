using UnityEngine;
using System.Collections;

public class StandingState : IPlayerState
{
    public void EnterState(Player player)
    {

    }

    public IPlayerState HandleInput(Player player)
    {
        if(Input.GetButtonDown("Jump") && player.GetMoveController().collisions.below)
        {
            return new JumpState();
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
