using UnityEngine;
using System.Collections;

public class Skill_BoostV2 : Skill
{
    private GameObject call;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(15, 1000, 15, "Boost", SkillLevel.Level1);

    }

    protected override void Update()
    {
        base.Update();
        if (GetCoolDownTimer() == 0)
        {
            float f = 0.75f;
            if (augment == Augment.Purple)
                f = 0.5f;

            modAtkSpd(call, false, f);
            if (augment == Augment.Orange)
                modSpd(call, false);
            if (augment == Augment.Teal)
                modAnimSpd(call, false);

        }
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        call = caller;
        if (augment == Augment.Neutral)
        {
            modAtkSpd(caller);
        }
        else if (augment == Augment.Orange)
        {
            modAtkSpd(caller);
            modSpd(caller);

        }
        else if (augment == Augment.Purple)
        {
            modAtkSpd(caller, true, 0.5f);
        }
        else if (augment == Augment.Teal)
        {
            modAtkSpd(caller);
            modAnimSpd(caller);
        }

    }

    private void modAtkSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (!add)
            val = 1 / val;

        caller.GetComponent<Animation>().GetClip("BasicAttack").frameRate = (caller.GetComponent<Animation>().GetClip("BasicAttack").frameRate * val);
    }

    private void modAnimSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (!add)
            val = 1 / val;

        caller.GetComponent<Animator>().speed = caller.GetComponent<Animator>().speed * val;
    }

    private void modSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (add)
            val = 1 / val;

        caller.GetComponent<Player>().SetAgility(caller.GetComponent<Player>().GetAgility() * val);
    }



    private void AddHealth(GameObject caller)
    {
        //heals 1/3 of starting health, which is ~1/4 of temp max hp
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetStartingHealth() / 3);
    }
}

