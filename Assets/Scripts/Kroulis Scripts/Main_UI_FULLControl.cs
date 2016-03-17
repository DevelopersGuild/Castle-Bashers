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
    public GameObject Main_Process;
    private Main_Process mps;
    public GameObject OPM;
    public GameObject TPM;
    public GameObject BossMode;
    public GameObject TeamMode;
    public bool had_init = false;
    public bool One_player_per_client;
    public bool Team_mode;
    public bool Killing_boss;
    public bool Menu_open;
    public bool In_Battle;
    
    //Control
    public int[,] skillid = new int[2,4];  //to set the skill id of 4 skill space
    public int[,] itemid = new int[2,3]; //to set the item id of 3 item space
    public Playerinfo[] partner;
    public Bossinfo boss;

    //Temp Control
    public int[] hp = { 0, 0 };
    public int[] maxhp = { 0, 0 };
    public int[] mp = { 0, 0 };
    public int[] maxmp = { 0, 0 };
    public int[] exp = { 0, 0 };
    public int[] nexp = { 0, 0 };
    public int[] lv = { 0, 0 };
    public int[] cid = { 0, 0 };

    //UI Updata
    Text[] HP=new Text[2];
    Text[] MP = new Text[2];
    Text[] LV=new Text[2];
    Text[] Class = new Text[2];
    Image[] HP_Bar = new Image[2];
    Image[] MP_Bar = new Image[2];
    Image[] EXP_Bar = new Image[2];
    Image[,] skillid_icon = new Image[2,4];
    Image[,] itemid_icon = new Image[2,3];

    Image Boss_HP;
    Image Boss_Icon;
    Image[] partner_hp = new Image[6];
    Image[] partner_mp = new Image[6];
    Text[] partner_name = new Text[6];
    Image[] partner_icon = new Image[6];
    Boss_HeadIcon Boss_HeadIcon_Script;
    Character_Class_Info Character_Class_Info_Script;
    Skill_info SI;
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
        GOResult = GameObject.Find("SkillDataBase");
        SI = GOResult.GetComponent<Skill_info>();
        //Link BOSS INFO
        finds2 = BossMode.GetComponentsInChildren<Image>();
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
        finds1 = TeamMode.GetComponentsInChildren<Text>();
        finds2 = TeamMode.GetComponentsInChildren<Image>();
        foreach(Text t in finds1)
        {
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
        foreach(Image i in finds2)
        {
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
        }
	}

    public bool init()
    {
        Text[] finds1;
        Image[] finds2;
        if(One_player_per_client==true)
        {
            TPM.SetActive(false);
            OPM.SetActive(true);
            finds1 = OPM.GetComponentsInChildren<Text>();
            foreach (Text t in finds1)
            {
                //Debug.Log(t.name);
                if (t.name == "HP Value")
                {
                    //Debug .Log("HP Value Found!");
                    HP[0] = t;
                    continue;
                }
                if (t.name == "MP Value")
                {
                    //Debug .Log("MP Value Found!");
                    MP[0] = t;
                    continue;
                }
                if (t.name == "Level")
                {
                    //Debug .Log("Level Found!");
                    LV[0] = t;
                    continue;
                }
                if (t.name == "Class")
                {
                    //Debug .Log("Class Found!");
                    Class[0] = t;
                    continue;
                }

            }
            finds2 = OPM.GetComponentsInChildren<Image>();
            foreach (Image i in finds2)
            {
                if (i.name == "HP")
                {
                    //Debug .Log("HP Value Found!");
                    HP_Bar[0] = i;
                    continue;
                }
                if (i.name == "MP")
                {
                    //Debug .Log("HP Value Found!");
                    MP_Bar[0] = i;
                    continue;
                }
                if (i.name == "EXP")
                {
                    //Debug .Log("HP Value Found!");
                    EXP_Bar[0] = i;
                    continue;
                }
                if (i.name == "SK1_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0,0] = i;
                    continue;
                }
                if (i.name == "SK2_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0,1] = i;
                    continue;
                }
                if (i.name == "SK3_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0,2] = i;
                    continue;
                }
                if (i.name == "SK4_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0,3] = i;
                    continue;
                }
                if (i.name == "IT1")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[0,0] = i;
                    continue;
                }
                if (i.name == "IT2")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[0,1] = i;
                    continue;
                }
                if (i.name == "IT3")
                {
                    //Debug .Log("HP Value Found!");
                    //itemid_icon[0,2] = i;
                    continue;
                }
            }
        }
        else
        {
            OPM.SetActive(false);
            TPM.SetActive(true);
            finds1 = TPM.GetComponentsInChildren<Text>();
            foreach (Text t in finds1)
            {
                if (t.name == "P1HP Value")
                {
                    //Debug .Log("HP Value Found!");
                    HP[0] = t;
                    continue;
                }
                if (t.name == "P2HP Value")
                {
                    //Debug .Log("HP Value Found!");
                    HP[1] = t;
                    continue;
                }
                if (t.name == "P1MP Value")
                {
                    //Debug .Log("MP Value Found!");
                    MP[0] = t;
                    continue;
                }
                if (t.name == "P2MP Value")
                {
                    //Debug .Log("MP Value Found!");
                    MP[1] = t;
                    continue;
                }
                if (t.name == "P1Level")
                {
                    //Debug .Log("Level Found!");
                    LV[0] = t;
                    continue;
                }
                if (t.name == "P2Level")
                {
                    //Debug .Log("Level Found!");
                    LV[1] = t;
                    continue;
                }
                if (t.name == "P1Class")
                {
                    //Debug .Log("Class Found!");
                    Class[0] = t;
                    continue;
                }
                if (t.name == "P2Class")
                {
                    //Debug .Log("Class Found!");
                    Class[1] = t;
                    continue;
                }

            }
            finds2 = TPM.GetComponentsInChildren<Image>();
            foreach (Image i in finds2)
            {
                if (i.name == "P1HP")
                {
                    //Debug .Log("HP Value Found!");
                    HP_Bar[0] = i;
                    continue;
                }
                if (i.name == "P2HP")
                {
                    //Debug .Log("HP Value Found!");
                    HP_Bar[1] = i;
                    continue;
                }
                if (i.name == "P1MP")
                {
                    //Debug .Log("HP Value Found!");
                    MP_Bar[0] = i;
                    continue;
                }
                if (i.name == "P2MP")
                {
                    //Debug .Log("HP Value Found!");
                    MP_Bar[1] = i;
                    continue;
                }
                if (i.name == "P1EXP")
                {
                    //Debug .Log("HP Value Found!");
                    EXP_Bar[0] = i;
                    continue;
                }
                if (i.name == "P2EXP")
                {
                    //Debug .Log("HP Value Found!");
                    EXP_Bar[1] = i;
                    continue;
                }
                if (i.name == "P1SK1_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0, 0] = i;
                    continue;
                }
                if (i.name == "P1SK2_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0, 1] = i;
                    continue;
                }
                if (i.name == "P1SK3_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0, 2] = i;
                    continue;
                }
                if (i.name == "P1SK4_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[0, 3] = i;
                    continue;
                }
                if (i.name == "P1IT1")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[0, 0] = i;
                    continue;
                }
                if (i.name == "P1IT2")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[0, 1] = i;
                    continue;
                }
                if (i.name == "P1IT3")
                {
                    //Debug .Log("HP Value Found!");
                    //itemid_icon[0, 2] = i;
                    continue;
                }
                if (i.name == "P2SK1_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[1, 0] = i;
                    continue;
                }
                if (i.name == "P2SK2_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[1, 1] = i;
                    continue;
                }
                if (i.name == "P2SK3_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[1, 2] = i;
                    continue;
                }
                if (i.name == "P2SK4_BAR")
                {
                    //Debug .Log("HP Value Found!");
                    skillid_icon[1, 3] = i;
                    continue;
                }
                if (i.name == "P2IT1")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[1, 0] = i;
                    continue;
                }
                if (i.name == "P2IT2")
                {
                    //Debug .Log("HP Value Found!");
                    itemid_icon[1, 1] = i;
                    continue;
                }
                if (i.name == "P2IT3")
                {
                    //Debug .Log("HP Value Found!");
                    //itemid_icon[1, 2] = i;
                    continue;
                }
            }
        }
        mps = Main_Process.GetComponent<Main_Process>();
        had_init = true;
        return true;
    }
	
    public void Main_UI_StartChangingIcon()
    {
        Invoke("ChangeIcon", 2.0f);
    }

    public void Main_UI_StopChangingIcon()
    {
        CancelInvoke();
    }

    void ChangeIcon()
    {
        CancelInvoke("SkillCoolDownUpdate");
        if(had_init)
        {
            int changeicon = -1;
            for (int i = 0; i <= 3;i++)
            {
                changeicon = mps.GetPlayerScript(0).GetSkillSlotSkillID(i+1);
                skillid[0, i] = changeicon;
                //Debug.Log(changeicon);
                if(changeicon!=-1)
                {
                    Debug.Log(SI.skill[changeicon].skillname);
                    skillid_icon[0, i].sprite = SI.skill[changeicon].skillicon;
                }
                else
                {
                    skillid_icon[0, i].sprite = Resources.Load<Sprite>("null");
                }
            }
            if (!One_player_per_client)
            {
                for (int i = 0; i <= 3; i++)
                {
                    changeicon = mps.GetPlayerScript(1).GetSkillSlotSkillID(i + 1);
                    skillid[1, i] = changeicon;
                    if (changeicon != -1)
                    {
                        skillid_icon[1, i].sprite = SI.skill[changeicon].skillicon;
                    }
                    else
                    {
                        skillid_icon[1, i].sprite = Resources.Load<Sprite>("null");
                    }
                }
            }
        }
        Invoke("SkillCoolDownUpdate", 0.5f);
    }

    void SkillCoolDownUpdate()
    {
        if(had_init)
        {
            float timercal,timerrest;
            for(int i=0;i<=3;i++)
            {
                if(skillid[0,i]!=-1)
                {
                    timercal=mps.GetPlayerScript(0).GetSkillSlotScript(i).GetCoolDown();
                    timerrest=timercal-mps.GetPlayerScript(0).GetSkillSlotScript(i).GetCoolDownTimer();
                    skillid_icon[0, i].fillAmount = timerrest / timercal;
                }
            }
            if(!One_player_per_client)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (skillid[1, i] != -1)
                    {
                        timercal = mps.GetPlayerScript(1).GetSkillSlotScript(i).GetCoolDown();
                        timerrest = timercal - mps.GetPlayerScript(1).GetSkillSlotScript(i + 1).GetCoolDownTimer();
                        skillid_icon[1, i].fillAmount = timerrest / timercal;
                    }
                }
            }
        }
        Invoke("SkillCoolDownUpdate", 0.5f);
    }

	// Update is called once per frame
    void Update()
    {
        if(had_init)
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
                    TPM.SetActive(false);
                    OPM.SetActive(true);
                    // Important infomation
                    HP[0].text = hp[0].ToString() + "/" + maxhp[0].ToString();
                    MP[0].text = mp[0].ToString() + "/" + maxmp[0].ToString();
                    HP_Bar[0].fillAmount = (float)(hp[0] * 1.00 / maxhp[0]);
                    MP_Bar[0].fillAmount = (float)(mp[0] * 1.00 / maxmp[0]);
                    EXP_Bar[0].fillAmount = (float)(exp[0] * 1.00 / nexp[0]);
                    //Level and Class
                    LV[0].text = "LV" + lv[0].ToString();
                    Class[0].text = Character_Class_Info_Script.Class_info[cid[0]].name;

                }
                else
                {
                    OPM.SetActive(false);
                    TPM.SetActive(true);
                    HP[0].text = hp[0].ToString() + "/" + maxhp[0].ToString();
                    HP[1].text = hp[1].ToString() + "/" + maxhp[1].ToString();
                    MP[0].text = mp[0].ToString() + "/" + maxmp[0].ToString();
                    MP[1].text = mp[1].ToString() + "/" + maxmp[1].ToString();
                    HP_Bar[0].fillAmount = (float)(hp[0] * 1.00 / maxhp[0]);
                    MP_Bar[0].fillAmount = (float)(mp[0] * 1.00 / maxmp[0]);
                    EXP_Bar[0].fillAmount = (float)(exp[0] * 1.00 / nexp[0]);
                    HP_Bar[1].fillAmount = (float)(hp[1] * 1.00 / maxhp[1]);
                    MP_Bar[1].fillAmount = (float)(mp[1] * 1.00 / maxmp[1]);
                    EXP_Bar[1].fillAmount = (float)(exp[1] * 1.00 / nexp[1]);
                    //Level and Class
                    LV[0].text = "LV" + lv[0].ToString();
                    LV[1].text = "LV" + lv[1].ToString();
                    Class[0].text = Character_Class_Info_Script.Class_info[cid[0]].name;
                    Class[1].text = Character_Class_Info_Script.Class_info[cid[1]].name;
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
            if (Input.GetKeyDown(KeyCode.Escape)) //Open Settings
            {
                Main_Process.GetComponent<Main_Process>().Menu_Force_Open(4);
                Main_Process.GetComponent<Main_Process>().esckey_up = true;
            }

        }

    }


    public void Update_Boss_Info()
    {
        Boss_Icon.sprite=Main_Process.GetComponentInChildren<Boss_HeadIcon>().icon[boss.id];
    }

}
