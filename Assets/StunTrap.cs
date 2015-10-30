using UnityEngine;
using System.Collections;

public class StunTrap : MonoBehaviour {
    CrowdControllable crowdControllable;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            crowdControllable = col.GetComponent<CrowdControllable>();
            crowdControllable.addStun(2);
            Destroy(gameObject);
        }
    }
}
