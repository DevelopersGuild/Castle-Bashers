using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

    private GameObject target;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 0, 0.5f);
        target = FindObjectOfType<Camera>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if(target)
        transform.position = target.transform.position + offset;
	}

    public void setTarget(GameObject t)
    {
        target = t;
    }

}
