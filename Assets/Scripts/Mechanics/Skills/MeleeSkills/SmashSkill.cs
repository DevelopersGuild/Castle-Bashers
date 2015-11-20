using UnityEngine;
using System.Collections;

public class SmashSkill : Skill
{
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1000, 10, "Smash", SkillLevel.Level1);
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
    }

}
