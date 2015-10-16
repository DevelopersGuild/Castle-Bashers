using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_BGMovie_Control : MonoBehaviour {

    RawImage BGMOV;
    MovieTexture MOV;
	// Use this for initialization
	void Start () {
        BGMOV = GetComponent<RawImage>();
        //Debug.Log(BGMOV.mainTexture.GetType());
        MOV = (MovieTexture)BGMOV.mainTexture;
        MOV.loop = true;
        MOV.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
