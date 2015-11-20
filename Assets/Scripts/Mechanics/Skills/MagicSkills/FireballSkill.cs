using UnityEngine;
using System.Collections;

public class FireballSkill : Skill
{
    private GameObject fireBall;
    protected override void Start()
    {
        base.Start();

        base.SetBaseValues(5, 16000, 85, "fireBall", SkillLevel.EnemyOnly);
    }
    protected override void Update()
    {
        base.Update();

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        Debug.Log("Using Fireball!");
        base.UseSkill(caller, target, optionalParameters);
        fireBall = Instantiate(Resources.Load("FireBall")) as GameObject;
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
}
