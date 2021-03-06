﻿using UnityEngine;
using System.Collections;

public class MeleeComboStart : IAttack
{

    public float LengthOfAttack = 0.5f;
    private float timer = 0;
    public IAttack HandleInput(Player player)
    {
        if (player.playerRewired.GetButtonDown("Fire1"))
        {
            return new MeleeComboFinish();
        }
        if (timer > LengthOfAttack)
        {
            return new IdleAttackState();
        }
        return null;
    }

    public void EnterState(Player player)
    {

    }

    public void UpdateState(Player player)
    {
        timer += Time.unscaledDeltaTime;
    }

    public void ExitState(Player player)
    {

    }
}
