using UnityEngine;
using System.Collections;

public class SwarmBehaviour : MonoBehaviour {

    public float Duration = 8;
    private Player player;
    private Rigidbody rigBod;
    //private MoveController moveCon;
    private Malady mal;
    private Vector3 direction, currentPos, max;

	// Use this for initialization
	void Start () {
        Duration -= 1;
        max = new Vector3(20, 5, 10);
        player = FindObjectOfType<Player>();
        rigBod = GetComponent<Rigidbody>();
       // moveCon = GetComponent<MoveController>();
        mal = FindObjectOfType<Malady>();
        direction = new Vector3(0, 0, 0);
        rigBod.velocity = direction;
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y <= 2f)
        {
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
        }
	    if(Duration > 0)
        {
            direction = player.transform.position - transform.position;
            rigBod.velocity += direction.normalized * 0.4f;
            if (Mathf.Abs(rigBod.velocity.x) > Mathf.Abs(max.x))
                rigBod.velocity = new Vector3(max.x, rigBod.velocity.y, rigBod.velocity.z);
            if (Mathf.Abs(rigBod.velocity.y) > Mathf.Abs(max.y))
                rigBod.velocity = new Vector3(rigBod.velocity.x, max.y, rigBod.velocity.z);
            if (Mathf.Abs(rigBod.velocity.z) > Mathf.Abs(max.z))
                rigBod.velocity = new Vector3(rigBod.velocity.x, rigBod.velocity.y, max.z);

            currentPos = transform.position;
        }
        else
        {
            if (mal)
            {
                transform.position = Vector3.Lerp(currentPos, mal.transform.position, Mathf.Abs(Duration));
            }
            else
            {
                Destroy(gameObject);
            }
            if(direction.magnitude < 0.2f)
            {
                Destroy(gameObject);
            }
             
        }
        Duration -= Time.deltaTime;
        if(Duration < -1)
        {
            Destroy(gameObject);
        }
	}

    public void setTarget(Player p)
    {
        player = p;
    }
}
