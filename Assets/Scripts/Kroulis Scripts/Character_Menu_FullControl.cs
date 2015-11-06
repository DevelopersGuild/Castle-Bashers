using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_Menu_FullControl : MonoBehaviour {
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
