using UnityEngine;
using System.Collections;
//using UnityEditor;

public class SwarmBehaviour : MonoBehaviour {

    public float Duration = 8;
    public bool flip = false;
    public GameObject player;
    private Rigidbody rigBod;
    //private MoveController moveCon;
    private Malady mal;
    private Animator animator;
    private Vector3 direction, currentPos, max;

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
        Duration -= 1;
        max = new Vector3(20, 5, 10);
        player = FindObjectOfType<Player>().gameObject;
        rigBod = GetComponent<Rigidbody>();
       // moveCon = GetComponent<MoveController>();
        mal = FindObjectOfType<Malady>();
        direction = new Vector3(0, 0, 0);
        rigBod.velocity = direction;
	}
	
	// Update is called once per frame
	void Update () {
        //animations are based on velocity
        //either set in mecanim or here, whatever works better
        //anim set 1 = velocity < 0.7f;
        //anim set2 = velocity < 1.1f;
        //anim set 3 = velocity > 1.1f;
        //anim set 4 = if velocity != direction && velocity < 0.7f;
        //maybe have no slow down in turns? we'll see


        if(flip)
        {
            Flip();
            Debug.Log(flip + " heyyyyy " + transform.localScale);
            flip = false;
        }
	    if (Vector3.Angle(rigBod.velocity, direction) > 90 && rigBod.velocity.magnitude < 15)
	    {
	        animator.SetTrigger("isTurning");
	    }

        animator.SetFloat("Speed", rigBod.velocity.magnitude);
        if (true)
        {
            if (transform.position.y <= 2f)
            {
                //transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            }
            if (Duration > 0)
            {
                direction = player.transform.position - transform.position;
                rigBod.velocity += direction.normalized * 0.4f;
                if (Mathf.Abs(rigBod.velocity.x) > Mathf.Abs(max.x))
                    rigBod.velocity = new Vector3(max.x * Mathf.Sign(rigBod.velocity.x), rigBod.velocity.y, rigBod.velocity.z);
                if (Mathf.Abs(rigBod.velocity.y) > Mathf.Abs(max.y))
                    rigBod.velocity = new Vector3(rigBod.velocity.x, max.y * Mathf.Sign(rigBod.velocity.y), rigBod.velocity.z);
                if (Mathf.Abs(rigBod.velocity.z) > Mathf.Abs(max.z))
                    rigBod.velocity = new Vector3(rigBod.velocity.x, rigBod.velocity.y, max.z * Mathf.Sign(rigBod.velocity.z));

                currentPos = transform.position;
                Debug.Log(Vector3.Angle(rigBod.velocity, direction));

            }
            else
            {
                if (mal)
                {
                    direction = mal.transform.position - transform.position;
                    transform.position = Vector3.Lerp(currentPos, mal.transform.position, Mathf.Abs(Duration));
                }
                else
                {
                    Destroy(gameObject);
                }
                if (direction.magnitude < 0.2f)
                {
                    Destroy(gameObject);
                }

            }
            Duration -= Time.deltaTime;
            if (Duration < -1)
            {
                Destroy(gameObject);
            }
        }
	}

    public void Flip()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void setTarget(GameObject p)
    {
        player = p;
    }
}
