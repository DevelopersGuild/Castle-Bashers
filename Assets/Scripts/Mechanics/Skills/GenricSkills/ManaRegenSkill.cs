using UnityEngine;
using System.Collections;

public class ManaRegenSkill :MonoBehaviour, ISkill
{
    private float coolDown = 0;
    private float timeSinceSkilledUsed = 0;
    private int price = 0;
    private SkillLevel skillLevel = SkillLevel.Level1;
    public void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0)
    {
        if(caller.tag == "Player")
        {
            
        }
    }

    public float GetCoolDownTimer()
    {
        //TODO Temporary value change 
        return coolDown;
    }
    public int GetPrice()
    {
        //TODO Temporary value change 
        return price;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return skillLevel;
    }

    void Update()
    {
        if (coolDown <= 0)
        {
            coolDown = 0;
        }
        else
        {
            coolDown = coolDown - 1 * Time.deltaTime;
        }
    }
}
