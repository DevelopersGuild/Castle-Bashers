using UnityEngine;
using System.Collections;



public class ChosenMage : MonoBehaviour {
    private float nextCast;
    private float castInterval = 1;
    private float meteorShowerCooldown;
    private MeteorShowerSkill meteorShower;
	// Use this for initialization
	void Start () {
        meteorShower = gameObject.AddComponent<MeteorShowerSkill>();
        nextCast = Time.time + castInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if (meteorShower.GetCoolDownTimer() <= 0)
        {
            meteorShower.UseSkill(gameObject, gameObject);
        }
	}

    public void NotifyWarriorDeath()
    {
        GetComponent<Defense>().PhysicalDefense /= 2;
        GetComponent<Defense>().MagicalDefense /= 2;
    }
    public void NotifyArcherDeath()
    {

    }
}
