using UnityEngine;
using System.Collections;

public class PolyCloud : MonoBehaviour {

    public GameObject PolyAttack;
    public float Delay;

    private bool d;

	// Use this for initialization
	void Start () {
        d = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Delay <= 0)
        {
            Destroy(gameObject);
        }
        else if(Delay <= 0.3f)
        {
            if (d)
            {
                Instantiate(PolyAttack, transform.position, PolyAttack.transform.rotation);
                d = false;
            }
        }

        Delay -= Time.deltaTime;
	}
}
