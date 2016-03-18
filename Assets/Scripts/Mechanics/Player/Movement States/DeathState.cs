using UnityEngine;
using System.Collections;

public class DeathState : IPlayerState
{
    public void EnterState(Player player)
    {
       // player.animator.SetBool("IsDead", true);
    }

    public IPlayerState HandleInput(Player player)
    {
        return null;
    }

    public void UpdateState(Player player)
    {

    }

    public void ExitState(Player player)
    {

    }
}
