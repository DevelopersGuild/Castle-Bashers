using UnityEngine;
using System.Collections;

public class Whirlwind : MonoBehaviour, ISkill
{
    private int price;
    private float cooldown;
    GameObject whirlwind;
    //public GameObject projectile;
    public void UseSkill(GameObject caller, GameObject target, float coolDownTimer = 0)
    { 
        whirlwind = Instantiate(Resources.Load("Whirlwind")) as GameObject;
        whirlwind.transform.position = caller.transform.position;
        whirlwind.transform.parent = caller.transform;
        Destroy(whirlwind, 2.5f);
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
