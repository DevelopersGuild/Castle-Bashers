using UnityEngine;
using System.Collections;
using Kroulis.Error;
using Kroulis.Verify;

public class Setting_FullControl : MonoBehaviour {

    private Main_Process MC;
    void Start()
    {
        MC = GameObject.Find("Main Process").GetComponent<Main_Process>();
    }
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.F10))
        {
            ErrorCatching.WriteBugXML();
        }
        else if(Input.GetKeyDown(KeyCode.F11))
        {
            if(Globe.Map_Load_id!=3) //in battle 
            {
                Globe.Map_Load_id = 3;
                MC.CancelLevel();
                Application.LoadLevel(2);
            }
            else//quit game
            {
                MC.GetComponent<SaveAndLoad>().SaveData();
                Application.Quit();
            }
        }
	}
}
