using UnityEngine;
using System.Collections;

public class BasicFunctions : MonoBehaviour {

	// Use this for initialization

    //some basic functions that can be added to whatever and then called. Will add more functions as needed
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CreateSelf()
    {
        Instantiate(this, transform.position, transform.rotation);
    }

    public void DestroySelf(float t = 0)
    {
        Destroy(gameObject, t);
    }
}
