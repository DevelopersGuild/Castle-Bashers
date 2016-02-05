using UnityEngine;
using System.Collections;

public class MaladyHand : MonoBehaviour {

    private Vector3 start, end, set;
    private float duration;

	// Use this for initialization
	void Start () {
        start = new Vector3(0, 0, 0);
        end = new Vector3(0, 0, 90);
        duration = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {
        //Will be set in animation, as it is easier and more accurate
        //change box collider size
        //change position slightly


        //this is here while no animations
        if(duration <= 0)
            Destroy(gameObject);

        set = Vector3.Lerp(start, end, duration);
        transform.rotation.eulerAngles.Set(set.x, set.y, set.z);
        duration -= Time.unscaledDeltaTime;
    }
}
