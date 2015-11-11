using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill_Shop_Fullcontrol : MonoBehaviour {

    //APIs
    public int shop_class_id = 0;
    public int[] store_skill_id;
    public Sprite null_image;
    //only Inscript
    struct shop_skill_control
    {
        public int id;
        public bool have; 
        public Text skillname;
        public Image skillicon;
    };

    shop_skill_control[] shop_skill = new shop_skill_control[15];
    Text Skill_info;
    Text Tips;
    Image Current;
    RawImage Skill_video;
    Text Gold;
    Text Classname;
    int current_id;
    int max_skill_id;
    int need_gold, gold;
    Character_Class_Info CCIS;
	// Use this for initialization
	void Start () {
        Text[] Result1;
        Result1 = GetComponentsInChildren<Text>();
        foreach (Text t in Result1)
        {
            if(t.name=="S_H_N1")
            {
                shop_skill[1].skillname = t;
                continue;
            }
            if (t.name == "S_H_N2")
            {
                shop_skill[2].skillname = t;
                continue;
            }
            if (t.name == "S_H_N3")
            {
                shop_skill[3].skillname = t;
                continue;
            }
            if (t.name == "S_H_N4")
            {
                shop_skill[4].skillname = t;
                continue;
            }
            if (t.name == "S_H_N5")
            {
                shop_skill[5].skillname = t;
                continue;
            }
            if (t.name == "S_H_N6")
            {
                shop_skill[6].skillname = t;
                continue;
            }
            if (t.name == "S_H_N7")
            {
                shop_skill[7].skillname = t;
                continue;
            }
            if (t.name == "S_H_N8")
            {
                shop_skill[8].skillname = t;
                continue;
            }
            if (t.name == "S_H_N9")
            {
                shop_skill[9].skillname = t;
                continue;
            }
            if (t.name == "S_H_N10")
            {
                shop_skill[10].skillname = t;
                continue;
            }
            if (t.name == "S_H_N11")
            {
                shop_skill[11].skillname = t;
                continue;
            }
            if (t.name == "S_H_N12")
            {
                shop_skill[12].skillname = t;
                continue;
            }
            if (t.name == "S_H_N13")
            {
                shop_skill[13].skillname = t;
                continue;
            }
            if (t.name == "S_H_N14")
            {
                shop_skill[14].skillname = t;
                continue;
            }
            if (t.name == "S_H_G")
            {
                Gold = t;
                continue;
            }
            if (t.name == "S_H_C")
            {
                Classname = t;
                continue;
            }
            if (t.name == "S_H_SI")
            {
                Skill_info = t;
                continue;
            }
            if(t.name=="S_H_tips")
            {
                Tips = t;
                continue;
            }
        }
        Image[] Result2;
        Result2 = GetComponentsInChildren<Image>();
        foreach(Image i in Result2)
        {
            if(i.name=="S_H_I1")
            {
                shop_skill[1].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I2")
            {
                shop_skill[2].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I3")
            {
                shop_skill[3].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I4")
            {
                shop_skill[4].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I5")
            {
                shop_skill[5].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I6")
            {
                shop_skill[6].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I7")
            {
                shop_skill[7].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I8")
            {
                shop_skill[8].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I9")
            {
                shop_skill[9].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I10")
            {
                shop_skill[10].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I11")
            {
                shop_skill[11].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I12")
            {
                shop_skill[12].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I13")
            {
                shop_skill[13].skillicon = i;
                continue;
            }
            if (i.name == "S_H_I14")
            {
                shop_skill[14].skillicon = i;
                continue;
            }
            if (i.name == "S_H_Current")
            {
                Current = i;
                continue;
            }
        }
        Skill_video = GetComponentInChildren<RawImage>();
        GameObject MainProcess = GameObject.Find("Main Process");
        CCIS= MainProcess.GetComponentInChildren<Character_Class_Info>();
        Change();
	}
	
    public void Change()
    {
        Classname.text = CCIS.Class_info[shop_class_id].name;
        max_skill_id = store_skill_id.Length;
        if(max_skill_id==0)
        {
            this.gameObject.SetActive(false);
        }
        for(int i=1;i<=max_skill_id;i++)
        {
            //shop_skill[i].skillicon.sprite=
            //shop_skill[i].skillname.text=
            //shop_skill[i].have=
            shop_skill[i].id = store_skill_id[i - 1];
        }
        if(max_skill_id+1<=14)
            for (int i = max_skill_id + 1; i <= 14;i++)
            {
                shop_skill[i].skillicon.sprite = null_image;
                shop_skill[i].skillname.text = "";
                shop_skill[i].have = false;
                shop_skill[i].id = 0;
            }
         current_id = 1;
        //Debug.Log(max_skill_id);
    }

	// Update is called once per frame
	void Update () {
        //gold=
        Gold.text = gold.ToString();
        //need_gold=
        //Skill_info.text=
        //Skill_video.texture=
        Current.GetComponent<RectTransform>().position = shop_skill[current_id].skillicon.GetComponent<RectTransform>().position;
        if(shop_skill[current_id].have==true)
        {
            Tips.text = "<color=#00ff00ff>You had already bought this skill, press [ESC] to quit the store.</color>";
        }
        else if(gold>=need_gold)
        {
            Tips.text = "<color=#00ff00ff>Need Gold:" + need_gold.ToString() + "</color>\n" + "<color=#00ff00ff>Press [Enter] to buy this skill, press [ESC] to quit the store.</color>";
        }
        else
        {
            Tips.text = "<color=#ff0000ff>Need Gold:" + need_gold.ToString() + "</color>\n" + "<color=#ff0000ff>You money is not enough. Press [ESC] to quit the store.</color>";
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

	}
}
