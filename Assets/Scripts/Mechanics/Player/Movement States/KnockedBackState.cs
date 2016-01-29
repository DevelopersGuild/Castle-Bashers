using UnityEngine;
using System.Collections;

public class KnockedBackState : IPlayerState
{
    float timer = 0;
    public void EnterState(Player player)
    {
        // player.animator.SetBool("IsKnockedBack", true);
    }

    public IPlayerState HandleInput(Player player)
    {
        if (timer <= 0)
        {
            return new KnockedDownState();
        }
        return null;
    }

    public void UpdateState(Player player)
    {
        timer = timer - Time.unscaledDeltaTime;
    }

    public void ExitState(Player player)
    {
        // player.animator.SetBool("IsKnockedBack", false);
    }
}
