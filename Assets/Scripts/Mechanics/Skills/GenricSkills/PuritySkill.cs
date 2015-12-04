using UnityEngine;
using System.Collections;

public class PuritySkill : Skill
{
    private Player player;
    private CrowdControllable crowdControllable;
    private Health health;
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(60, 16000, 150, "Purity", SkillLevel.Level1);
        player = GetComponent<Player>();
        crowdControllable = GetComponent<CrowdControllable>();
        health = GetComponent<Health>();
    }
    protected override void Update()
    {
        base.Update();

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        crowdControllable.removeSilences();
        crowdControllable.removeSlows();
        crowdControllable.removeSnares();
        crowdControllable.removeStuns();
        health.AddHealth(health.GetMaxHP() * 0.3f + player.GetStamina());
    }
}