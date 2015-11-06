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
    //Character Infos
    Text ATK, DEF, STA, SPI, AGI, BATK, MATK,PDEF,MDEF,CRIR,C_HP, C_MP, C_EXP, C_NEXP, C_Name, C_LV;
    Image C_ICON;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Text[] finds1;
        Image[] finds2;
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
        finds1 = GOResult.GetComponentsInChildren<Text>();
        foreach(Text t in finds1)
        {
            if (t.name == "ATK")
            {
                ATK = t;
                continue;
            }
            if (t.name == "DEF")
            {
                DEF = t;
                continue;
            }
            if (t.name == "STA")
            {
                STA = t;
                continue;
            }
            if (t.name == "SPI")
            {
                SPI = t;
                continue;
            }
            if (t.name == "AGI")
            {
                AGI = t;
                continue;
            }
            if (t.name == "BATK")
            {
                BATK = t;
                continue;
            }
            if (t.name == "MATK")
            {
                MATK = t;
                continue;
            }
            if (t.name == "PDEF")
            {
                PDEF = t;
                continue;
            }
            if (t.name == "MDEF")
            {
                MDEF = t;
                continue;
            }
            if (t.name == "CRIR")
            {
                CRIR = t;
                continue;
            }
            if (t.name == "C_HP")
            {
                C_HP = t;
                continue;
            }
            if (t.name == "C_MP")
            {
                C_MP = t;
                continue;
            }
            if (t.name == "C_EXP")
            {
                C_EXP = t;
                continue;
            }
            if (t.name == "C_NEXP")
            {
                C_NEXP = t;
                continue;
            }
            if (t.name == "C_Name")
            {
                C_Name = t;
                continue;
            }
            if (t.name == "C_LV")
            {
                C_LV = t;
                continue;
            }

        }
        finds2 = GOResult.GetComponentsInChildren<Image>();
        foreach(Image i in finds2)
        {
            if(i.name=="C_ICON")
            {
                C_ICON = i;
                break;
            }
        }
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
