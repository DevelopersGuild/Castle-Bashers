using UnityEngine;
using System.Collections;

public class sMeteor : MonoBehaviour, ISkill
{
    private int price = 1500;
    //public GameObject projectile;
    public void UseSkill(GameObject caller, GameObject target, float coolDownTimer = 0)
    {
        GameObject projectile = Instantiate(Resources.Load("Meteor")) as GameObject;
        projectile.transform.position = target.transform.position + new Vector3(3, 6, 0);

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