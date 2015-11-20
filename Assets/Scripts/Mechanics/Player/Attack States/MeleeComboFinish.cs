using UnityEngine;
using System.Collections;

public class MeleeComboFinish : IAttack
{

    public float LengthOfAttack = 1;
    private float timer = 0;
    public IAttack HandleInput(Player player)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            return new MeleeBasicAttack();
        }
        if (timer > LengthOfAttack)
        {
            return new IdleAttackState();
        }
        return null;
    }

    public void EnterState(Player player)
    {
        player.animator.SetBool("IsComboTriggered", true);
        player.GetAttackCollider().SetActive(true);

    }

    public void UpdateState(Player player)
    {
        timer += Time.unscaledDeltaTime;
    }

    public void ExitState(Player player)
    {
        player.animator.SetBool("IsComboTriggered", false);
        player.GetAttackCollider().SetActive(false);
    }
}
