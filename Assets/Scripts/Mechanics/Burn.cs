using UnityEngine;
using System.Collections;

public class Burn : MonoBehaviour {
    private float percentDamagePerTick = 0.05f;
    private float damagePerTick;
    private float maxHealth;
    private float nextTick;
    private float tickInterval = 0.5f;
    private float duration = 5;
    private float expiration;
    
    private Defense defense;
	// Use this for initialization
	void Start () {

        defense = GetComponent<Defense>();
        maxHealth = GetComponent<Health>().GetMaxHP();
        damagePerTick = maxHealth * percentDamagePerTick;
        nextTick = Time.time + tickInterval;
        expiration = Time.time + duration;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time >= nextTick)
        {
            nextTick += tickInterval;
            Debug.Log("Burn dealing damage");
            defense.CheckMagicalDefense(damagePerTick);

        }
        if(Time.time >= duration)
        {
            Destroy(this);
        }
        
	}

    public void setDuration(float i)
    {
        Debug.Log("Burn time set to: " + i + " for unit" + gameObject.name);
        duration = i;
    }
}
