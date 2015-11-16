using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Other_Windows_FullControl : MonoBehaviour {

    //link Main_Process
    public GameObject Main_Process;
    public GameObject Black;
    public GameObject Death;
    public GameObject Upgrade;
    public GameObject Mission_Success;
    public GameObject Level_Select;
    public GameObject Skill_Shop;
    //Full Scale
    float full_scale;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Black.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        //Adjust
        full_scale = (float)(Screen.width / 1920.00);
        this.GetComponent<CanvasScaler>().scaleFactor = full_scale;
        if(Input.GetKeyDown(KeyCode.F9))
        {
            Globe.Map_Load_id = 1;
            Main_Process.GetComponent<Main_Process>().UI_Mission_Success_Open();
        }
	}
}
