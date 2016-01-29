﻿using UnityEngine;
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

    private UI_Gem_Selector selector;
    [HideInInspector]
    public int selecting;
    [HideInInspector]
    public bool subselecting;
    [HideInInspector]
    public int subindex;
	// Use this for initialization
	void Start () {
        selector = GetComponentInChildren<UI_Gem_Selector>();
        selecting = 0;
        subselecting = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(changing==true)
        {
            if(subselecting==true)
            {
                selector.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    subselecting = false;
                }
            }
            else
            {
                selector.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    GetComponentInParent<Character_Menu_FullControl>().gem_selecting = false;
                    changing = false;
                }
                if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    selecting = selecting == 1 ? 3 : selecting - 1;
                }
                if(Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.RightArrow))
                {
                    selecting = selecting == 3 ? 1 : selecting + 1;
                }
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    subindex= 0;
                    subselecting = true;
                }
            }
            
        }
        else
        {
            selector.gameObject.SetActive(false);
        }
	}
}