using UnityEngine;
using System.Collections;

public class ID : MonoBehaviour {

    private float identity;
    private bool affectedByTime = true;

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

    public bool getTime()
    {
        return affectedByTime;
    }

    public void setTime(bool b)
    {
        affectedByTime = b;
    }
}
