using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using Kroulis.Verify;

public class SaveAndLoad : MonoBehaviour {
    GameObject Player_PF;
    Player Player_Script;
    Health Player_Health;
    Mana Player_Mana;
    Defense Player_Defense;
    Experience Player_EXP;
    DealDamageToEnemy Player_ATK;
    CoinManager Player_gold;
    XmlDocument character_data=new XmlDocument();
    string path = "";
	// Use this for initialization
	void Start () {

        path = FileVerify.GetPath();

        Player_PF = GameObject.Find("Player");
        Player_Script = Player_PF.GetComponent<Player>();
        Player_Health = Player_PF.GetComponent<Health>();
        Player_Mana = Player_PF.GetComponent<Mana>();
        Player_Defense = Player_PF.GetComponent<Defense>();
        Player_EXP = Player_PF.GetComponent<Experience>();
        Player_ATK = Player_PF.GetComponentInChildren<DealDamageToEnemy>();
        Player_gold = Player_PF.GetComponent<CoinManager>();

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
        //create player1 node
        XmlElement data = character_data.CreateElement("data");
        data.SetAttribute("id", "1");
        root.AppendChild(data);
            //write infomation
            XmlElement pid = character_data.CreateElement("pid");
            pid.InnerText = Globe.Character_id;
            XmlElement c_name = character_data.CreateElement("name");
            c_name.InnerText = Player_Script.Player_Name;
            XmlElement cid = character_data.CreateElement("cid");
            cid.InnerText = Player_Script.GetClassID().ToString();
            XmlElement lv = character_data.CreateElement("lv");
            lv.InnerText = Player_EXP.GetCurrentLevel().ToString();
            XmlElement exp = character_data.CreateElement("exp");
            exp.InnerText = Player_EXP.GetExperience().ToString();
            XmlElement gold = character_data.CreateElement("gold");
            gold.InnerText = Player_gold.getCoins().ToString();
            XmlElement weapon_level = character_data.CreateElement("weapon_level");
            weapon_level.InnerText = Player_Script.GetWeaponLV().ToString();
            XmlElement armor_level = character_data.CreateElement("armor_level");
            armor_level.InnerText = Player_Script.GetAmrorLV().ToString();
            XmlElement accessories_level = character_data.CreateElement("accessories_level");
            accessories_level.InnerText = Player_Script.GetAccessoriesLV().ToString();
            XmlElement atk = character_data.CreateElement("atk");
            atk.InnerText = Player_Script.GetStrength().ToString();
            XmlElement def = character_data.CreateElement("def");
            def.InnerText = Player_Defense.GetDefense().ToString();
            XmlElement sta = character_data.CreateElement("sta");
            sta.InnerText = Player_Script.GetStamina().ToString();
            XmlElement spi = character_data.CreateElement("spi");
            spi.InnerText = Player_Script.GetIntelligence().ToString();
            XmlElement agi = character_data.CreateElement("agi");
            agi.InnerText = Player_Script.GetAgility().ToString();
            XmlElement map = character_data.CreateElement("map");
            map.InnerText = Globe.Map_Load_id.ToString();
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
        //player2 (ignore)

        //save file to a temporary place
        character_data.Save(path + "/saving.xml");
        //encrypt
        string md5;
        md5 = FileVerify.getFileHash(path + "/saving.xml");
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
        string md5;
        md5 = "CB" + FileVerify.getFileHash(path + "/" + Globe.Character_Data_File) + "D";
        Debug.Log(md5);
        //File Verify
        if(Application.platform != RuntimePlatform.WindowsEditor)
        {
            if(md5+".xml"!=Globe.Character_Data_File)
            {
                //Application.OpenURL("www.kroulisworld.com/");
                Application.Quit();
            }
        }
        character_data.Load(path + "/" + Globe.Character_Data_File);
        XmlNode root=character_data.SelectSingleNode("datacounter");
        XmlNodeList character_info = root.ChildNodes;
        int player_id_load;
        foreach(XmlElement xl in character_info)
        {
            if(xl.GetAttribute("id")=="1")
            {
                player_id_load = 1;
            }
            else
            {
                player_id_load = 2;
            }

            XmlNodeList char_info_detail = xl.ChildNodes;
            foreach (XmlElement xl2 in char_info_detail)
            {
                if(xl2.Name=="pid")
                {
                    if (xl2.InnerText!=Globe.Character_id)
                    {
                        Debug.LogWarning("PID Verify failed.");
                    }
                }
                else if(xl2.Name=="name")
                {
                    Player_Script.Player_Name = xl2.InnerText;
                }
                else if(xl2.Name=="cid")
                {
                    Player_Script.SetClassID(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="lv")
                {
                    Player_EXP.SetLevel(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="exp")
                {
                    Player_EXP.SetExperience(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="gold")
                {
                    Player_gold.setCoins(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="weapon_level")
                {
                    Player_Script.SetWeaponLV(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="armor_level")
                {
                    Player_Script.SetArmorLV(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="accessories_level")
                {
                    Player_Script.SetAccessoriesLV(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="atk")
                {
                    Player_Script.SetStrength(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="def")
                {
                    Player_Defense.SetDefense(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="sta")
                {
                    Player_Script.SetStamina(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="spi")
                {
                    Player_Script.SetIntelligence(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="agi")
                {
                    Player_Script.SetAgility(int.Parse(xl2.InnerText));
                }
                else if(xl2.Name=="map")
                {
                    Globe.Map_Load_id = int.Parse(xl2.InnerText);
                }
            }
            Player_EXP.LevelUp();
            Player_Script.Fully_Update();
        }

    }
}
