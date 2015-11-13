﻿using UnityEngine;
using System.Collections;

public class Vanish : MonoBehaviour, ISkill
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

public class Stalker : MonoBehaviour {
    public float averageVanishInterval = 6;
    public float vanishIntervalRange = 1;
    public float averageVanishTime = 2;
    public float vanishTimeRange = 2;
    public float vanishSpeed = 1;
    private float vanishTime;
    private bool vanishing = false;
    float nextVanish;
    float vanishEnd;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        nextVanish = Random.Range((averageVanishInterval - vanishIntervalRange), (averageVanishInterval + vanishIntervalRange)) + Time.time;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > nextVanish)
        {
            beginVanish();
            nextVanish = Random.Range((averageVanishInterval - vanishIntervalRange), (averageVanishInterval + vanishIntervalRange)) + Time.time;
        }
        if (vanishing)
        {
            spriteRenderer.color -= new Color(0, 0, 0, 0.05f * vanishSpeed);
            if(Time.time >= vanishEnd)
            {
                vanishing = false;
                spriteRenderer.color = originalColor;
            }
        }
	}

    void beginVanish()
    {
        vanishing = true;
        vanishTime = Random.Range((averageVanishTime - vanishTimeRange), (averageVanishTime + vanishTimeRange));
        vanishEnd = Time.time + vanishTime;
    }


}
