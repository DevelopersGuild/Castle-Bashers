using UnityEngine;
using System.Collections;
using System;

public class ClawAttack : Skill
{
    public ExtendBehaviour claw1, claw2, claw3, claw4;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1000, 10, "ClawAttack", SkillLevel.Level1);
    }

    public ClawAttack()
    {

    }

    public ClawAttack(ExtendBehaviour c1, ExtendBehaviour c2, ExtendBehaviour c3, ExtendBehaviour c4)
    {
        claw1 = c1;
        claw2 = c2;
        claw3 = c3;
        claw4 = c4;
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        float dir = caller.GetComponent<Malady>().getDirection();
        ExtendBehaviour c1 = Instantiate(claw1, caller.transform.position + new Vector3(0, 0, -1.62f), claw1.transform.rotation) as ExtendBehaviour;
        c1.direction = dir;
        ExtendBehaviour c2 = Instantiate(claw2, caller.transform.position + new Vector3(0, 0, 0.65f), claw2.transform.rotation) as ExtendBehaviour;
        c2.direction = dir;
        ExtendBehaviour c3 = Instantiate(claw3, caller.transform.position + new Vector3(0, 0, 0.81f), claw3.transform.rotation) as ExtendBehaviour;
        c3.direction = dir;
        ExtendBehaviour c4 = Instantiate(claw4, caller.transform.position + new Vector3(0, 0, 1.42f), claw4.transform.rotation) as ExtendBehaviour;
        c4.direction = dir;
    }
}


