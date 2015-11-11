using UnityEngine;
using System.Collections;

public class Map_Transfer_Process : MonoBehaviour {

    private float fps = 60.0f;
    private float time;
    private int nowFram;
    AsyncOperation async;
    Map_Transfer_UI_Control Map_Transfer_UI_Control_Script;

    void Start()
    {
        Globe.Map_Load_id = 3;
        //link the UI
        GameObject GOResult;
        GOResult = GameObject.Find("TransferUI");
        Map_Transfer_UI_Control_Script = GOResult.GetComponent<Map_Transfer_UI_Control>();
        //start to load scene
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        async = Application.LoadLevelAsync(Globe.Map_Load_id);

        yield return async;

    }
    void Update()
    {
        Map_Transfer_UI_Control_Script.Progress = async.progress;
    }

}
