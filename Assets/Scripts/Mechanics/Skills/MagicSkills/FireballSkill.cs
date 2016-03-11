using UnityEngine;
using System.Collections;

public class FireballSkill : Skill
{
    private GameObject fireBall;
    private float damage;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(5, 5000, 5, "fireBall", SkillLevel.EnemyOnly);
        damage = 15;
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/fireball"));
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("g"))
        {
            Debug.Log("Name of fireball object: " + gameObject.name);
            Debug.Log("Fireball position: " + gameObject.transform.position);
            UseSkill(gameObject);
        }

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        Debug.Log("Using Fireball!");
        base.UseSkill(caller, target, optionalParameters);
        fireBall = Instantiate(Resources.Load("Fireball")) as GameObject;   //was FireBall
        fireBall.GetComponent<DealDamage>().setDamage(damage);
        fireBall.GetComponent<DealDamage>().damagesEnemies = true;
        fireBall.transform.position = caller.transform.position;
        if (GetComponent<MoveController>().GetFacing() == -1)
        {
            fireBall.GetComponent<SimpleObjectMover>().right = true;
            fireBall.GetComponent<SimpleObjectMover>().left = false;
        }
        else
        {
            fireBall.GetComponent<SimpleObjectMover>().right = false;
            fireBall.GetComponent<SimpleObjectMover>().left = true;
        }
        Destroy(fireBall, 0.75f);


    }

    public override void addLevel()
    {
        base.addLevel();
        switch (level)
        {
            case 2:
                manaCost = 10;
                damage = 30;
                break;
            case 3:
                manaCost = 20;
                damage = 75;
                break;
        }
    }
}
