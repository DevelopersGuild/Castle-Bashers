using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_Ability_Fullcontrol : MonoBehaviour {

    //APIs
    public int class_id = 0;
    public Sprite null_image;
    public bool change_slot;
    public int change_slot_id;
    //only Inscript
    struct ability_control
    {
        public int id;
        public bool have;
        public Text skillname;
        public Image skillicon;
    };

    ability_control[] ability= new ability_control[15];
    Text Skill_info;
    Text Tips;
    Image Current;
    Image[] Quick_slot=new Image[5];
    RawImage Skill_video;
    int current_id;
    int max_skill_id;
    Character_Class_Info CCIS;
    bool key_up;
    // Use this for initialization
    void Start()
    {
        Text[] Result1;
        Result1 = GetComponentsInChildren<Text>();
        foreach (Text t in Result1)
        {
            if (t.name == "A_M_N1")
            {
                ability[1].skillname = t;
                continue;
            }
            if (t.name == "A_M_N2")
            {
                ability[2].skillname = t;
                continue;
            }
            if (t.name == "A_M_N3")
            {
                ability[3].skillname = t;
                continue;
            }
            if (t.name == "A_M_N4")
            {
                ability[4].skillname = t;
                continue;
            }
            if (t.name == "A_M_N5")
            {
                ability[5].skillname = t;
                continue;
            }
            if (t.name == "A_M_N6")
            {
                ability[6].skillname = t;
                continue;
            }
            if (t.name == "A_M_N7")
            {
                ability[7].skillname = t;
                continue;
            }
            if (t.name == "A_M_N8")
            {
                ability[8].skillname = t;
                continue;
            }
            if (t.name == "A_M_N9")
            {
                ability[9].skillname = t;
                continue;
            }
            if (t.name == "A_M_N10")
            {
                ability[10].skillname = t;
                continue;
            }
            if (t.name == "A_M_N11")
            {
                ability[11].skillname = t;
                continue;
            }
            if (t.name == "A_M_N12")
            {
                ability[12].skillname = t;
                continue;
            }
            if (t.name == "A_M_N13")
            {
                ability[13].skillname = t;
                continue;
            }
            if (t.name == "A_M_N14")
            {
                ability[14].skillname = t;
                continue;
            }
            if (t.name == "A_M_SI")
            {
                Skill_info = t;
                continue;
            }
            if (t.name == "A_M_tips")
            {
                Tips = t;
                continue;
            }
        }
        Image[] Result2;
        Result2 = GetComponentsInChildren<Image>();
        foreach (Image i in Result2)
        {
            if (i.name == "A_M_I1")
            {
                ability[1].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I2")
            {
                ability[2].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I3")
            {
                ability[3].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I4")
            {
                ability[4].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I5")
            {
                ability[5].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I6")
            {
                ability[6].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I7")
            {
                ability[7].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I8")
            {
                ability[8].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I9")
            {
                ability[9].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I10")
            {
                ability[10].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I11")
            {
                ability[11].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I12")
            {
                ability[12].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I13")
            {
                ability[13].skillicon = i;
                continue;
            }
            if (i.name == "A_M_I14")
            {
                ability[14].skillicon = i;
                continue;
            }
            if (i.name == "A_M_Current")
            {
                Current = i;
                continue;
            }
            if (i.name == "A_M_Q1")
            {
                Quick_slot[1] = i;
                continue;
            }
            if (i.name == "A_M_Q2")
            {
                Quick_slot[2] = i;
                continue;
            }
            if (i.name == "A_M_Q3")
            {
                Quick_slot[3] = i;
                continue;
            }
            if (i.name == "A_M_Q4")
            {
                Quick_slot[4] = i;
                continue;
            }
        }
        Skill_video = GetComponentInChildren<RawImage>();
        GameObject MainProcess = GameObject.Find("Main Process");
        CCIS = MainProcess.GetComponentInChildren<Character_Class_Info>();
        Change();
    }

    public void Change()
    {
        max_skill_id = CCIS.Class_info[class_id].skillid.Length;
        if (max_skill_id == 0)
        {
            this.gameObject.SetActive(false);
        }
        for (int i = 1; i <= max_skill_id; i++)
        {
            //ability[i].skillicon.sprite=
            //ability[i].skillname.text=
            /*if(have==false)
             * ability[i].skillicon.color = Color.gray;
             * else
             * ability[i].skillicon.color = Color.white;
             */

            //ability[i].have=
            ability[i].id = CCIS.Class_info[class_id].skillid[i - 1];
        }
        if (max_skill_id + 1 <= 14)
            for (int i = max_skill_id + 1; i <= 14; i++)
            {
                ability[i].skillicon.sprite = null_image;
                ability[i].skillicon.color = Color.white;
                ability[i].skillname.text = "";
                ability[i].have = false;
                ability[i].id = 0;
            }
        current_id = 1;
        Tips.text = "";
        change_slot = false;
        change_slot_id = 0;
        key_up = false;
        //Debug.Log(max_skill_id);
    }

    // Update is called once per frame
    void Update()
    {
        //Skill_info.text=
        //Skill_video.texture=
        Current.GetComponent<RectTransform>().position = ability[current_id].skillicon.GetComponent<RectTransform>().position;


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (current_id != 1)
            {
                if (current_id - 2 < 1)
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

        if(change_slot==false)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                change_slot = true;
                change_slot_id = 1;
                key_up = true;
                Tips.text = "<color=#00ff00ff>You are changing the Quick Slot 1</color>\n" + "<color=#00ff00ff>Press [Enter] to put this skill into the slot, press [Q] to cancel.</color>";
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                change_slot = true;
                change_slot_id = 2;
                key_up = true;
                Tips.text = "<color=#00ff00ff>You are changing the Quick Slot 2</color>\n" + "<color=#00ff00ff>Press [Enter] to put this skill into the slot, press [W] to cancel.</color>";
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                change_slot = true;
                change_slot_id = 3;
                key_up = true;
                Tips.text = "<color=#00ff00ff>You are changing the Quick Slot 3</color>\n" + "<color=#00ff00ff>Press [Enter] to put this skill into the slot, press [E] to cancel.</color>";
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                change_slot = true;
                change_slot_id = 4;
                key_up = true;
                Tips.text = "<color=#00ff00ff>You are changing the Quick Slot 4</color>\n" + "<color=#00ff00ff>Press [Enter] to put this skill into the slot, press [R] to cancel.</color>";
            }
        }
        else
        {
            if(key_up==true)
            {
                switch (change_slot_id)
                {
                    case 1:
                        {
                            if (Input.GetKeyUp(KeyCode.Q))
                            {
                                key_up = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (Input.GetKeyUp(KeyCode.W))
                            {
                                key_up = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Input.GetKeyUp(KeyCode.E))
                            {
                                key_up = false;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (Input.GetKeyUp(KeyCode.R))
                            {
                                key_up = false;
                            }
                            break;
                        }
                }
            }
            else
            {
                switch(change_slot_id)
                {
                    case 1:
                        {
                            if (Input.GetKeyDown(KeyCode.Q))
                            {
                                change_slot = false;
                                change_slot_id = 0;
                                Tips.text = "";
                            }
                            break;
                        }
                    case 2:
                        {
                            if (Input.GetKeyDown(KeyCode.W))
                            {
                                change_slot = false;
                                change_slot_id = 0;
                                Tips.text = "";
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                change_slot = false;
                                change_slot_id = 0;
                                Tips.text = "";
                            }
                            break;
                        }
                    case 4:
                        {
                            if (Input.GetKeyDown(KeyCode.R))
                            {
                                change_slot = false;
                                change_slot_id = 0;
                                Tips.text = "";
                            }
                            break;
                        }
                }
            }
            if(change_slot==true)
            {
                Quick_slot[change_slot_id].sprite = ability[current_id].skillicon.sprite;
                //change the data in the player
                Tips.text = "";
                change_slot_id = 0;
                change_slot = false;
            }
        }

    }

}
