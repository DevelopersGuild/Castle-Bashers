using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_Menu_FullControl : MonoBehaviour {
    GameObject Player_PF;
    Player Player_Script;
    Health Player_Health;
    Mana Player_Mana;
    Defense Player_Defense;
    Experience Player_EXP;
    DealDamage Player_ATK;
    UI_GEM_FullControl gem_system;
    public Character_Class_Info CI;
    public Main_Process main_process;
    //Character Infos
    Text ATK, DEF, STA, SPI, AGI, BATK, MATK, PDEF, MDEF, CRIR, C_HP, C_MP, C_EXP, C_NEXP, C_Name, C_LV, C_PASP;
    Image C_ICON, weapon, amror, accer, cm_current,upgrade;

    private int select_current;
    private int playerid;
    [HideInInspector]
    public bool gem_selecting;
    [HideInInspector]
    public bool passive_selecting;
	// Use this for initialization
	void Start () {
        Text[] finds1;
        Image[] finds2;
        finds1 = GetComponentsInChildren<Text>();
        foreach (Text t in finds1)
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
            if(t.name=="C_PASPoint")
            {
                C_PASP = t;
                continue;
            }

        }
        finds2 = GetComponentsInChildren<Image>();
        foreach (Image i in finds2)
        {
            if (i.name == "C_ICON")
            {
                C_ICON = i;
                continue;
            }
            if (i.name == "C_WEP")
            {
                weapon = i;
                continue;
            }
            if (i.name == "C_AMR")
            {
                amror = i;
                continue;
            }
            if (i.name == "C_ACC")
            {
                accer = i;
                continue;
            }
            if(i.name=="CM_Current")
            {
                cm_current = i;
                continue;
            }
            if(i.name=="CM_Hover_Upgrade")
            {
                upgrade = i;
                continue;
            }
        }
        main_process = GameObject.Find("Main Process").GetComponent<Main_Process>();
        gem_system = GetComponentInChildren<UI_GEM_FullControl>();
        gem_system.mainp = main_process;
        select_current = 0;
        gem_selecting = false;
        passive_selecting = false;
        playerid = 0;
	}
	
	// Update is called once per frame
	public void Change(int ? id = null)
    {
        if (id == null)
            playerid = 0;
        else
            playerid = (int)id;
        Player_PF = main_process.GetPlayerObject(id);
        Player_Script = main_process.GetPlayerScript(id);
        Player_EXP = main_process.GetPlayerExperience(id);
        Player_Health = Player_PF.GetComponent<Health>();
        Player_Mana = Player_PF.GetComponent<Mana>();
        Player_Defense = Player_PF.GetComponent<Defense>();
        Player_ATK = Player_Script.AttackCollider.GetComponent<DealDamage>();
        //if (!Player_ATK)
        //    Debug.Log("Cannot Get Player_ATK");
        ATK.text = Player_Script.GetStrength().ToString();
        DEF.text = Player_Defense.defense.ToString();
        STA.text = Player_Script.GetStamina().ToString();
        SPI.text = Player_Script.GetIntelligence().ToString();
        AGI.text = Player_Script.GetAgility().ToString();
        BATK.text = "<color=#ffffffff>" + Player_ATK.GetDamageI().ToString() + "-" + Player_ATK.GetDamageI().ToString() + "</color>";
        MATK.text = "<color=#ffffffff>" + Player_ATK.GetDamageI().ToString() + "-" + Player_ATK.GetDamageI().ToString() + "</color>";
        PDEF.text = Player_Defense.GetPhysicalDefense().ToString();
        MDEF.text = Player_Defense.GetMagicalDefense().ToString();
        //CRIR.text = (Player_ATK.GetCriticalChance()*100).ToString()+"%";
        C_HP.text = Player_Health.GetCurrentHealth().ToString() + "/" + Player_Health.GetMaxHP().ToString();
        CRIR.text = "0.00%";
        C_MP.text = Player_Mana.GetMana().ToString() + "/" + Player_Mana.MaxMana.ToString();
        C_LV.text = Player_EXP.GetCurrentLevel().ToString();
        C_EXP.text = Player_EXP.GetExperience().ToString();
        C_NEXP.text = (Player_EXP.GetNEXP()-Player_EXP.GetExperience()).ToString();
        C_Name.text = Player_Script.Player_Name;
        weapon.sprite = CI.Class_info[Player_Script.GetClassID()].weapon[Player_Script.GetWeaponLV()].icon;
        amror.sprite = CI.Class_info[Player_Script.GetClassID()].armor[Player_Script.GetWeaponLV()].icon;
        accer.sprite = CI.Class_info[Player_Script.GetClassID()].accessory[Player_Script.GetWeaponLV()].icon;
        C_PASP.text = Player_PF.GetComponent<PassiveSkillManager>().getAvailablePoints().ToString();
        select_current = 1;
        gem_selecting = false;
        passive_selecting = false;
    }

    void Update()
    {
        switch (select_current)
        {
            case 1: //weapon
                upgrade.gameObject.SetActive(false);
                cm_current.gameObject.SetActive(true);
                cm_current.transform.localPosition = weapon.transform.localPosition;
                break;
            case 2: //amr
                upgrade.gameObject.SetActive(false);
                cm_current.gameObject.SetActive(true);
                cm_current.transform.localPosition = amror.transform.localPosition;
                break;
            case 3: //acy
                upgrade.gameObject.SetActive(false);
                cm_current.gameObject.SetActive(true);
                cm_current.transform.localPosition = accer.transform.localPosition;
                break;
            case 4://upgrade passive
                cm_current.gameObject.SetActive(false);
                upgrade.gameObject.SetActive(true);
                break;
        }
        if(!gem_selecting && !passive_selecting)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                select_current = select_current == 4 ? 1 : select_current + 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                select_current = select_current == 1 ? 4 : select_current - 1;
            }
            //Gems
            if(select_current<4)
            {
                gem_system.playerid = playerid;
                gem_system.change = select_current;
                //Start Selecting Gems
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    gem_system.selecting=1;
                    gem_system.subselecting = false;
                    gem_selecting = true;
                    gem_system.changing = true;
                }
            }
            //Passive Skills
            else
            {
                //Start Selecting Passive Skills
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    main_process.UI_Passive_Skill_Panel_Open(playerid);
                }
            }
        }

    }
}
