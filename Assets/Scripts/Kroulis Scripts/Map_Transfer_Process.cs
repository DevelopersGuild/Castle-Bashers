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
    static int counter = 0;

    void Start()
    {
        /*if(Application.platform!=RuntimePlatform.WindowsEditor)
        {
            if (Globe.Map_Load_id == 1)
                Application.LoadLevel("mainLevel");
            else
                Application.LoadLevel("mainTown");
            return;
        }*/
        //Globe.Map_Load_id = 1;
        //Debug.Log("Teleport to Level: " + Globe.Map_Load_id);
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
            if(Application.platform!=RuntimePlatform.WindowsEditor)
                if (GOResult.GetComponent<SaveAndLoad>()!=null)
                    GOResult.GetComponent<SaveAndLoad>().SaveData();
            //play the bgm
            if (mapdb.mapinfo[Globe.Map_Load_id].have_bgm && Globe.Map_Load_id!=1)
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
        counter++;
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
        if(counter==0 && async!=null)
            Map_Transfer_UI_Control_Script.Progress = async.progress;
    }

}
