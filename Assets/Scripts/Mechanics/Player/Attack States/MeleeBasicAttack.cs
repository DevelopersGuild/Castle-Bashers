using UnityEngine;
using System.Collections;

public class MeleeBasicAttack : IAttack
{
    public float LengthOfAttack = 2.0f;
    private float timer = 1.0f;
    public IAttack HandleInput(Player player)
    {
        if(Input.GetButtonDown("Fire1"))
        {
            return new MeleeBasicAttack();
        }
        if(timer > LengthOfAttack)
        {
            return new IdleAttackState();
        }
        return null;
    }

    public void EnterState(Player player)
    {
        player.animator.SetBool("IsUsingBasicAttack", true);
    }

    public void UpdateState(Player player)
    {
        timer += Time.deltaTime;
    }

    public void ExitState(Player player)
    {
        player.animator.SetBool("IsUsingBasicAttack", false);
    }
}
