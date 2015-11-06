using UnityEngine;
using System.Collections;

public class SkillTemplate : MonoBehaviour, ISkill
{
    private int price = 0;
    private float cooldown = 0;
    //public GameObject projectile;
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
        return price;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return SkillLevel.EnemyOnly;
    }
}
