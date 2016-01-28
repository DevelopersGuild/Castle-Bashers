using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GEM_FullControl : MonoBehaviour {

    public Image[] Gem = new Image[3];
    public Image[] Gem_Upper = new Image[3];

    [HideInInspector]
    public int change=0;
    [HideInInspector]
    public bool changing = false;
    [HideInInspector]
    public int playerid = 0;
    [HideInInspector]
    public Main_Process mainp = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
