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
    //dlls
    [DllImport("Char_Proc")]
    private static extern bool Is_Character_Created();
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Main_UI.GetComponent<Main_UI_FULLControl>().Main_Process = this.gameObject;
        Menu_UI.GetComponent<Menu_UI_FullControl>().Main_Process = this.gameObject;
        Other_Windows.GetComponent<Other_Windows_FullControl>().Main_Process = this.gameObject;
        Debug.Log(Is_Character_Created());
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
        }
        
        
    }
	
}
