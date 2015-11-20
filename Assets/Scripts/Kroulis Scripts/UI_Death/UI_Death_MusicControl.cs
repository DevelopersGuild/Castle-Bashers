using UnityEngine;
using System.Collections;

public class UI_Death_MusicControl : MonoBehaviour {
    public bool BGM_Start;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(BGM_Start==true)
        {
            BGM_Start = false;
            audio.mute = false;
            audio.Play();
        }
	    
	}
}
