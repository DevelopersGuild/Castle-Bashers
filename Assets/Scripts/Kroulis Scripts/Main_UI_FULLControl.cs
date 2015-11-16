using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main_UI_FULLControl : MonoBehaviour {
    [System.Serializable]
    public struct Playerinfo
    {
        public int id;
        public string name;
        public int classid;
        public int hp, maxhp;
        public int mp, maxmp;
    };
    [System.Serializable]
    public struct Bossinfo
    {
        public int id;
        public int headiconid;
        public int hp, maxhp;
    };
    //Full Scale
    float full_scale;
    //Mode Control
    public bool One_player_per_client;
    public bool Team_mode;
    public bool Killing_boss;
    public bool Menu_open;
    public bool In_Battle;
    
    //Control
    public int[] skillid = new int[4];  //to set the skill id of 4 skill space
    public int[] itemid = new int[3]; //to set the item id of 3 item space
    public Playerinfo[] partner;
    public Bossinfo boss;
    public GameObject Main_Process;

    //Temp Control
    public int hp, maxhp, mp, maxmp, exp, nexp,lv,cid;

    //UI Updata
    Text HP;
    Text MP;
    Text LV;
    Text Class;
    Image HP_Bar;
    Image MP_Bar;
    Image EXP_Bar;
    Image Boss_HP;
    Image Boss_Icon;
    Image[] partner_hp = new Image[6];
    Image[] partner_mp = new Image[6];
    Image[] skillid_icon = new Image[4];
    Image[] itemid_icon = new Image[3];
    Text[] partner_name = new Text[6];
    Image[] partner_icon = new Image[6];
    GameObject BossMode;
    GameObject TeamMode;
    GameObject OPM;
    Boss_HeadIcon Boss_HeadIcon_Script;
    Character_Class_Info Character_Class_Info_Script;
    int teamsize=0;

	// Use this for initialization
	void Start () {
        Text[] finds1;
        Image[] finds2;
        GameObject GOResult;
        //Set Static
        DontDestroyOnLoad(this.gameObject);
        //Link Database
        GOResult = GameObject.Find("DataBase");
        Boss_HeadIcon_Script = GOResult.GetComponent<Boss_HeadIcon>();
        Character_Class_Info_Script = GOResult.GetComponent<Character_Class_Info>();
        //Link BOSS INFO
        GOResult = GameObject.Find("Boss Bar");
        BossMode = GOResult;
        finds2=GOResult.GetComponentsInChildren<Image>();
        foreach (Image i in finds2)
        {
            if(i.name=="HP")
            {
               //Debug.Log("Find Boss HP");
                Boss_HP = i;
                continue;
            }
            if(i.name=="HEAD ICON")
            {
                Boss_Icon = i;
                continue;
            }
        }
        //Make Sure the typpe of UI
        if(One_player_per_client==true)
        {
            //Link important Objects
            GOResult = GameObject.Find("One Player Panel");
            OPM = GOResult;
            finds1 = GOResult.GetComponentsInChildren<Text>();
            foreach(Text t in finds1)
            {
                if(t.name == "HP Value" )
                {
                    //Debug .Log("HP Value Found!");
                    HP = t;
                    continue;
                }
                if(t.name == "MP Value" )
                {
                    //Debug .Log("MP Value Found!");
                    MP = t;
                    continue;
                }
                if(t.name == "Level")
                {
                    //Debug .Log("Level Found!");
                    LV = t;
                    continue;
                }
                if (t.name == "Class")
                {
                    //Debug .Log("Class Found!");
                    Class = t;
                    continue;
                }
                if (t.name == "T1N")
                {
                    //Debug .Log("T1N Found!");
                    partner_name[0] = t;
                    continue;
                }
                if (t.name == "T2N")
                {
                    //Debug.Log("T1N Found!");
                    partner_name[1] = t;
                    continue;
                }
                if (t.name == "T3N")
                {
                    //Debug.Log("T3N Found!");
                    partner_name[2] = t;
                    continue;
                }
                if (t.name == "T4N")
                {
                    //Debug.Log("T4N Found!");
                    partner_name[3] = t;
                    continue;
                }
                if (t.name == "T5N")
                {
                    //Debug.Log("T5N Found!");
                    partner_name[4] = t;
                    continue;
                }
                if (t.name == "T6N")
                {
                    //Debug.Log("T6N Found!");
                    partner_name[5] = t;
                    continue;
                }
                
            }
            finds2 = GOResult.GetComponentsInChildren<Image>();
            foreach(Image i in finds2)
            {
                if (i.name == "Team Mode")
                {
                    //Debug .Log("TeamMode Found!");
                    TeamMode = i.gameObject;
                    continue;
                }
                if (i.name == "HP")
                {
                    //Debug .Log("HP Value Found!");
                    HP_Bar = i;
                    continue;
                }
                if (i.name == "MP")
                {
                    //Debug .Log("HP Value Found!");
                    MP_Bar = i;
                    continue;
                }
                if (i.name == "EXP")
                {
                    //Debug .Log("HP Value Found!");
                    EXP_Bar = i;
                    continue;
                }
                if (i.name == "T1H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[0] = i;
                    continue;
                }
                if (i.name == "T1M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[0] = i;
                    continue;
                }
                if (i.name == "T2H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[1] = i;
                    continue;
                }
                if (i.name == "T2M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[1] = i;
                    continue;
                }
                if (i.name == "T3H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[2] = i;
                    continue;
                }
                if (i.name == "T3M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[2] = i;
                    continue;
                }
                if (i.name == "T4H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[3] = i;
                    continue;
                }
                if (i.name == "T4M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[3] = i;
                    continue;
                }
                if (i.name == "T5H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[4] = i;
                    continue;
                }
                if (i.name == "T5M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[4] = i;
                    continue;
                }
                if (i.name == "T6H")
                {
                    //Debug .Log("HP Value Found!");
                    partner_hp[5] = i;
                    continue;
                }
                if (i.name == "T6M")
                {
                    //Debug .Log("HP Value Found!");
                    partner_mp[5] = i;
                    continue;
                }
                if (i.name == "SK1")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0] = i;
                    continue;
                }
                if (i.name == "SK2")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[1] = i;
                    continue;
                }
                if (i.name == "SK3")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[2] = i;
                    continue;
                }
                if (i.name == "SK4")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[3] = i;
                    continue;
                }
                if (i.name == "IT1")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[0] = i;
                    continue;
                }
                if (i.name == "IT2")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[1] = i;
                    continue;
                }
                if (i.name == "IT3")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[2] = i;
                    continue;
                }
            }
            

        }
        else
        {

        }
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Menu_open == true)
        {
            OPM.SetActive(false);
            TeamMode.SetActive(false);
            BossMode.SetActive(false);
        }
        else
        {
            //Adjust
            full_scale = (float)(Screen.width / 1920.00);
            this.GetComponent<CanvasScaler>().scaleFactor = full_scale;
            //Cilent Control
            if (One_player_per_client == true)
            {
                OPM.SetActive(true);
                // Important infomation
                HP.text = hp.ToString() + "/" + maxhp.ToString();
                MP.text = mp.ToString() + "/" + maxmp.ToString();
                HP_Bar.fillAmount = (float)(hp * 1.00 / maxhp);
                MP_Bar.fillAmount = (float)(mp * 1.00 / maxmp);
                EXP_Bar.fillAmount = (float)(exp * 1.00 / nexp);
                //Level and Class
                LV.text = "LV" + lv.ToString();
                Class.text = Character_Class_Info_Script.Class_info[cid].name;

            }
            else
            {
                OPM.SetActive(false);
            }
            //Team Mode Control
            if (Team_mode == true)
            {
                TeamMode.SetActive(true);
                teamsize = partner.Length;
                TeamMode.GetComponent<Image>().fillAmount=(float)((teamsize+1)*1.00/6); 
                //if()

            }
            else
            {
                TeamMode.SetActive(false);
            }
            //Boss Mode Control
            if (Killing_boss == true)
            {
                BossMode.SetActive(true);
                Boss_HP.fillAmount = (float)(boss.hp * 1.00 / boss.maxhp);
                //Boss_Icon.sprite = Boss_HeadIcon_Script.icon[boss.headiconid];
            }
            else
            {
                BossMode.SetActive(false);
            }
            if (In_Battle == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //Open Settings
                {
                    Main_Process.GetComponent<Main_Process>().Menu_Force_Open(4);
                    Main_Process.GetComponent<Main_Process>().esckey_up = true;
                }
                if (Input.GetKeyDown(KeyCode.P))//Open Character
                {
                    Main_Process.GetComponent<Main_Process>().Menu_Force_Open(1); 
                }
                if (Input.GetKeyDown(KeyCode.K))//Open Skill
                {
                    Main_Process.GetComponent<Main_Process>().Menu_Force_Open(3);
                    
                }
                if (Input.GetKeyDown(KeyCode.B))//Open Bag
                {
                    Main_Process.GetComponent<Main_Process>().Menu_Force_Open(2);
                }
            }


        }

    }


    public void Update_Boss_Info()
    {
        Boss_Icon.sprite=Main_Process.GetComponentInChildren<Boss_HeadIcon>().icon[boss.id];
    }

}
