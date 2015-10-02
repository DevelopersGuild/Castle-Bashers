using UnityEngine;
using System.Collections;
using System;

public class JumpState : IPlayerState
{

    public void EnterState(Player player)
    {

    }

    public IPlayerState HandleInput(Player player, Input input)
    {
        //Change this later
        return new JumpState();
    }

    public void UpdateState(Player player)
    {

    }




}
