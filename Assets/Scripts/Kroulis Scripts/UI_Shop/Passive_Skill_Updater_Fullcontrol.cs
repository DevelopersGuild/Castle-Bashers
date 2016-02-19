using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Passive_Skill_Updater_Fullcontrol : MonoBehaviour {

    //APIs
    //public int shop_class_id = 0;
    private int[] store_skill_id;
    public Sprite null_image;
    //only Inscript
    struct passive_skill_control
    {
        public int level; 
        public Text skillname;
        public Image skillicon;
    };

    passive_skill_control[] shop_skill = new passive_skill_control[15];
    Text Skill_info,Skill_effect;
    Text Tips;
    Image Current;
    Text Point;
    int current_id;
    int max_skill_id;
    int points;
    private int player_id=0;
    private Main_Process MainProcess;

	// Use this for initialization
	void Start () {
        Text[] Result1;
        Result1 = GetComponentsInChildren<Text>();
        foreach (Text t in Result1)
        {
            if(t.name=="P_S_N1")
            {
                shop_skill[1].skillname = t;
                continue;
            }
            if (t.name == "P_S_N2")
            {
                shop_skill[2].skillname = t;
                continue;
            }
            if (t.name == "P_S_N3")
            {
                shop_skill[3].skillname = t;
                continue;
            }
            if (t.name == "P_S_N4")
            {
                shop_skill[4].skillname = t;
                continue;
            }
            if (t.name == "P_S_N5")
            {
                shop_skill[5].skillname = t;
                continue;
            }
            if (t.name == "P_S_N6")
            {
                shop_skill[6].skillname = t;
                continue;
            }
            if (t.name == "P_S_N7")
            {
                shop_skill[7].skillname = t;
                continue;
            }
            if (t.name == "P_S_N8")
            {
                shop_skill[8].skillname = t;
                continue;
            }
            if (t.name == "P_S_N9")
            {
                shop_skill[9].skillname = t;
                continue;
            }
            if (t.name == "P_S_N10")
            {
                shop_skill[10].skillname = t;
                continue;
            }
            if (t.name == "P_S_N11")
            {
                shop_skill[11].skillname = t;
                continue;
            }
            if (t.name == "P_S_N12")
            {
                shop_skill[12].skillname = t;
                continue;
            }
            if (t.name == "P_S_N13")
            {
                shop_skill[13].skillname = t;
                continue;
            }
            if (t.name == "P_S_N14")
            {
                shop_skill[14].skillname = t;
                continue;
            }
            if (t.name == "P_S_P")
            {
                Point = t;
                continue;
            }
            if (t.name == "P_S_EF")
            {
                Skill_effect = t;
                continue;
            }
            if (t.name == "P_S_SI")
            {
                Skill_info = t;
                continue;
            }
            if(t.name=="P_S_tips")
            {
                Tips = t;
                continue;
            }
        }
        Image[] Result2;
        Result2 = GetComponentsInChildren<Image>();
        foreach(Image i in Result2)
        {
            if(i.name=="P_S_I1")
            {
                shop_skill[1].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I2")
            {
                shop_skill[2].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I3")
            {
                shop_skill[3].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I4")
            {
                shop_skill[4].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I5")
            {
                shop_skill[5].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I6")
            {
                shop_skill[6].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I7")
            {
                shop_skill[7].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I8")
            {
                shop_skill[8].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I9")
            {
                shop_skill[9].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I10")
            {
                shop_skill[10].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I11")
            {
                shop_skill[11].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I12")
            {
                shop_skill[12].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I13")
            {
                shop_skill[13].skillicon = i;
                continue;
            }
            if (i.name == "P_S_I14")
            {
                shop_skill[14].skillicon = i;
                continue;
            }
            if (i.name == "P_S_Current")
            {
                Current = i;
                continue;
            }
        }
        MainProcess = GameObject.Find("Main Process").GetComponent<Main_Process>();
        //Change();
        current_id = 1;
        player_id = 0;
        gameObject.SetActive(false);
	}
	
    public void Change(int ? pid=null)
    {
        if(pid==null)
        {
            player_id = 0;
        }
        else
        {
            player_id = (int)pid;
        }
        //shop_class_id = MainProcess.GetPlayerScript(player_id).GetClassID();
        //Classname.text = CCIS.Class_info[shop_class_id].name;
        //store_skill_id = new int[CCIS.Class_info[shop_class_id].skillid.Length];
        //store_skill_id = CCIS.Class_info[shop_class_id].skillid;
        max_skill_id = 1;
        if (max_skill_id == 0)
        {
            this.gameObject.SetActive(false);
        }
        for(int i=1;i<=max_skill_id;i++)
        {
            //shop_skill[i].skillicon.sprite = ;
            shop_skill[i].skillname.text = MainProcess.GetPlayerPassiveSkillManager(player_id).getName(current_id-1);
            shop_skill[i].level = MainProcess.GetPlayerPassiveSkillManager(player_id).getLevel(current_id-1);
        }
        if(max_skill_id+1<=14)
            for (int i = max_skill_id + 1; i <= 14;i++)
            {
                shop_skill[i].skillicon.sprite = null_image;
                shop_skill[i].skillname.text = "";
                shop_skill[i].level = 0;
            }
         current_id = 1;
        //Debug.Log(max_skill_id);
    }

	// Update is called once per frame
	void Update () {
        points = MainProcess.GetPlayerPassiveSkillManager(player_id).getAvailablePoints();
        Point.text = points.ToString();
        Skill_info.text = "Skill Name: " + MainProcess.GetPlayerPassiveSkillManager(player_id).getName(current_id-1) + "\nDescrible:\n" + MainProcess.GetPlayerPassiveSkillManager(player_id).getDescription(current_id-1);
        Skill_effect.text = "Current Level: " + MainProcess.GetPlayerPassiveSkillManager(player_id).getLevel(current_id - 1) + "\nMax Level:" + MainProcess.GetPlayerPassiveSkillManager(player_id).getMaxLevel(current_id - 1);
        //Skill_video.texture=
        Current.GetComponent<RectTransform>().position = shop_skill[current_id].skillicon.GetComponent<RectTransform>().position;
        if(shop_skill[current_id].level>MainProcess.GetPlayerPassiveSkillManager(player_id).getMaxLevel(current_id-1))
        {
            Tips.text = "<color=#00ff00ff>The skill had been updated to the max level, press [ESC] to quit.</color>";
        }
        else if(points>0)
        {
            Tips.text = "<color=#00ff00ff>Press [Enter] to update this skill, press [ESC] to quit.</color>";
        }
        else
        {
            Tips.text = "<color=#ff0000ff>You don't have enough skill points. Press [ESC] to quit.</color>";
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(current_id!=1)
            {
                if(current_id-2<1)
                {
                    current_id = current_id - 1;
                }
                else
                {
                    current_id = current_id - 2;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (current_id != max_skill_id)
            {
                if (current_id + 2 > max_skill_id)
                {
                    current_id = current_id + 1;
                }
                else
                {
                    current_id = current_id + 2;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (current_id != 1)
            {
                current_id = current_id - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (current_id != max_skill_id)
            {
                current_id = current_id + 1;
            }
        }

        if(points>0 && shop_skill[current_id].level<MainProcess.GetPlayerPassiveSkillManager(player_id).getMaxLevel(current_id-1))
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                MainProcess.GetPlayerPassiveSkillManager().addPointToSkill(PassiveSkillManager.Passives.StrengthBonus);
                shop_skill[current_id].level++;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            MainProcess.OtherWindows_Close();
        }
	}
}
