using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class M_S_CalculateRank : MonoBehaviour {
    
    //Control
    public int stop_time, limit_time, death, limit_death, get_gold, limit_gold;

    //Link UI
    Image Rank_Control_I;
    M_S_Rank_ImageLibrary Rank_Control_L;
    Text M_S_Time, M_S_Death, M_S_GetGold, M_S_RDEXP, M_S_RDGold;
    Mission_Timer M_Timer;
    Mission_Database M_DB;
    Map_Transfer_DB M_T_DB;
    Main_Process mps;

	// Use this for initialization
	void Start () {
        //link link!!
        Image[] Rank = GetComponentsInChildren<Image>();
        foreach(Image I in Rank)
        {
            if(I.name=="Rank_Control")
            {
                Rank_Control_I = I;
                break;
            }
        }
        Rank_Control_L = GetComponentInChildren<M_S_Rank_ImageLibrary>();
        Text[] textinfo;
        textinfo = GetComponentsInChildren<Text>();
        foreach(Text t in textinfo)
        {
            if(t.name=="M_S_Time")
            {
                M_S_Time = t;
                continue;
            }
            if (t.name == "M_S_Death")
            {
                M_S_Death = t;
                continue;
            }
            if (t.name == "M_S_GetGold")
            {
                M_S_GetGold = t;
                continue;
            }
            if (t.name == "M_S_RDEXP")
            {
                M_S_RDEXP = t;
                continue;
            }
            if (t.name == "M_S_RDGold")
            {
                M_S_RDGold = t;
                continue;
            }
        }
        M_Timer = GetComponentInParent<Other_Windows_FullControl>().Main_Process.GetComponentInChildren<Mission_Timer>();
        M_T_DB = GetComponentInParent<Other_Windows_FullControl>().Main_Process.GetComponentInChildren<Map_Transfer_DB>();
        M_DB = GetComponentInParent<Other_Windows_FullControl>().Main_Process.GetComponentInChildren<Mission_Database>();
        gameObject.SetActive(false);
	}
	
    public void Calculate(int player_mode)
    {
        stop_time = M_Timer.current_time;
        death = M_DB.death;
        get_gold = M_DB.get_gold;
        limit_time = M_T_DB.mapinfo[Globe.Map_Load_id].limit_time;
        limit_death = M_T_DB.mapinfo[Globe.Map_Load_id].limit_death;
        limit_gold = M_T_DB.mapinfo[Globe.Map_Load_id].limit_gold;
        if(player_mode==2)
        {
            limit_time = (int)(limit_time * 0.75);
            limit_death = (int)(limit_death * 1.5);
            limit_gold = (int)(limit_gold * 1.5);
        }
        int onlimit = 0;
        if(stop_time>limit_time)
        {
            onlimit++;
            M_S_Time.text = "<color=#ff0000ff>" + (stop_time / 60).ToString("D") + ":" + (stop_time % 60).ToString("D2") + "/" + (limit_time / 60).ToString("D") + ":" + (limit_time % 60).ToString("D2") + "</color>";
        }
        else
        {
            M_S_Time.text = "<color=#00ff00ff>" + (stop_time / 60).ToString("D") + ":" + (stop_time % 60).ToString("D2") + "/" + (limit_time / 60).ToString("D") + ":" + (limit_time % 60).ToString("D2") + "</color>";
        }
        if(death>limit_death)
        {
            M_S_Death.text = "<color=#ff0000ff>" + death.ToString() + "</color>";
            onlimit++;
        }
        else
        {
            M_S_Death.text = "<color=#00ff00ff>" + death.ToString() + "</color>";
        }
        if(get_gold<limit_gold)
        {
            M_S_GetGold.text ="<color=#ff0000ff>" + get_gold.ToString() + "</color>";
            onlimit++;
        }
        else
        {
            M_S_GetGold.text = "<color=#00ff00ff>" + get_gold.ToString() + "</color>";
        }
        Rank_Control_I.sprite = Rank_Control_L.Rank_Image[onlimit];
        M_S_RDEXP.text = M_T_DB.mapinfo[Globe.Map_Load_id].Reward_EXP[onlimit].ToString();
        mps = GetComponentInParent<Other_Windows_FullControl>().Main_Process.GetComponent<Main_Process>();
        //Add Exp
        if (player_mode == 1)
            mps.GetPlayerExperience().AddExperince(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_EXP[onlimit]);
        else
        {
            mps.GetPlayerExperience(0).AddExperince(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_EXP[onlimit]);
            mps.GetPlayerExperience(1).AddExperince(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_EXP[onlimit]);
        }
        M_S_RDGold.text = M_T_DB.mapinfo[Globe.Map_Load_id].Reward_Gold[onlimit].ToString();
        //Add Gold
        if (player_mode == 1)
            mps.GetPlayerCoinManager().addCoins(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_Gold[onlimit]);
        else
        {
            mps.GetPlayerCoinManager(0).addCoins(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_Gold[onlimit]);
            mps.GetPlayerCoinManager(1).addCoins(M_T_DB.mapinfo[Globe.Map_Load_id].Reward_Gold[onlimit]);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Globe.Map_Load_id = 3;
            this.gameObject.SetActive(false);
            mps.OtherWindows_Close();
            mps.End_Battle();
            Application.LoadLevel(2);
            mps.Start_new_BGM(3,false,true);
        }
    }
}
