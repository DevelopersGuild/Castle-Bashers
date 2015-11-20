using UnityEngine;
using System.Collections;

public class ChosenWarrior : MonoBehaviour {
    public float whirlwindCooldown = 5;
    private float lastCast = 0;
    private Skill whirlwind;



	// Use this for initialization
	void Start () {
        whirlwind = new Whirlwind();
	}
	
	// Update is called once per frame
	void Update () {

        
	    if (Time.time - lastCast >= whirlwindCooldown)
        {
            whirlwind.UseSkill(gameObject);
            lastCast = Time.time;
        }
	}

    public void NotifyArcherDeath()
    {

    }

    public void NotifyMageDeath()
    {
        whirlwindCooldown *= 2;
    }
}
