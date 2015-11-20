using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Equipment_Upgrade_FullControl : MonoBehaviour {

    private Main_Process main_process;
    private Character_Class_Info ci;
    private Player player;
    public GameObject Weapon;
    public GameObject Amror;
    public GameObject Accessories;
    public Image Old, New;
    public Text LV, GO, HGO, Tips;
    public Text[] weapons= new Text[3], amrors= new Text[3], accessories = new Text[3]; 

    private int currentlevel,nextlevel;
    private int classid;
    private int havegold, needgold;
    private int playerlv,needlv;
    private int[] diff = new int[3];
    private bool upg = true;
    private int current_id=0;
	// Use this for initialization
	void Start () {
        main_process = GameObject.Find("Main Process").GetComponent<Main_Process>();
        ci = main_process.gameObject.GetComponentInChildren<Character_Class_Info>();
        gameObject.SetActive(false);
	}

    public void Change(int id,int? player_id=null)
    {
        player = main_process.GetPlayerScript(player_id);
        classid = player.GetClassID();
        havegold = main_process.GetPlayerCoinManager(player_id).getCoins();
        playerlv = main_process.GetPlayerExperience(player_id).GetCurrentLevel();
        current_id = id;
        upg = true;

        GO.text = havegold.ToString();

        if(id==1)
        {
            Amror.SetActive(false);
            Accessories.SetActive(false);
            Weapon.SetActive(true);
            currentlevel = player.GetWeaponLV();
            nextlevel = currentlevel + 1;
            Old.sprite = ci.Class_info[classid].weapon[currentlevel].icon;
            New.sprite = ci.Class_info[classid].weapon[nextlevel].icon;
            needlv = ci.Class_info[classid].weapon[nextlevel].lim_lv;
            needgold = ci.Class_info[classid].weapon[nextlevel].up_gold;
            if(playerlv<needlv)
            {
                LV.text = "<color=#ff0000ff>" + needlv.ToString() + "</color>";
                upg = false;
            }
            else
                LV.text =needlv.ToString();

            if (havegold < needgold)
            {
                GO.text = "<color=#ff0000ff>" + needgold.ToString() + "</color>";
                upg = false;
            }
            else
                GO.text = needgold.ToString();
            //atk
            diff[0] = ci.Class_info[classid].weapon[nextlevel].atk - ci.Class_info[classid].weapon[currentlevel].atk;
            diff[1] = diff[0] * 5;
            diff[2] = diff[0] * 2;
            for (int i = 0; i <= 2; i++)
                weapons[i].text = diff[i].ToString();
            if(upg)
            {
                Tips.text = "...Press [ENTER] To Upgrade, Press [ESC] to Cancel...";
            }
            else
            {
                Tips.text = "...Press [ESC] to Cancel...";
            }

        }
        else if(id==2)
        {
            Accessories.SetActive(false);
            Weapon.SetActive(false);
            Amror.SetActive(true);
            currentlevel = player.GetAmrorLV();
            nextlevel = currentlevel + 1;
            Old.sprite = ci.Class_info[classid].armor[currentlevel].icon;
            New.sprite = ci.Class_info[classid].armor[nextlevel].icon;
            needlv = ci.Class_info[classid].armor[nextlevel].lim_lv;
            needgold = ci.Class_info[classid].armor[nextlevel].up_gold;
            if (playerlv < needlv)
            {
                LV.text = "<color=#ff0000ff>" + needlv.ToString() + "</color>";
                upg = false;
            }
            else
                LV.text = needlv.ToString();

            if (havegold < needgold)
            {
                GO.text = "<color=#ff0000ff>" + needgold.ToString() + "</color>";
                upg = false;
            }
            else
                GO.text = needgold.ToString();
            //def
            diff[0] = ci.Class_info[classid].armor[nextlevel].def - ci.Class_info[classid].armor[currentlevel].def;
            diff[1] = diff[0] * 5;
            diff[2] = diff[0] * 2;
            for (int i = 0; i <= 2; i++)
                amrors[i].text = diff[i].ToString();
            if (upg)
            {
                Tips.text = "...Press [ENTER] To Upgrade, Press [ESC] to Cancel...";
            }
            else
            {
                Tips.text = "...Press [ESC] to Cancel...";
            }
        }
        else //id=3
        {
            Weapon.SetActive(false);
            Amror.SetActive(false);
            Accessories.SetActive(true);
            currentlevel = player.GetAccessoriesLV();
            nextlevel = currentlevel + 1;
            Old.sprite = ci.Class_info[classid].accessory[currentlevel].icon;
            New.sprite = ci.Class_info[classid].accessory[nextlevel].icon;
            needlv = ci.Class_info[classid].accessory[nextlevel].lim_lv;
            needgold = ci.Class_info[classid].accessory[nextlevel].up_gold;
            if (playerlv < needlv)
            {
                LV.text = "<color=#ff0000ff>" + needlv.ToString() + "</color>";
                upg = false;
            }
            else
                LV.text = needlv.ToString();

            if (havegold < needgold)
            {
                GO.text = "<color=#ff0000ff>" + needgold.ToString() + "</color>";
                upg = false;
            }
            else
                GO.text = needgold.ToString();
            //maxhp,maxmp,cri
            diff[0] = ci.Class_info[classid].accessory[nextlevel].maxhp - ci.Class_info[classid].accessory[currentlevel].maxhp;
            diff[1] = ci.Class_info[classid].accessory[nextlevel].maxmp - ci.Class_info[classid].accessory[currentlevel].maxmp;
            diff[2] = ci.Class_info[classid].accessory[nextlevel].cri - ci.Class_info[classid].accessory[currentlevel].cri;
            for (int i = 0; i <= 2; i++)
                accessories[i].text = diff[i].ToString();
            if (upg)
            {
                Tips.text = "...Press [ENTER] To Upgrade, Press [ESC] to Cancel...";
            }
            else
            {
                Tips.text = "...Press [ESC] to Cancel...";
            }
        }
    }
	
    void Update()
    {
        if(upg && current_id!=0)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                switch(current_id)
                {
                    case 1:
                        {
                            player.AddStrength(diff[0]);
                            player.Fully_Update();
                            player.SetWeaponLV(nextlevel);
                            break;
                        }
                    case 2:
                        {
                            player.AddDefense(diff[0]);
                            player.Fully_Update();
                            player.SetArmorLV(nextlevel);
                            break;
                        }
                    case 3:
                        {
                            int sta=diff[0] / 30;
                            player.AddStamina(sta);
                            player.AddIntelligence((diff[1] - sta * 10) / 20);
                            player.AddAgility(diff[2]);
                            player.Fully_Update();
                            player.SetAccessoriesLV(nextlevel);
                            break;
                        }
                }
                current_id = 0;
                gameObject.SetActive(false);
                main_process.Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
                main_process.OtherWindows_Close();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            current_id = 0;
            gameObject.SetActive(false);
            main_process.Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
            main_process.OtherWindows_Close();
        }
    }
}
