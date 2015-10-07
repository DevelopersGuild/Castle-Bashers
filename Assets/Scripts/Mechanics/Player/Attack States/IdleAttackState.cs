using UnityEngine;
using System.Collections;

public class IdleAttackState : IAttack
{
    public IAttack HandleInput(Player player)
    {
        if(Input.GetButtonDown("Fire1"))
        {
            return new MeleeBasicAttack();
        }
        return null;
    }

    public void EnterState(Player player)
    {

    }

    public void UpdateState(Player player)
    {

    }

    public void ExitState(Player player)
    {

    }
}
