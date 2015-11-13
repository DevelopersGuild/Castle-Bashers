using UnityEngine;
using System.Collections;

public class ID : MonoBehaviour {

    private float identity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetID(float f)
    {
        identity = f;
    }

    public float getID()
    {
        return identity;
    }
}
