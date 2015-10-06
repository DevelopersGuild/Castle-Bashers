using UnityEngine;
using System.Collections;

public class StandingState : IPlayerState
{
    public void EnterState(Player player)
    {

    }

    public IPlayerState HandleInput(Player player)
    {
        if(Input.GetButtonDown("Jump"))
        {
            return new JumpState();
        }
        return null;
    }

    public void UpdateState(Player player)
    {

    }
}
