using UnityEngine;
using System.Collections;

public class StunOnHit : MonoBehaviour {
    public float percentChance = 100;
    public float duration = 1;
	// Use this for initialization
	void Start () {
	    
	}
	void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<CrowdControllable>())
        {
            if (Random.Range(1, 100) <= percentChance)
            {
                col.gameObject.GetComponent<CrowdControllable>().addStun(duration);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<CrowdControllable>())
        {
            if (Random.Range(1, 100) <= percentChance)
            {
                col.gameObject.GetComponent<CrowdControllable>().addStun(duration);
            }
        }
    }
}
