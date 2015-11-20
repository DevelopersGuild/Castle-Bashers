using UnityEngine;
using System.Collections;


public class Ignite : Skill
{
    public float aoeRadius = 10;
    float damage = 15;
    protected override void Start()
    {
        base.Start();

        base.SetBaseValues(4, 16000, 150, "Ignite", SkillLevel.Level1);

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
            Debug.Log("Found " + col.gameObject.name);
            if (col.gameObject.GetComponent<Burn>())
            {
                Debug.Log("Dealt " + damage + " to " + col.gameObject.name);
                col.gameObject.GetComponent<Defense>().CheckMagicalDefense(damage);
            }
        }

    }
}
