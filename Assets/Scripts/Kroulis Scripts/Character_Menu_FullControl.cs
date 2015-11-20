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
    DealDamageToEnemy Player_ATK;
    public Character_Class_Info CI;
    public Main_Process main_process;
    //Character Infos
    Text ATK, DEF, STA, SPI, AGI, BATK, MATK, PDEF, MDEF, CRIR, C_HP, C_MP, C_EXP, C_NEXP, C_Name, C_LV;
    Image C_ICON,weapon,amror,accer;

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
        }
        main_process = GameObject.Find("Main Process").GetComponent<Main_Process>();
	}
	
	// Update is called once per frame
	public void Change(int ? id = null)
    {
        Player_PF = main_process.GetPlayerObject(id);
        Player_Script = main_process.GetPlayerScript(id);
        Player_EXP = main_process.GetPlayerExperience(id);
        Player_Health = Player_PF.GetComponent<Health>();
        Player_Mana = Player_PF.GetComponent<Mana>();
        Player_Defense = Player_PF.GetComponent<Defense>();
        Player_ATK = Player_Script.AttackCollider.GetComponent<DealDamageToEnemy>();
        ATK.text = Player_Script.GetStrength().ToString();
        DEF.text = Player_Defense.defense.ToString();
        STA.text = Player_Script.GetStamina().ToString();
        SPI.text = Player_Script.GetIntelligence().ToString();
        AGI.text = Player_Script.GetAgility().ToString();
        BATK.text = "<color=#ffffffff>" + Player_ATK.GetPhysicalAttackLeftRange().ToString()+"-"+Player_ATK.GetPhysicalAttackRightRange().ToString() + "</color>";
        MATK.text = "<color=#ffffffff>" + Player_ATK.GetMagicalAttackLeftRange().ToString()+"-"+Player_ATK.GetMagicalAttackRightRange().ToString() + "</color>";
        PDEF.text = Player_Defense.PhysicalDefense.ToString();
        MDEF.text = Player_Defense.MagicalDefense.ToString();
        CRIR.text = (Player_ATK.GetCriticalChance()*100).ToString()+"%";
        C_HP.text = Player_Health.GetCurrentHealth().ToString() + "/" + Player_Health.GetStartingHealth().ToString();
        C_MP.text = Player_Mana.GetMana().ToString() + "/" + Player_Mana.MaxMana.ToString();
        C_LV.text = Player_EXP.GetCurrentLevel().ToString();
        C_EXP.text = Player_EXP.GetExperience().ToString();
        C_NEXP.text = (Player_EXP.GetNEXP()-Player_EXP.GetExperience()).ToString();
        C_Name.text = Player_Script.Player_Name;
        weapon.sprite = CI.Class_info[Player_Script.GetClassID()].weapon[Player_Script.GetWeaponLV()].icon;
        amror.sprite = CI.Class_info[Player_Script.GetClassID()].armor[Player_Script.GetWeaponLV()].icon;
        accer.sprite = CI.Class_info[Player_Script.GetClassID()].accessory[Player_Script.GetWeaponLV()].icon;

    }
}
