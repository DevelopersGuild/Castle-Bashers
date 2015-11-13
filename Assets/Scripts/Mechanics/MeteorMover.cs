using UnityEngine;
using System.Collections;

public class MeteorMover : MonoBehaviour {
    private Vector3 moveIncrement;
    public float speed = 1;
    public float speedIncrement = 0; //speeds meteor up by this amount each frame
	// Use this for initialization
	void Start () {
        moveIncrement = new Vector3(-1, -2, 0);
	}
	
	// Update is called once per frame
	void Update () {
        speed += speedIncrement;
        gameObject.transform.position += moveIncrement * speed;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            Debug.Log("Hit platform!");
            Destroy(gameObject);
        }
    }
}
