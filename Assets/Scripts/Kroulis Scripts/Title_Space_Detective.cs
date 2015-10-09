using UnityEngine;
using System.Collections;

public class Title_Space_Detective : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Title_TransScene tts;
        tts=GetComponent<Title_TransScene>();
        if (Input.GetKey(KeyCode.Space)){
            tts.StartSplash (3);
        }
	}
}
