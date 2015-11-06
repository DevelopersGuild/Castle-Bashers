using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
    private float instantiateTime;
    public float lifeSpan;
	// Use this for initialization
	void Start () {
        instantiateTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time - instantiateTime >= lifeSpan)
        {
            Destroy(gameObject);
        }
	}
}
