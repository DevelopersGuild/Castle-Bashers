using UnityEngine;
using System.Collections;

public class UI_Death_MusicControl : MonoBehaviour {
    public bool BGM_Start;
    private AudioSource audio;
    private Main_Process mp;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        mp = GetComponentInParent<Other_Windows_FullControl>().Main_Process.GetComponent<Main_Process>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(BGM_Start==true)
        {
            BGM_Start = false;
            audio.mute = false;
            audio.volume = Globe.music_volume;
            audio.Play();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            mp.ReviveAllPlayers();
            mp.OtherWindows_Close();

        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            mp.ReviveAllPlayers();
            mp.CancelLevel();
            mp.OtherWindows_Close();
            Globe.Map_Load_id = 3;
            Application.LoadLevel(2);
        }
	    
	}
}
