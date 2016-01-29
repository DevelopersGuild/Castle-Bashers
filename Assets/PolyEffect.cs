using UnityEngine;
using System.Collections;

public class PolyEffect : MonoBehaviour {

    public float PolyTime;

	// Use this for initialization
	void Start () {
	    //Might need to create an empty object to play the animation, as this one moves
        //We'll see when animations are implemented
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void OnCollisionStay(Collision other)
    {
        //Check for player collision
        if (other.gameObject.GetComponent<Player>())
        {
            //Find components necessary to take damage and knockback
            GameObject playerObject = other.gameObject;
            Player player = playerObject.GetComponent<Player>();
            Health hp = playerObject.GetComponent<Health>();
            player.setPoly(0.9f, PolyTime);

        }
    }

    //Same code just make sure it happens
    public void OnTriggerStay(Collider other)
    {

        //Check for player collision
        if (other.gameObject.GetComponent<Player>())
        {
            //Find components necessary to take damage and knockback
            GameObject playerObject = other.gameObject;
            Player player = playerObject.GetComponent<Player>();
            player.setPoly(0.9f, PolyTime);
        }
    }
}
