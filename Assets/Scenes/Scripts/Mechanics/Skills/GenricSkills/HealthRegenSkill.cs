using UnityEngine;
using System.Collections;

public class HealthRegenSkill : MonoBehaviour, ISkill
{
    private float coolDown = 0;
    private float timeSinceSkilledUsed = 0;
    private int price = 0;
    private SkillLevel skillLevel = SkillLevel.Level1;
    public void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0)
    {
        if (caller.tag == "Player")
        {
            caller.GetComponent<Health>().Regen();
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
}
