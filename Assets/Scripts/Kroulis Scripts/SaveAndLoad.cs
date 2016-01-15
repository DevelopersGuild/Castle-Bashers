using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using Kroulis.Verify;
using Kroulis.Error;

public class SaveAndLoad : MonoBehaviour {
    GameObject PlayerHolder;
    GameObject[] Player_PF=new GameObject[2];
    Player[] Player_Script=new Player[2];
    Health[] Player_Health=new Health[2];
    Mana[] Player_Mana=new Mana[2];
    Defense[] Player_Defense=new Defense[2];
    Experience[] Player_EXP=new Experience[2];
    DealDamageToEnemy[] Player_ATK=new DealDamageToEnemy[2];
    CoinManager[] Player_gold=new CoinManager[2];
    XmlDocument character_data=new XmlDocument();
    bool twoplayer = false;
    string path = "";
	// Use this for initialization
	void Start () {

        path = FileVerify.GetPath();

        PlayerHolder = GameObject.Find("PlayerHolder");
        Player_Script = PlayerHolder.GetComponentsInChildren<Player>();
        //Debug.Log("Player Object Found: " + Player_Script.Length.ToString());
        for (int i = 0; i <= 1;i++ )
        {
            Player_PF[i] = Player_Script[i].gameObject;
            Player_Health[i] = Player_PF[i].GetComponent<Health>();
            Player_Mana[i] = Player_PF[i].GetComponent<Mana>();
            Player_Defense[i] = Player_PF[i].GetComponent<Defense>();
            Player_EXP[i] = Player_PF[i].GetComponent<Experience>();
            Player_ATK[i] = Player_PF[i].GetComponentInChildren<DealDamageToEnemy>();
            Player_gold[i] = Player_PF[i].GetComponent<CoinManager>();
        }
            
        Invoke("LoadData",1.00f);
        //character_data.Load(path + "/" + Globe.Character_Data_File);
        //character_data.Save();
	}
	
	public void SaveData()
    {
        character_data = new XmlDocument();
        //Set Declaration
        XmlDeclaration xmldec = character_data.CreateXmlDeclaration("1.0", "UTF-8", null);
        character_data.AppendChild(xmldec);
        //create root node
        XmlElement root = character_data.CreateElement("datacounter");
        character_data.AppendChild(root);
        for (int i = 0; i <= (twoplayer?1:0);i++)
        {
            //create player1 node
            XmlElement data = character_data.CreateElement("data");
            data.SetAttribute("id", (i+1).ToString());
            root.AppendChild(data);
            //write infomation
            XmlElement pid = character_data.CreateElement("pid");
            pid.InnerText = Globe.Character_id;
            XmlElement c_name = character_data.CreateElement("name");
            c_name.InnerText = Player_Script[i].Player_Name;
            XmlElement cid = character_data.CreateElement("cid");
            cid.InnerText = Player_Script[i].GetClassID().ToString();
            XmlElement lv = character_data.CreateElement("lv");
            lv.InnerText = Player_EXP[i].GetCurrentLevel().ToString();
            XmlElement exp = character_data.CreateElement("exp");
            exp.InnerText = Player_EXP[i].GetExperience().ToString();
            XmlElement gold = character_data.CreateElement("gold");
            gold.InnerText = Player_gold[i].getCoins().ToString();
            XmlElement weapon_level = character_data.CreateElement("weapon_level");
            weapon_level.InnerText = Player_Script[i].GetWeaponLV().ToString();
            XmlElement armor_level = character_data.CreateElement("armor_level");
            armor_level.InnerText = Player_Script[i].GetAmrorLV().ToString();
            XmlElement accessories_level = character_data.CreateElement("accessories_level");
            accessories_level.InnerText = Player_Script[i].GetAccessoriesLV().ToString();
            XmlElement atk = character_data.CreateElement("atk");
            atk.InnerText = Player_Script[i].GetStrength().ToString();
            XmlElement def = character_data.CreateElement("def");
            def.InnerText = Player_Defense[i].GetDefense().ToString();
            XmlElement sta = character_data.CreateElement("sta");
            sta.InnerText = Player_Script[i].GetStamina().ToString();
            XmlElement spi = character_data.CreateElement("spi");
            spi.InnerText = Player_Script[i].GetIntelligence().ToString();
            XmlElement agi = character_data.CreateElement("agi");
            agi.InnerText = Player_Script[i].GetAgility().ToString();
            XmlElement map = character_data.CreateElement("map");
            map.InnerText = Globe.Map_Load_id.ToString();
            bool[] skill = Player_Script[i].GetUnlockSkillList();
            XmlElement skillinfo = character_data.CreateElement("skill");
            string blskill = "";
            for (int j = 0; j < skill.Length;j++)
            {
                blskill = blskill + (skill[j] ? "1" : "0");
            }
            skillinfo.InnerText = blskill;
            data.AppendChild(pid);
            data.AppendChild(c_name);
            data.AppendChild(cid);
            data.AppendChild(lv);
            data.AppendChild(gold);
            data.AppendChild(weapon_level);
            data.AppendChild(armor_level);
            data.AppendChild(accessories_level);
            data.AppendChild(atk);
            data.AppendChild(def);
            data.AppendChild(sta);
            data.AppendChild(spi);
            data.AppendChild(agi);
            data.AppendChild(map);
            data.AppendChild(skillinfo);
        }
        //save file to a temporary place
        character_data.Save(path + "/saving.xml");
        //encrypt
        string md5;
        md5 = FileVerify.getFileHash(path + "/saving.xml");
        if (File.Exists(path + "/CB" + md5 + "D.xml"))
            File.Delete(path + "/CB" + md5 + "D.xml");
        File.Move(path + "/saving.xml", path + "/CB" + md5 + "D.xml");
        if (File.Exists(path + "/saving.xml"))
        {
            File.Delete(path + "/saving.xml");
        }
        Debug.Log("Data saved:" + path + "/CB" + md5 + "D.xml");
        //Change File Infomation
        Globe.Character_Data_File = "CB" + md5 + "D.xml";
        character_data = new XmlDocument();
        character_data.Load(path + "/config.xml");
        XmlNode config = character_data.SelectSingleNode("config");
        XmlNodeList findresult = config.ChildNodes;
        foreach(XmlElement xl in findresult)
        {
            if(xl.Name=="savefile")
            {
                xl.InnerText = "CB" + md5 + "D.xml";
                break;
            }
        }
        character_data.Save(path + "/config.xml");


    }

