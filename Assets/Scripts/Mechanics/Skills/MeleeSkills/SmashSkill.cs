using UnityEngine;
using System.Collections;

public class SmashSkill : MonoBehaviour, ISkill
{
    public void UseSkill(GameObject caller, GameObject target, float coolDownTimer = 0)
    {

    }

    public float GetCoolDownTimer()
    {
        //TODO Temporary value change 
        return 0;
    }
    public int GetPrice()
    {
        //TODO Temporary value change 
        return 200;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return SkillLevel.EnemyOnly;
    }
}
