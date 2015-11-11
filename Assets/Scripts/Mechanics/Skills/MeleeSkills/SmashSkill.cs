using UnityEngine;
using System.Collections;

public class SmashSkill : Skill
{
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1000, 10, "Health Regen");
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        Smash();
    }

    protected override void Update()
    {
        base.Update();
        if (gameObject.GetComponent<SphereCollider>() != null)
        {
            gameObject.GetComponent<SphereCollider>().radius += 10 * Time.deltaTime;
        }
    }

    private void Smash()
    {
        gameObject.AddComponent<SphereCollider>();
        
    }
}