    public void LoadData()
    {
        CancelInvoke();
        if(Application.platform==RuntimePlatform.WindowsEditor)
        {
            bool[] test = { false, true, true, true, true, true, true, true };
            Player_Script[0].SetUnlockSkillList(test);
        }
        if (File.Exists(path + "/" + Globe.Character_Data_File) == false || Globe.Character_Data_File=="null")
        {
            ErrorCatching.WriteCharacterDataXML();
            Application.Quit();
        }
        else
        {
            string md5;
            md5 = "CB" + FileVerify.getFileHash(path + "/" + Globe.Character_Data_File) + "D";
            Debug.Log(md5);
            //File Verify
            if (Application.platform != RuntimePlatform.WindowsEditor)
            {
                if (md5 + ".xml" != Globe.Character_Data_File)
                {
                    ErrorCatching.WriteVerifyXML();
                    //Application.OpenURL("www.kroulisworld.com/");
                    Application.Quit();
                }
            }
            character_data.Load(path + "/" + Globe.Character_Data_File);
            XmlNode root = character_data.SelectSingleNode("datacounter");
            XmlNodeList character_info = root.ChildNodes;
            int player_id_load;
            foreach (XmlElement xl in character_info)
            {
                if (xl.GetAttribute("id") == "1")
                {
                    player_id_load = 1;
                }
                else
                {
                    player_id_load = 2;
                    twoplayer = true;
                }

                XmlNodeList char_info_detail = xl.ChildNodes;
                foreach (XmlElement xl2 in char_info_detail)
                {
                    if (xl2.Name == "pid")
                    {
                        if (xl2.InnerText != Globe.Character_id)
                        {
                            Debug.LogWarning("PID Verify failed.");
                        }
                    }
                    else if (xl2.Name == "name")
                    {
                        Player_Script[player_id_load-1].Player_Name = xl2.InnerText;
                    }
                    else if (xl2.Name == "cid")
                    {
                        Player_Script[player_id_load - 1].SetClassID(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "lv")
                    {
                        Player_EXP[player_id_load - 1].SetLevel(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "exp")
                    {
                        Player_EXP[player_id_load - 1].SetExperience(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "gold")
                    {
                        Player_gold[player_id_load - 1].setCoins(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "weapon_level")
                    {
                        Player_Script[player_id_load - 1].SetWeaponLV(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "armor_level")
                    {
                        Player_Script[player_id_load - 1].SetArmorLV(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "accessories_level")
                    {
                        Player_Script[player_id_load - 1].SetAccessoriesLV(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "atk")
                    {
                        Player_Script[player_id_load - 1].SetStrength(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "def")
                    {
                        Player_Defense[player_id_load - 1].SetDefense(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "sta")
                    {
                        Player_Script[player_id_load - 1].SetStamina(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "spi")
                    {
                        Player_Script[player_id_load - 1].SetIntelligence(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "agi")
                    {
                        Player_Script[player_id_load - 1].SetAgility(int.Parse(xl2.InnerText));
                    }
                    else if (xl2.Name == "map")
                    {
                        Globe.Map_Load_id = int.Parse(xl2.InnerText);
                    }
                    else if (xl2.Name == "skill")
                    {
                        bool[] skillinfo = new bool[xl2.InnerText.Length];
                        for (int i = 0; i < xl2.InnerText.Length; i++)
                        {
                            if (xl2.InnerText[i] == '1')
                                skillinfo[i] = true;
                            else
                                skillinfo[i] = false;
                        }
                        Player_Script[player_id_load - 1].SetUnlockSkillList(skillinfo);
                    }
                }
                Player_EXP[player_id_load - 1].LevelUp();
                Player_Script[player_id_load - 1].Fully_Update();
            }
        }
        if (twoplayer == false)
        {
            GetComponent<Main_Process>().One_player_per_client = true;
            Player_PF[1].SetActive(false);
            //Destroy(Player_PF[1]);
            //Debug.Log("1 player setuped.");
        }
        else
            GetComponent<Main_Process>().One_player_per_client = false;
        Invoke("UpdateSkill", 3.0f);
        //twoplayer = true;
    }

    void UpdateSkill()
    {
        Player_Script[0].UpdateSkillSlot();
        if (twoplayer)
            Player_Script[1].UpdateSkillSlot();
        CancelInvoke();
    }
}
