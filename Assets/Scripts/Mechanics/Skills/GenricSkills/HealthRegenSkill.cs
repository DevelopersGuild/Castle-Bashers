﻿using UnityEngine;
using System.Collections;

public class HealthRegenSkill : Skill
{

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1000, 10, "Health Regen", SkillLevel.Level1);
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/healthregen"));
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        AddHealth(caller);
    }

    private void AddHealth(GameObject caller)
    {
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetMaxHP() / 4);
    }
}
