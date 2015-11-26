using UnityEngine;
using System.Collections;

public class FireblastSkill : Skill
{
    private GameObject fireBlast;
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(5, 16000, 150, "FireBlast", SkillLevel.EnemyOnly);
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/FireBlast"));
    }
    protected override void Update()
    {
        base.Update();

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        Debug.Log("Using Fireblast!");
        base.UseSkill(caller, target, optionalParameters);
        fireBlast = Instantiate(Resources.Load("FireBlast")) as GameObject;
        fireBlast.transform.position = caller.transform.position;
        if (GetComponent<MoveController>().GetFacing() == -1)
        {
            fireBlast.GetComponent<SimpleObjectMover>().right = true;
            fireBlast.GetComponent<SimpleObjectMover>().left = false;
        }
        else
        {
            fireBlast.GetComponent<SimpleObjectMover>().right = false;
            fireBlast.GetComponent<SimpleObjectMover>().left = true;
        }
        Destroy(fireBlast, 2f);


    }
}
