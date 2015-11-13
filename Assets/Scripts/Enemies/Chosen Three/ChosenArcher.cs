﻿using UnityEngine;
using System.Collections;

public class ChosenArcher : MonoBehaviour {

    private Skill multiShot;

	// Use this for initialization
	void Start () {
        multiShot = gameObject.AddComponent<MultiShot>();
        Debug.Log("MultiShot starting cooldown!" + multiShot.GetCoolDown());
	}
	
	// Update is called once per frame
	void Update () {

	    if(multiShot.GetCoolDownTimer() <= 0)
        {
            multiShot.UseSkill(gameObject, GetComponent<Enemy>().target);
        }
	}

    public void NotifyMageDeath()
    {
        multiShot.SetCoolDown(multiShot.GetCoolDown() * 2);
        Debug.Log("Multi cd after death!" + multiShot.GetCoolDown());
    }
    public void NotifyWarriorDeath()
    {
        GetComponent<Defense>().PhysicalDefense /= 2;
        GetComponent<Defense>().MagicalDefense /= 2;
    }
}
