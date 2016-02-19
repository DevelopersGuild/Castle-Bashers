using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GEM_CUR_ANIME : MonoBehaviour {
    Image thisimage;
    bool inverse = false;
	// Use this for initialization
	void Start () {
        thisimage = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
	    if(!inverse)
        {
            thisimage.color = new Color(thisimage.color.r, thisimage.color.g, thisimage.color.b, thisimage.color.a - 0.05f * Time.deltaTime);
            if (thisimage.color.a <= 0)
                inverse = true;
        }
        else
        {
            thisimage.color = new Color(thisimage.color.r, thisimage.color.g, thisimage.color.b, thisimage.color.a + 0.05f * Time.deltaTime);
            if (thisimage.color.a >= 1)
                inverse = false;
        }
	}
}
