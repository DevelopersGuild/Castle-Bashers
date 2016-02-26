﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rewired;

public class Level_Select_FullControl : MonoBehaviour {

    public int chapid;
    public int currentmap;
    public int currentdiff;

    Level_Select_mapinfo Level_Select_mapinfo_script;
    Map_Transfer_DB Map_Transfer_DB_Script;
    //Level_Select_Diff_ImageLib Diff_ImageLab;
    Image Map_BG;
    Image Windows_BG;
    Text Map_name;
    Text Chapter_name;
    Image Bar;
    //Image Diff;
    Text Diff_num;
    GameObject Mainprocess;
    GameObject Windows;


	// Use this for initialization
	void Start () {
        Map_BG = GetComponent<Image>();
        Image[] Result1;
        Result1 = GetComponentsInChildren<Image>();
        foreach(Image i in Result1)
        {
            if(i.name=="L_S_W_BG")
            {
                Windows_BG = i;
                continue;
            }
            if(i.name=="Level_Select_Bar")
            {
                Bar = i;
                continue;
            }
            //if(i.name=="L_S_W_diff")
            //{
            //    Diff = i;
            //    Diff_ImageLab = Diff.GetComponent<Level_Select_Diff_ImageLib>();
            //    continue;
            //}
            if(i.name=="Level_Select_Window")
            {
                Windows = i.gameObject;
                continue;
            }
        }
        Text[] Result2;
        Result2 = GetComponentsInChildren<Text>();
        foreach(Text t in Result2)
        {
            if(t.name=="L_S_W_name")
            {
                Map_name = t;
                continue;
            }
            if(t.name=="Level_Select_Chapter")
            {
                Chapter_name = t;
                continue;
            }
            if(t.name=="L_S_W_diff_nub")
            {
                Diff_num = t;
                continue;
            }
        }
        Mainprocess = GameObject.Find("Main Process");
        Level_Select_mapinfo_script = Mainprocess.GetComponentInChildren<Level_Select_mapinfo>();
        Map_Transfer_DB_Script = Mainprocess.GetComponentInChildren<Map_Transfer_DB>();
        gameObject.SetActive(false);
	
	}

    public void ShowMap(int chapterid)
    {
        if (chapterid > Level_Select_mapinfo_script.Chapter.Length)
        {
            gameObject.SetActive(false);
            Mainprocess.GetComponent<Main_Process>().OtherWindows_Close();
        }
        chapid = chapterid;
        currentmap = 0;
        currentdiff = 1;
        Map_BG.sprite=Level_Select_mapinfo_script.Chapter[chapid].Map_Background;
        Chapter_name.text = chapid.ToString();
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        Windows.GetComponent<RectTransform>().localPosition=Level_Select_mapinfo_script.Chapter[chapid].mapinfo[currentmap].Position;
        Map_name.text = Map_Transfer_DB_Script.mapinfo[Level_Select_mapinfo_script.Chapter[chapid].mapinfo[currentmap].mapid].name;
        Windows_BG.sprite= Map_Transfer_DB_Script.mapinfo[Level_Select_mapinfo_script.Chapter[chapid].mapinfo[currentmap].mapid].mini_bg_texture;
        //Diff.sprite = Diff_ImageLab.diff[currentdiff];
        Diff_num.text = "<   LEVEL "+ currentdiff.ToString() +"   >";
	    if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentdiff != 0)
                currentdiff = currentdiff - 1;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
           // if (currentdiff != 4)
              if (currentdiff != 99)
                //if(the difficulty had unlocked)
                currentdiff = currentdiff + 1;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentmap != 0)
                currentmap = currentmap - 1;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentmap!=Level_Select_mapinfo_script.Chapter[chapid].mapinfo.Length-1)
            {
                currentmap = currentmap + 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Go to the map:"+Map_name.text.ToString());
            Globe.Map_Load_id = Level_Select_mapinfo_script.Chapter[chapid].mapinfo[currentmap].mapid;
            Globe.Map_Level = currentdiff;
            this.gameObject.SetActive(false);
            Mainprocess.GetComponent<Main_Process>().OtherWindows_Close();
            Application.LoadLevel("_loading");
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Go Back to the town.");
            this.gameObject.SetActive(false);
            Mainprocess.GetComponent<Main_Process>().OtherWindows_Close();
        }
	}
}
