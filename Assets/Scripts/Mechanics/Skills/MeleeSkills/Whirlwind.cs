using UnityEngine;
using System.Collections;

public class Whirlwind : Skill
{
    private int price;
    private float cooldown;
    GameObject whirlwind;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1000, 10, "Whirlwind", SkillLevel.Level1);
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        whirlwind = Instantiate(Resources.Load("Whirlwind")) as GameObject;
        whirlwind.transform.position = caller.transform.position;
        whirlwind.transform.parent = caller.transform;
        Destroy(whirlwind, 2.5f);
    }


}
