using UnityEngine;
using System.Collections;

public class KnockedDownState : IPlayerState
{
    float timer = 0;
    public void EnterState(Player player)
    {
        player.animator.SetBool("IsGrounded", true);
        timer = 1;
    }

    public IPlayerState HandleInput(Player player)
    {
        
        if (timer <= 0)
        {
            return new StandingState();
        }
        return null;
    }

    public void UpdateState(Player player)
    {
        timer = timer - Time.deltaTime;
    }

    public void ExitState(Player player)
    {
        player.animator.SetBool("IsGrounded", false);
    }
}
