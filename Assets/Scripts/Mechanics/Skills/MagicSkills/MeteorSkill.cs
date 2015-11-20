using UnityEngine;
using System.Collections;

public class sMeteor : Skill
{

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1500, 10, "Meteor", SkillLevel.Level1);
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        GameObject projectile = Instantiate(Resources.Load("Meteor")) as GameObject;
        projectile.transform.position = target.transform.position + new Vector3(3, 6, 0);

    }
}