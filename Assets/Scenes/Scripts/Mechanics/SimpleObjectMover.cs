using UnityEngine;
using System.Collections;

public class SimpleObjectMover : MonoBehaviour {
    public float speed = 0.5f;
    public float time = 2;
    public bool transformRight = false;
    public bool up = false;
    public bool left = false;
    public bool right = false;
    public bool down = false;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(transformRight == true)
        {
            gameObject.transform.right += gameObject.transform.forward * speed;
        }
	    if(up == true)
        {
            gameObject.transform.position += new Vector3(0f, 0.25f, 0f) * speed;
        }
        if (left == true)
        {
            gameObject.transform.position += new Vector3(-0.25f, 0, 0f) * speed;
        }
        if (right == true)
        {
            gameObject.transform.position += new Vector3(0.25f, 0, 0f) * speed;
        }
        if (down == true)
        {
            gameObject.transform.position += new Vector3(0f, -0.25f, 0f) * speed;
        }
    }
}
