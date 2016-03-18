﻿using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Kroulis.Error;

public class Main_Process : MonoBehaviour {

    //Start Up Movie
    public MovieTexture movTexture;
    bool start_to_play = false;
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
    bool had_init = false;
    bool teleport_once = false;
    AudioSource BGM_Player;
    GameObject[] Player_GO=new GameObject[2];
    Health[] Player_Health=new Health[2];
    Mana[] Player_Mana=new Mana[2];
    Experience[] Player_EXP=new Experience[2];
    Player[] Player_Script=new Player[2];
    CoinManager[] Player_Gold = new CoinManager[2];
    PassiveSkillManager[] Player_PSM = new PassiveSkillManager[2];
    GemManager[] Player_GEMM = new GemManager[2];
    //ErrorCatch
    private ErrorCatching error = new ErrorCatching();
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        movTexture.loop = false;
        if(Application.platform!=RuntimePlatform.WindowsEditor)
            movTexture.Play();
        start_to_play = true;
        Main_UI = FindObjectOfType<Main_UI_FULLControl>().gameObject;
        Menu_UI = FindObjectOfType<Menu_UI_FullControl>().gameObject;
        Other_Windows = FindObjectOfType<Other_Windows_FullControl>().gameObject;
        Main_UI.GetComponent<Main_UI_FULLControl>().Main_Process = this.gameObject;
        Menu_UI.GetComponent<Menu_UI_FullControl>().Main_Process = this.gameObject;
        Other_Windows.GetComponent<Other_Windows_FullControl>().Main_Process = this.gameObject;
        BGM_Player = GetComponent<AudioSource>();
        GameObject Result=GameObject.Find("PlayerHolder");
        Player[] playerlist=Result.GetComponentsInChildren<Player>();
        int id=0;
        foreach(Player pl in playerlist)
        {
            Player_GO[id] = pl.gameObject;
            Player_Health[id] = pl.gameObject.GetComponent<Health>();
            Player_Mana[id] = pl.gameObject.GetComponent<Mana>();
            Player_EXP[id] = pl.gameObject.GetComponent<Experience>();
            Player_Gold[id] = pl.gameObject.GetComponent<CoinManager>();
            Player_PSM[id] = pl.gameObject.GetComponent<PassiveSkillManager>();
            Player_GEMM[id] = pl.gameObject.GetComponent<GemManager>();
            Player_Script[id] = pl;
            id++;
        }
        //if (id == 2)
        //    One_player_per_client = false;
        //else
        //    One_player_per_client = true;

        //Start to setup and init Main_UI and Menu_UI
        Invoke("Main_UI_Init",2.00f);
        
