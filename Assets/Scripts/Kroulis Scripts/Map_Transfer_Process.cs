using UnityEngine;
using System.Collections;

public class Map_Transfer_Process : MonoBehaviour {

    private float fps = 60.0f;
    private float time;
    private int nowFram;
    AsyncOperation async;
    Map_Transfer_UI_Control Map_Transfer_UI_Control_Script;
    private Map_Transfer_DB mapdb;
    private AudioSource audio;
    private Main_Process mp;

    void Start()
    {
        //Globe.Map_Load_id = 1;
        //link the UI
        GameObject GOResult;
        GOResult = GameObject.Find("TransferUI");
        Map_Transfer_UI_Control_Script = GOResult.GetComponent<Map_Transfer_UI_Control>();
        GOResult = GameObject.Find("Main Process");
        audio = GOResult.GetComponent<AudioSource>();
        mapdb=GOResult.GetComponentInChildren<Map_Transfer_DB>();
        mp = GOResult.GetComponent<Main_Process>();
        if(GOResult)
        {
            GOResult.GetComponent<SaveAndLoad>().SaveData();
            //play the bgm
            if (mapdb.mapinfo[Globe.Map_Load_id].have_bgm)
            {
                audio.clip = mapdb.mapinfo[Globe.Map_Load_id].bgm;
                audio.Play();
                audio.volume = 1;
            }
            else
            {
                mp.Start_Battle();
            }
                
        }
            
        //start to load scene
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        async = Application.LoadLevelAsync(Globe.Map_Load_id);
        //Debug.Log(Globe.Map_Load_id);
        yield return async;

    }
    void Update()
    {
        Map_Transfer_UI_Control_Script.Progress = async.progress;
    }

}
