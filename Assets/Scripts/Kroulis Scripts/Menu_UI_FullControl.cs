using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_UI_FullControl : MonoBehaviour {

    //Full Scale
    float full_scale;
    //Control
    GameObject ON_OFF;
    GameObject Character_Menu;
    GameObject Bag_Menu;
    GameObject Ability_Menu;
    GameObject Setting_Menu;
    //Control API
    public bool Menu_open;
    public int Menu_id;
    public GameObject Main_Process;
    //Gold
    Text GoldAmount;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Text[] finds1;
        //Image[] finds2;
        GameObject GOResult;
        //Link UI
        GOResult = GameObject.Find("Menu_Background");
        finds1=GOResult.GetComponentsInChildren<Text>();
        foreach(Text t in finds1)
        {
            if(t.name=="Gold_Amount")
            {
                GoldAmount = t;
                break;
            }
        }
        ON_OFF = GOResult;
        //Link Menu->Character
        GOResult = GameObject.Find("Character_Menu");
        Character_Menu = GOResult;
        //Link Menu->Bag
        GOResult = GameObject.Find("Bag_Menu");
        Bag_Menu = GOResult;
            //todo
        //Link Menu->Ability
        GOResult = GameObject.Find("Ability_Menu");
        Ability_Menu = GOResult;
            //todo
        //Link Menu->Setting
        GOResult = GameObject.Find("Setting_Menu");
        Setting_Menu = GOResult;
            //todo

	}
	
	// Update is called once per frame
	void Update () {
        if(Menu_open==false)
        {
            ON_OFF.SetActive(false);
        }
        else
        {
            full_scale = (float)(Screen.width / 1920.00);
            this.GetComponent<CanvasScaler>().scaleFactor = full_scale;
            ON_OFF.SetActive(true);
            if(Menu_id==1)//character open
            {
                Character_Menu.SetActive(true);
                Bag_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else if(Menu_id==2)//bag open
            {
                Bag_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else if(Menu_id==3)//ability open
            {
                Ability_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Bag_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else//setting open
            {
                Setting_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Bag_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && Main_Process.GetComponent<Main_Process>().esckey_up==false)
        {
            Main_Process.GetComponent<Main_Process>().Menu_Open = false;
        }
        if(Main_Process.GetComponent<Main_Process>().esckey_up==true && Input.GetKeyUp(KeyCode.Escape))
        {
            Main_Process.GetComponent<Main_Process>().esckey_up = false;
        }
        
	}
}
