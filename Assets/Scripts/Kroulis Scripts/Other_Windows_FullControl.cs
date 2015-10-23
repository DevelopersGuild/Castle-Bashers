using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Other_Windows_FullControl : MonoBehaviour {

    //link Main_Process
    public GameObject Main_Process;
    //Full Scale
    float full_scale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Adjust
        full_scale = (float)(Screen.width / 1920.00);
        this.GetComponent<CanvasScaler>().scaleFactor = full_scale;

	}
}
