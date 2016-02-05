using UnityEngine;
using System.Collections;

public class RottenField : MonoBehaviour {

    public float dmgAmount, invTime, slowPercent, TimeToLive;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, TimeToLive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionStay(Collision other)
    {
        GameObject enemObj = other.gameObject;
        if (other.gameObject.GetComponent<Player>())
        {
            Player player = enemObj.GetComponent<Player>();
            Health hp = enemObj.GetComponent<Health>();
            if (!player.health.getInvincibility())
            {
                hp.takeDamage(dmgAmount, 2);
                player.SetInvTime(invTime);
            }
            player.GetComponent<CrowdControllable>().addSlow(slowPercent, 0.5f);
        }
    }

    //Same code just make sure it happens
    public void OnTriggerEnter(Collider other)
    {
        GameObject enemObj = other.gameObject;
        if (other.gameObject.GetComponent<Player>())
        {
            Player player = enemObj.GetComponent<Player>();
            Health hp = enemObj.GetComponent<Health>();
            if (!player.health.getInvincibility())
            {
                hp.takeDamage(dmgAmount, 2);
                player.SetInvTime(invTime);
            }
            player.GetComponent<CrowdControllable>().addSlow(slowPercent, 0.5f);
        }
    }
}
