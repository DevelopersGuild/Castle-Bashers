using UnityEngine;
using System.Collections;


public class ShockSkill : Skill
{
    private GameObject shockField;
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(5, 16000, 85, "Shock", SkillLevel.EnemyOnly);
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/shock"));
    }
    protected override void Update()
    {
        base.Update();

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        Debug.Log("Using shock!");
        base.UseSkill(caller, target, optionalParameters);
        shockField = Instantiate(Resources.Load("shockField")) as GameObject;
        shockField.transform.position = caller.transform.position;
        Destroy(shockField, 0.75f);


    }
}
