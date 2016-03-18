using UnityEngine;
using System.Collections;

public class sMeteor : Skill
{
    private int damage;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(20, 1500, 10, "Meteor", SkillLevel.Level1);
        damage = 15;
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        GameObject projectile = Instantiate(Resources.Load("Meteor")) as GameObject;
        projectile.transform.position = target.transform.position + new Vector3(3, 6, 0);
        if (gameObject.GetComponent<Player>())
        {
            projectile.GetComponent<DealDamage>().damagesPlayers = false;
            projectile.GetComponent<DealDamage>().damagesEnemies = true;
            projectile.GetComponent<MeteorMover>().faceLeft();
        }
        else
        {

            projectile.GetComponent<DealDamage>().damagesPlayers = true;
            projectile.GetComponent<DealDamage>().damagesEnemies = false;
        }
            

    }

    public override void addLevel()
    {
        base.addLevel();
        switch (level)
        {
            case 2:
                damage = 45;
                manaCost = 30;
                break;
            case 3:
                damage = 80;
                manaCost = 50;
                break;
        }
    }
}