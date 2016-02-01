using UnityEngine;
using System.Collections;

public class PolyCloud : MonoBehaviour {

    public GameObject PolyAttack;
    public float Delay;

    private bool d;

	// Use this for initialization
	void Start () {
        d = true;
        //play idle animation
	}
	
	// Update is called once per frame
	void Update () {
	    if(Delay <= 0)
        {
            Destroy(gameObject);
        }
        else if(Delay <= 0.3f)
        {
            Instantiate(PolyAttack, transform.position, PolyAttack.transform.rotation);
            //run activation animation
            //end of animation, run destroy self (from basic functions component)


            //wtf is d
            //   if (d)
            //  {
            //   Instantiate(PolyAttack, transform.position, PolyAttack.transform.rotation);
            //      d = false;
            //  }
        }

        Delay -= Time.deltaTime;
	}

}
