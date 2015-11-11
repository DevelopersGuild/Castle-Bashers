using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Main_Process : MonoBehaviour {

    //UI Objects
    public GameObject Main_UI;
    public GameObject Menu_UI;
    public GameObject Other_Windows;
    //UI Infomations
    public bool Hide_UI;
    public bool Menu_Open;
    public int Menu_id;
    public bool One_player_per_client;
    public bool In_Battle;
    public bool Killing_boss;
    public bool Team_Mode;
    public bool esckey_up; // Avoid key conflict
    AudioSource BGM_Player;
    GameObject Player_GO;
    Health Player_Health;
    Mana Player_Mana;
    Experience Player_EXP;
    Player Player_Script;
    //dlls
    [DllImport("Char_Proc")]
    private static extern bool Is_Character_Created();
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Main_UI.GetComponent<Main_UI_FULLControl>().Main_Process = this.gameObject;
        Menu_UI.GetComponent<Menu_UI_FullControl>().Main_Process = this.gameObject;
        Other_Windows.GetComponent<Other_Windows_FullControl>().Main_Process = this.gameObject;
        BGM_Player = GetComponent<AudioSource>();
        Debug.Log(Is_Character_Created());
        Player_GO = GameObject.Find("Player");
        Player_Health = Player_GO.GetComponent<Health>();
        Player_Mana = Player_GO.GetComponent<Mana>();
        Player_EXP = Player_GO.GetComponent<Experience>();
        Player_Script = Player_GO.GetComponent<Player>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Hide_UI == true)
        {
            Main_UI.SetActive(false);
            Menu_UI.SetActive(false);
        }
        else
        {
            Main_UI.GetComponent<Main_UI_FULLControl>().Menu_open = Menu_Open;
            Main_UI.GetComponent<Main_UI_FULLControl>().Killing_boss = Killing_boss;
            Main_UI.GetComponent<Main_UI_FULLControl>().One_player_per_client = One_player_per_client;
            Main_UI.GetComponent<Main_UI_FULLControl>().In_Battle = In_Battle;
            Main_UI.GetComponent<Main_UI_FULLControl>().Team_mode = Team_Mode;
            Menu_UI.GetComponent<Menu_UI_FullControl>().Menu_open = Menu_Open;
            Menu_UI.GetComponent<Menu_UI_FullControl>().Menu_id = Menu_id;
            Main_UI.SetActive(true);
            Menu_UI.SetActive(true);
            if(Menu_Open==false)
            {
                if(One_player_per_client==true)
                {
                    Main_UI.GetComponent<Main_UI_FULLControl>().maxhp = (int)Player_Health.GetMaxHP();
                    Main_UI.GetComponent<Main_UI_FULLControl>().hp = (int)Player_Health.GetCurrentHealth();
                    Main_UI.GetComponent<Main_UI_FULLControl>().maxmp = (int)Player_Mana.GetMaxMana();
                    Main_UI.GetComponent<Main_UI_FULLControl>().mp = (int)Player_Mana.GetMana();
                    Main_UI.GetComponent<Main_UI_FULLControl>().exp = Player_EXP.GetExperience();
                    Main_UI.GetComponent<Main_UI_FULLControl>().nexp = Player_EXP.GetNEXP();
                    Main_UI.GetComponent<Main_UI_FULLControl>().lv = Player_EXP.GetCurrentLevel();
                    Main_UI.GetComponent<Main_UI_FULLControl>().cid = Player_Script.GetClassID();
                }
                else
                {

                }
            }
        }
        
        
    }

    //for Init the main UI (only use one time at the begining of the game)
    public void Main_UI_Init(bool two_player)
    {
        Team_Mode=false;
        Menu_Open = false;
        Killing_boss = false;
        In_Battle = false;
        Hide_UI = false;
        if(two_player == false)
        {
            One_player_per_client = true;
        }
    }

    //for forcing open Menu (Do not use it when other windows UI is opening)
    public void Menu_Force_Open(int menu_id)
    {
        Menu_id = menu_id;
        Menu_Open = true;
        Hide_UI = false;
    }

    //when the mission start, use this to start the timer and ban the menu
    public void mission_start()
    {
        //In_Battle = true;
        Start_Battle();
        GetComponentInChildren<Mission_Database>().clear_db();
        GetComponentInChildren<Mission_Timer>().Clear_Timer();
        GetComponentInChildren<Mission_Timer>().Start_Timer();
    }

    //for opening Other Windows -> UI
    public void UI_Level_Selector_Open(int chapter_id)
    {
        Hide_UI = true;
        Other_Windows.SetActive(true);
        GameObject LS = Other_Windows.GetComponent<Other_Windows_FullControl>().Level_Select;
        LS.GetComponent<Level_Select_FullControl>().chapid = chapter_id;
        LS.GetComponent<Level_Select_FullControl>().currentmap = 0;
        LS.GetComponent<Level_Select_FullControl>().currentdiff = 0;
        LS.SetActive(true);
    }

    public void UI_Death_Window_Open_Withmusic()
    {
        Hide_UI = true;
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
        GameObject Death = Other_Windows.GetComponent<Other_Windows_FullControl>().Death;
        Death.SetActive(true);
        Death.GetComponent<UI_Death_MusicControl>().BGM_Start=true;
    }

    public void UI_SkillShop_Open(int class_id, int[] skill_id)
    {
        Hide_UI = true;
        Other_Windows.SetActive(true);
        GameObject Shop = Other_Windows.GetComponent<Other_Windows_FullControl>().Skill_Shop;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().shop_class_id = class_id;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().store_skill_id = skill_id;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().Change();
        Shop.SetActive(true);
    }

    public void UI_Upgrade_Window_Open(int equipment_type)
    {

    }

    public void UI_Mission_Success_Open()
    {
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
        GameObject MS= Other_Windows.GetComponent<Other_Windows_FullControl>().Mission_Success;
        GetComponentInChildren<Mission_Timer>().Stop_Timer();
        //MS.GetComponent<M_S_CalculateRank>().Calculate();
        MS.SetActive(true);

    }

    public void OtherWindows_Close()
    {
        Other_Windows.SetActive(false);
        Hide_UI = false;
    }

    //Battle Control

    public void Start_Battle()
    {
        In_Battle = true;
        BGM_Player.clip = GetComponentInChildren<BGM_Manage>().BGM[Random.Range(1, 2)].aud;
        BGM_Player.Play();
    }

    public void Srart_Battle_Boss(int boss_id,int boss_headicon_id)
    {
        Killing_boss = true;
        Main_UI.GetComponent<Main_UI_FULLControl>().boss.id = boss_id;
        Main_UI.GetComponent<Main_UI_FULLControl>().boss.headiconid = boss_headicon_id;
        Main_UI.GetComponent<Main_UI_FULLControl>().Update_Boss_Info();
        
        while(BGM_Player.volume>0)
        {
            BGM_Player.volume = BGM_Player.volume - 0.05F * Time.deltaTime;
        }
        BGM_Player.volume = 0;
        BGM_Player.clip = GetComponentInChildren<BGM_Manage>().BGM[0].aud;
        BGM_Player.Play();
        BGM_Player.volume = 1;
    }
	
    public void SwitchBackToNormalBattle()
    {
        Killing_boss = false;
        while (BGM_Player.volume > 0)
        {
            BGM_Player.volume = BGM_Player.volume - 0.05F * Time.deltaTime;
        }
        BGM_Player.volume = 0;
        BGM_Player.clip = GetComponentInChildren<BGM_Manage>().BGM[Random.Range(1,2)].aud;
        BGM_Player.Play();
        while (BGM_Player.volume < 1)
        {
            BGM_Player.volume = BGM_Player.volume + 0.05F * Time.deltaTime;
        }
        BGM_Player.volume = 1;
    }

    public void End_Battle()
    {
        In_Battle = false;
        while (BGM_Player.volume > 0)
        {
            BGM_Player.volume = BGM_Player.volume - 0.05F * Time.deltaTime;
        }
        BGM_Player.Stop();
        BGM_Player.clip = null;
        BGM_Player.volume = 1;
    }

    //Start Play a new Background music. Let BGM_id be -1 will just stop play the music.
    public void Start_new_BGM(int BGM_id,bool SmoothlyDown,bool SmoothlyUp)  
    {
        if(BGM_id==-1)
        {
            BGM_Player.clip = null;
        }
        else if (BGM_id > GetComponentInChildren<BGM_Manage>().BGM.Length)
        {
            Debug.LogWarning("The BGM_id You Input is not correct. Please checkout what happened.");
            BGM_Player.clip = null;
        }
        else
        {
            if(SmoothlyDown==true)
            {
                while (BGM_Player.volume > 0)
                {
                    BGM_Player.volume = BGM_Player.volume - 0.05F * Time.deltaTime;
                }
                BGM_Player.volume = 0;
            }

            BGM_Player.clip = GetComponentInChildren<BGM_Manage>().BGM[BGM_id].aud;
            BGM_Player.Play();
            if(SmoothlyUp==true)
            {
                while (BGM_Player.volume < 1)
                {
                    BGM_Player.volume = BGM_Player.volume + 0.05F * Time.deltaTime;
                }
            }
            BGM_Player.volume = 1;
        }
        
    }
}
