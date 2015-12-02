using UnityEngine;
using System.Collections;

public class testSkill : Skill
{
    public float aoeRadius = 10;
    float damage = 15;
    private float amountToHeal = 0;
    public float healPercentPerTarget = 0.05f;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(4, 16000, 150, "Spirit of the Flame", SkillLevel.Level1);
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
            Debug.Log(col.gameObject.name);
            if (col.gameObject.GetComponent<Burn>())
            {
                Debug.Log("Adding % to heal");
                amountToHeal += healPercentPerTarget;
            }
        }
        float healAmount = gameObject.GetComponent<Health>().GetMaxHP() * amountToHeal;
        Debug.Log("Heal amount: " + healAmount);
        gameObject.GetComponent<Health>().AddHealth(healAmount);
    }
}



public class skilltesting : MonoBehaviour {

    float castInterval;
    
    private Skill testSkill;
    

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<SotFSkill>();

        testSkill = GetComponent<Skill>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time >= 1) 
	    if(testSkill.GetCoolDownTimer() <= 0)
        {
            testSkill.UseSkill(gameObject);

        }
	}
}
