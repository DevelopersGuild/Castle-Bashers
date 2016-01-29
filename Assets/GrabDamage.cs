using UnityEngine;
using System.Collections;

public class GrabDamage : MonoBehaviour {

    private float tickInterval, tick, damage, dmgAmount;
    private bool throwAttack = false;
    private Player target;

    //when created, also call destroy(this, timetolive) (timetolive should be ~2s, final values will be adjusted, including tick times and damage numbers)

	// Use this for initialization
	void Start () {
        tickInterval = 0.33f;
        tick = 0;
        damage = 0.5f;
        dmgAmount = damage;

	}
	
	// Update is called once per frame
	void Update () {
        if (!throwAttack)
        {
            //player set to invincible, still takes damage from hair grab (and only grab)
            //set invincible off OnDestroy
            target.SetInvincible(true);
            if (tick >= tickInterval)
            {
                //This is if magical defense is a percentage, which it is not. Will change when final defense system is known
                //More magic defense = more damage this deals
                //dmgAmount = damage * (1 + target.GetComponent<Defense>().GetMagicalDefense());

                //How to deal magic damage? there's only take damage
                target.GetComponent<Health>().takeDamage(dmgAmount, 0);
                tick = 0;
            }
            //is not affected by time slow/stop
            tick += Time.unscaledDeltaTime;
        }
	}
    
    public void setTarget(Player p)
    {
        target = p;
    }

    void OnDestroy()
    {
        target.SetInvincible(false);
        Debug.Log("Hello? Is this working?");
    }

    public void setThrow()
    {
        throwAttack = true;
    }



}
