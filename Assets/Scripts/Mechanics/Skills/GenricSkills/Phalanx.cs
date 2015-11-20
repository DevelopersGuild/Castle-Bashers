using UnityEngine;
using System.Collections;

public class Phalanx : Skill
{
    private float addedPhysicalDefense;
    private float addedMagicDefense;
    private Player player;
    private Defense defense;

    private float duration = 10f;
    private float expiration;
    bool active = false;
    protected override void Start()
    {
        base.Start();

        base.SetBaseValues(15, 16000, 150, "Phalanx", SkillLevel.Level1);
        player = GetComponent<Player>();
        defense = GetComponent<Defense>();
    }
    protected override void Update()
    {
        base.Update();
        if (active && Time.time >= expiration)
        {
            defense.SetPhysicalDefense(defense.GetPhysicalDefense() - addedPhysicalDefense);
            defense.SetMagicalDefense(defense.GetMagicalDefense() - addedMagicDefense);
            active = false;
        }

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        addedPhysicalDefense = player.GetIntelligence() / 2;
        addedMagicDefense = player.GetStrength() / 2;
        defense.SetPhysicalDefense(defense.GetPhysicalDefense() + addedPhysicalDefense);
        defense.SetMagicalDefense(defense.GetMagicalDefense() + addedMagicDefense);
        expiration = Time.time + duration;
        active = true;

    }
}
