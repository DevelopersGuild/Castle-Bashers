using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu_UI_FullControl : MonoBehaviour {

    //Full Scale
    float full_scale;
    //Control
    GameObject ON_OFF;
    public GameObject Character_Menu;
    public GameObject Bag_Menu;
    public GameObject Ability_Menu;
    public GameObject Setting_Menu;
    //Control API
    public bool Menu_open;
    public int Menu_id;
    public GameObject Main_Process;
    //Gold
    Text GoldAmount;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Text[] finds1;
        //Image[] finds2;
        GameObject GOResult;
        //Link UI
        GOResult = GameObject.Find("Menu_Background");
        finds1=GOResult.GetComponentsInChildren<Text>();
        foreach(Text t in finds1)
        {
            if(t.name=="Gold_Amount")
            {
                GoldAmount = t;
                break;
            }
        }
        ON_OFF = GOResult;

	}

    public void UpdateGold()
    {
        if(Main_Process.GetComponent<Main_Process>().One_player_per_client==true)
        {
            GoldAmount.text = Main_Process.GetComponent<Main_Process>().GetPlayerCoinManager().getCoins().ToString();
        }
        else
        {
            int coinA = Main_Process.GetComponent<Main_Process>().GetPlayerCoinManager(0).getCoins();
            int coinB = Main_Process.GetComponent<Main_Process>().GetPlayerCoinManager(1).getCoins();
            int totalCoin = coinA + coinB;
            GoldAmount.text = totalCoin.ToString() + " <color=#c0c0c0ff>("+coinA.ToString()+"+"+coinB.ToString()+")</color>";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Menu_open==false)
        {
            ON_OFF.SetActive(false);
        }
        else
        {
            full_scale = (float)(Screen.width / 1920.00);
            this.GetComponent<CanvasScaler>().scaleFactor = full_scale;
            ON_OFF.SetActive(true);
            if(Menu_id==1)//character open
            {
                Character_Menu.SetActive(true);
                Bag_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else if(Menu_id==2)//bag open
            {
                Bag_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else if(Menu_id==3)//ability open
            {
                Ability_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Bag_Menu.SetActive(false);
                Setting_Menu.SetActive(false);
            }
            else//setting open
            {
                Setting_Menu.SetActive(true);
                Character_Menu.SetActive(false);
                Bag_Menu.SetActive(false);
                Ability_Menu.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && Main_Process.GetComponent<Main_Process>().esckey_up==false)
        {
            Main_Process.GetComponent<Main_Process>().Menu_Open = false;
        }
        if(Main_Process.GetComponent<Main_Process>().esckey_up==true && Input.GetKeyUp(KeyCode.Escape))
        {
            Main_Process.GetComponent<Main_Process>().esckey_up = false;
        }
        
	}
}
