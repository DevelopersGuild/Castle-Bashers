using UnityEngine;
using System.Collections;


public class IgniteSkill : Skill
{
    public float aoeRadius = 10;
    float damage = 15;
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(4, 16000, 150, "Ignite", SkillLevel.Level1);
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/Ignite"));

    }
    protected override void Update()
    {
        base.Update();


    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, aoeRadius);
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.GetComponent<Burn>())
            {
                col.gameObject.GetComponent<Defense>().CheckMagicalDefense(damage);
            }
        }

    }
}
