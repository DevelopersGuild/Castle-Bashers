﻿using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour
{
    public AudioClip useSkillAudio;
    private float coolDown = 0;
    private float coolDownTimer = 0;
    private int price = 0;
    private int manaCost;
    private string skillName;
    public float value;
    [HideInInspector]
    public enum Type { Ranged, Melee, Support, Other };

    public Type skillType;

    //This is the level of the skill or if the skill is only useable by enemys, 
    //check out SkillLevel enum for all possible vaules.
    private SkillLevel skillLevel;

    public void SetBaseValues(float coolDown, int price, int manaCost, string skillName, SkillLevel skillLevel)
    {
        this.coolDown = coolDown;
        this.price = price;
        this.manaCost = manaCost;
        this.skillName = skillName;
        this.skillLevel = skillLevel;
    }

    // Use this for initialization
    protected virtual void Start()
    {

    }

    //Use Update to handle the cool down for the skill.
    protected virtual void Update()
    {
        //Reduce cool down timer.
        if (coolDownTimer <= 0)
        {
            coolDownTimer = 0;
        }
        else
        {
            coolDownTimer = coolDownTimer - 1 * Time.deltaTime;
        }
    }

    //The first gameobject that will be passed in is the caller gameObject this can be used to tell who is calling
    //It also provides you access to the caller gameObjects functions. 
    //The second argument is optional, meaning it may not always get passed in. It allows you to send in a 
    //target gameObject for the skill being used.
    public virtual void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        //Assign the value of coolDownTimer to the coolDown varible so we can check the cooldown.
        coolDownTimer = coolDown;
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    //override the default cooldown
    public void SetCoolDown(float value)
    {
        coolDown = value;
    }

    public float GetCoolDownTimer()
    {
        //Return the current time on the cool down.
        return coolDownTimer;
    }

    public int GetPrice()
    {
        //Return the price of the skill, so it can be purchased at the skill store.
        return price;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    public string GetSkillName()
    {
        return skillName;
    }

    public SkillLevel GetSkillLevel()
    {
        return skillLevel;
    }



    //Any number of other functions needed for your skill.
}
