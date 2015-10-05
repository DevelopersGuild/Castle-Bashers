using UnityEngine;
using System.Collections;

public class Title_Movie_Control : MonoBehaviour {
    public MovieTexture movTexture;
	// Use this for initialization
	void Start () {
    //GetComponent<Renderer>().material.mainTexture = movTexture;
    movTexture.loop = true;
    movTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture, ScaleMode.StretchToFill); ;
    }
}
