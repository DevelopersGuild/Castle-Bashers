using UnityEngine;
using System.Collections;

public class MeleeComboStart : IAttack
{

    public float LengthOfAttack = 0.5f;
    private float timer = 0;
    public IAttack HandleInput(Player player)
    {
        if (Input.GetButtonDown("Fire1"))
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
        player.animator.SetBool("IsUsingBasicAttack", true);
        player.GetAttackCollider().SetActive(true);

    }

    public void UpdateState(Player player)
    {
        timer += Time.deltaTime;
    }

    public void ExitState(Player player)
    {
        player.animator.SetBool("IsUsingBasicAttack", false);
        player.GetAttackCollider().SetActive(false);
    }
}
