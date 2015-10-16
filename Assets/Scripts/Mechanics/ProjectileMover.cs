using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
    private Vector3 moveIncrement;
    public GameObject target;
    private Vector3 targetPosition; //Used to avoid checking Z axis
    public float speed = 1;
	// Use this for initialization
	void Start () {
        targetPosition = target.transform.position;
        //Using 60 as a base number. When speed is 1, it will take 60 frames to land.

        moveIncrement = (targetPosition - gameObject.transform.position) / 60;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position += moveIncrement * speed;
	}
}
