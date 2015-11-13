using UnityEngine;
using System.Collections;
using System;

public class ClawAttack : MonoBehaviour, ISkill
{
    public ExtendBehaviour claw1, claw2, claw3, claw4;

    public ClawAttack()
    {

    }

    public ClawAttack(ExtendBehaviour c1, ExtendBehaviour c2, ExtendBehaviour c3, ExtendBehaviour c4)
    {
        claw1 = c1;
        claw2 = c2;
        claw3 = c3;
        claw4 = c4;
    }

    public void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0)
    {
        float dir = caller.GetComponent<Malady>().getDirection();
        ExtendBehaviour c1 = Instantiate(claw1, caller.transform.position + new Vector3(0, 0, -1.62f), claw1.transform.rotation) as ExtendBehaviour;
        c1.direction = dir;
        ExtendBehaviour c2 = Instantiate(claw2, caller.transform.position + new Vector3(0, 0, 0.65f), claw2.transform.rotation) as ExtendBehaviour;
        c2.direction = dir;
        ExtendBehaviour c3 = Instantiate(claw3, caller.transform.position + new Vector3(0, 0, 0.81f), claw3.transform.rotation) as ExtendBehaviour;
        c3.direction = dir;
        ExtendBehaviour c4 = Instantiate(claw4, caller.transform.position + new Vector3(0, 0, 1.42f), claw4.transform.rotation) as ExtendBehaviour;
        c4.direction = dir;
    }
    public float GetCoolDownTimer()
    {
        //TODO Temporary value change 
        return 0;
    }
    public int GetPrice()
    {
        //TODO Temporary value change 
        return 0;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return SkillLevel.EnemyOnly;
    }



}