        if(Application.platform!=RuntimePlatform.WindowsEditor)
            error.OnEnable();
	}

    // Update is called once per frame
    void Update()
    {
        if (had_init)
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
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxhp[0] = (int)Player_Health[0].GetMaxHP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().hp[0] = (int)Player_Health[0].GetCurrentHealth();
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxmp[0] = (int)Player_Mana[0].GetMaxMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().mp[0] = (int)Player_Mana[0].GetMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().exp[0] = Player_EXP[0].GetExperience();
                        Main_UI.GetComponent<Main_UI_FULLControl>().nexp[0] = Player_EXP[0].GetNEXP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().lv[0] = Player_EXP[0].GetCurrentLevel();
                        Main_UI.GetComponent<Main_UI_FULLControl>().cid[0] = Player_Script[0].GetClassID();
                    }
                    else
                    {
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxhp[0] = (int)Player_Health[0].GetMaxHP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().hp[0] = (int)Player_Health[0].GetCurrentHealth();
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxmp[0] = (int)Player_Mana[0].GetMaxMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().mp[0] = (int)Player_Mana[0].GetMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().exp[0] = Player_EXP[0].GetExperience();
                        Main_UI.GetComponent<Main_UI_FULLControl>().nexp[0] = Player_EXP[0].GetNEXP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().lv[0] = Player_EXP[0].GetCurrentLevel();
                        Main_UI.GetComponent<Main_UI_FULLControl>().cid[0] = Player_Script[0].GetClassID();
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxhp[1] = (int)Player_Health[1].GetMaxHP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().hp[1] = (int)Player_Health[1].GetCurrentHealth();
                        Main_UI.GetComponent<Main_UI_FULLControl>().maxmp[1] = (int)Player_Mana[1].GetMaxMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().mp[1] = (int)Player_Mana[1].GetMana();
                        Main_UI.GetComponent<Main_UI_FULLControl>().exp[1] = Player_EXP[1].GetExperience();
                        Main_UI.GetComponent<Main_UI_FULLControl>().nexp[1] = Player_EXP[1].GetNEXP();
                        Main_UI.GetComponent<Main_UI_FULLControl>().lv[1] = Player_EXP[1].GetCurrentLevel();
                        Main_UI.GetComponent<Main_UI_FULLControl>().cid[1] = Player_Script[1].GetClassID();
                    }
                }
            }
        
        
    }

    //for Init the main UI (only use one time at the begining of the game)
    void Main_UI_Init()
    {
        Team_Mode=false;
        Menu_Open = false;
        Killing_boss = false;
        In_Battle = false;
        Hide_UI = false;
        Main_UI.GetComponent<Main_UI_FULLControl>().One_player_per_client = One_player_per_client;
        Main_UI.SetActive(true);
        Menu_UI.SetActive(false);
        Main_UI.GetComponent<Main_UI_FULLControl>().init();
        CancelInvoke();
        Invoke("Menu_UI_Init", 1.00f);

    }

    void Menu_UI_Init()
    {
        Menu_id = 0;
        Menu_Open = true;
        Main_UI.SetActive(false);
        Menu_UI.SetActive(true);
        CancelInvoke();
        Invoke("finish_init", 1.00f);
    }

    void finish_init()
    {
        Menu_Open = false;
        Hide_UI = true;
        had_init = true;
        CancelInvoke();
        if(Application.platform==RuntimePlatform.WindowsEditor)
        {
            Invoke("TEST", 2.00f);
        }
        else
        {
            Time.timeScale = 2.0f;
        }
    }

    //for forcing open Menu (Do not use it when other windows UI is opening)
    public void Menu_Force_Open(int menu_id)
    {

        Main_UI.GetComponent<Main_UI_FULLControl>().Main_UI_StopChangingIcon();
        Menu_id = menu_id;
        Menu_UI.GetComponent<Menu_UI_FullControl>().UpdateGold();
        Menu_Open = true;
        if(menu_id==1)
            Menu_UI.GetComponent<Menu_UI_FullControl>().Character_Menu.GetComponent<Character_Menu_FullControl>().Change();
        if(menu_id==3)
            Menu_UI.GetComponent<Menu_UI_FullControl>().Ability_Menu.GetComponent<Menu_Ability_Fullcontrol>().Change();
        deinput();
        Hide_UI = false;
    }

    public void Menu_Normal_Close()
    {
        Main_UI.GetComponent<Main_UI_FULLControl>().Main_UI_StartChangingIcon();
        eninput();
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
        deinput();
        Hide_UI = true;
        Other_Windows.SetActive(true);
        GameObject LS = Other_Windows.GetComponent<Other_Windows_FullControl>().Level_Select;
        LS.GetComponent<Level_Select_FullControl>().ShowMap(chapter_id);
    }

    public void UI_Death_Window_Open_Withmusic()
    {
        deinput();
        Hide_UI = true;
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
        GameObject Death = Other_Windows.GetComponent<Other_Windows_FullControl>().Death;
        Death.SetActive(true);
        Death.GetComponent<UI_Death_MusicControl>().BGM_Start=true;
    }

    public void UI_SkillShop_Open(int class_id, int[] skill_id)
    {

        Debug.LogWarning("This Function is out of date. Please use UI_SkillShop_Open(int class_id) as new method.");
        Hide_UI = true;
        Other_Windows.SetActive(true);
        GameObject Shop = Other_Windows.GetComponent<Other_Windows_FullControl>().Skill_Shop;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().shop_class_id = class_id;
        //Shop.GetComponent<Skill_Shop_Fullcontrol>().store_skill_id = skill_id;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().Change();
        Shop.SetActive(true);
    }

    public void UI_SkillShop_Open(int player_id)
    {
        deinput();
        Hide_UI = true;
        Other_Windows.SetActive(true);
        GameObject Shop = Other_Windows.GetComponent<Other_Windows_FullControl>().Skill_Shop;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().shop_class_id = 0;
        Shop.GetComponent<Skill_Shop_Fullcontrol>().Change(player_id);
        Shop.SetActive(true);
    }

    public void UI_Upgrade_Window_Open(int equipment_type)
    {
        deinput();
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Upgrade.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Upgrade.GetComponent<Equipment_Upgrade_FullControl>().Change(equipment_type);
        
    }

    public void UI_Mission_Success_Open()
    {
        deinput();
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Black.SetActive(true);
        GameObject MS= Other_Windows.GetComponent<Other_Windows_FullControl>().Mission_Success;
        GetComponentInChildren<Mission_Timer>().Stop_Timer();
        MS.GetComponent<M_S_CalculateRank>().Calculate(One_player_per_client?1:2);
        MS.SetActive(true);

    }

    public void UI_Passive_Skill_Panel_Open(int ? playerid)
    {
        deinput();
        Hide_UI = true;
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().PassiveSkill.GetComponent<Passive_Skill_Updater_Fullcontrol>().Change(playerid);
        Other_Windows.GetComponent<Other_Windows_FullControl>().PassiveSkill.SetActive(true);
    }

    public void OtherWindows_Close()
    {
        eninput();
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
        BGM_Player.volume = Globe.music_volume;
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
        while (BGM_Player.volume < Globe.music_volume)
        {
            BGM_Player.volume = BGM_Player.volume + 0.05F * Time.deltaTime;
        }
        BGM_Player.volume = Globe.music_volume;
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
        BGM_Player.volume = Globe.music_volume;
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
                while (BGM_Player.volume < Globe.music_volume)
                {
                    BGM_Player.volume = BGM_Player.volume + 0.05F * Time.deltaTime;
                }
            }
            BGM_Player.volume = Globe.music_volume;
        }
        
    }

    public void CancelLevel()
    {
        GetComponentInChildren<Mission_Timer>().Clear_Timer();
        GetComponentInChildren<Mission_Database>().clear_db();
        End_Battle();
    }

    public void ReviveAllPlayers()
    {
        Player_Health[0].PlayerRevive(100);
        if (One_player_per_client == false)
            Player_Health[1].PlayerRevive(100);
    }

    public GameObject GetPlayerObject(int ? id=null)
    {
        if (id == null)
            return Player_GO[0];
        else
        {
            if(id<2 && id>=0)
                return Player_GO[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerObject: id is out of range.");
                return Player_GO[0];
            }
        }
    }

    public Player GetPlayerScript(int ? id=null)
    {
        if (id == null)
            return Player_Script[0];
        else
        {
            if (id < 2 && id >= 0)
                return Player_Script[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerScript: id is out of range.");
                return Player_Script[0];
            }
        }
    }
    public Experience GetPlayerExperience(int ? id=null)
    {
        if (id == null)
            return Player_EXP[0];
        else
        {
            if (id < 2 && id >= 0)
                return Player_EXP[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerExperience: id is out of range.");
                return Player_EXP[0];
            }
        }
    }

    public CoinManager GetPlayerCoinManager(int ? id=null)
    {
        if (id == null)
            return Player_Gold[0];
        else
        {
            if (id < 2 && id >= 0)
                return Player_Gold[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerCoinManager: id is out of range.");
                return Player_Gold[0];
            }
        }
    }

    public PassiveSkillManager GetPlayerPassiveSkillManager(int? id = null)
    {
        if (id == null)
            return Player_PSM[0];
        else
        {
            if (id < 2 && id >= 0)
                return Player_PSM[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerPassiveSkillManager: id is out of range.");
                return Player_PSM[0];
            }
        }
    }

    public GemManager GetPlayerGemManager (int? id = null)
    {
        if (id == null)
            return Player_GEMM[0];
        else
        {
            if (id < 2 && id >= 0)
                return Player_GEMM[(int)id];
            else
            {
                Debug.LogWarning("GetPlayerPassiveSkillManager: id is out of range.");
                return Player_GEMM[0];
            }
        }
    }

    public void OpenDialog(string id,string npcname)
    {
        foreach (Player p in Player_Script)
        {
            p.DisableInput();
        }
        Globe.talking = true;
        Other_Windows.SetActive(true);
        Other_Windows.GetComponent<Other_Windows_FullControl>().Dialog.GetComponent<Dialog_FullControl>().OpenDialog(id,npcname);
    }

    //Boss Data Update
    public void SetBossMaxHP(int maxhp)
    {
        Main_UI.GetComponent<Main_UI_FULLControl>().boss.maxhp = maxhp;
    }

    public void SetBossCurrentHP(int hp)
    {
        Main_UI.GetComponent<Main_UI_FULLControl>().boss.hp = hp;
    }

    private void TEST()
    {
        CancelInvoke();
        //OpenDialog("10000", "test");
        UI_Level_Selector_Open(0);
    }

    public QuestList GetQuestList()
    {
        return GetComponentInChildren<QuestList>();
    }

    void OnGUI()
    {
        if(movTexture)
            if(movTexture.isPlaying)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture, ScaleMode.StretchToFill);
            }
            else if(start_to_play==true && teleport_once == false)
            {
                Time.timeScale=1.0f;
                if(Application.platform!=RuntimePlatform.WindowsEditor)
                {
                    //if (Globe.Map_Load_id != 3)
                    //    Start_Battle();
                    Globe.Map_Load_id = 3;
                    //Application.LoadLevel("_loading");
                    Application.LoadLevel("_loading");
                    OtherWindows_Close();
                    teleport_once = true;
                }
            
            }
    }

    void deinput()
    {
        foreach (Player p in Player_Script)
        {
            if(p!=null)
                p.DisableInput();
        }
    }

    void eninput()
    {
        foreach (Player p in Player_Script)
        {
            if (p != null)
                p.enableInput();
        }
    }
}
