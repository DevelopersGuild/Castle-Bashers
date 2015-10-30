using UnityEngine;
using System.Collections;

//Skill must be a MonoBehavior and then implement the ISkill interface.
public class SkillExample : MonoBehaviour, ISkill
{
    //this is the timer for the cooldown.
    private float coolDown = 0;
    private float timeSinceSkilledUsed = 0;
    //this is the price of the skill.
    private int price = 0;
    //This is the level of the skill or if the skill is only useable by enemys, 
    //check out SkillLevel enum for all possible vaules.
    private SkillLevel skillLevel = SkillLevel.Level1;

    // Use this for initialization
    void Start()
    {

    }

    //The first gameobject that will be passed in is the caller gameObject this can be used to tell who is calling
    //It also provides you access to the caller gameObjects functions. 
    //The second argument is optional, meaning it may not always get passed in. It allows you to send in a 
    //target gameObject for the skill being used.
    //The third argument is also optional it is the cool down time for the skill. 
    public void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0)
    {
        //Assign the value of coolDownTimer to the coolDown varible so we can check the cooldown.
        coolDown = coolDownTimer;
        //Optional checks for who is calling the skill.
        //Check if the caller is a player. 
        if (caller.tag == "Player")
        {
            //Do something here.
        }
        //Check if the caller is a enemy.
        if (caller.tag == "Enemy")
        {
            //Do something here.
        }
    }

    public float GetCoolDownTimer()
    {
        //Return the current time on the cool down.
        return coolDown;
    }
    public int GetPrice()
    {
        //Return the price of the skill, so it can be pruchased at the skill store.
        return price;
    }
    public SkillLevel GetSkillLevel()
    {
        //Temporary value change.
        return skillLevel;
    }

    //Use Update to handle the cool down for the skill.
    void Update()
    {
        //Reduce cool down timer.
        if(coolDown <= 0)
        {
            coolDown = 0;
        }
        else
        {
            coolDown = coolDown - 1 * Time.deltaTime;
        }
    }

    //Any number of other functions needed for your skill.
}
