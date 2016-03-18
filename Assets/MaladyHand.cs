using UnityEngine;
using System.Collections;

public class MaladyHand : MonoBehaviour {

    private Vector3 start, end, set;
    private float duration, lopp;

	// Use this for initialization
	void Start () {
        start = new Vector3(0, 0, 60);
        end = new Vector3(0, 0, -120);
        duration = 0f;
        lopp = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Will be set in animation, as it is easier and more accurate
        //change box collider size
        //change position slightly


        //this is here while no animations
        //if(duration > 1.5f)
           // Destroy(gameObject);

        //set = Vector3.Lerp(start, end, duration);
        //transform.eulerAngles = set;
        //duration += Time.unscaledDeltaTime;
    }

    void updateLoop()
    {
        lopp++;
        if(lopp > 2)
        {
            Destroy(gameObject);
        }
    }

    void activateColl()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
