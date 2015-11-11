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
        Image[] Result;
        Result = GetComponentsInChildren<Image>();
        foreach(Image i in Result)
        {
            if(i.name=="Out_Black")
            {
                Black = i.gameObject;
                Black.SetActive(false);
                continue;
            }
            if(i.name=="Death")
            {
                Death = i.gameObject;
                Death.SetActive(false);
                continue;
            }
            if (i.name == "Upgrade")
            {
                Upgrade = i.gameObject;
                Upgrade.SetActive(false);
                continue;
            }
            if(i.name=="Mission_Success")
            {
                Mission_Success = i.gameObject;
                Mission_Success.SetActive(false);
                continue;
            }
            if(i.name=="Level_Select")
            {
                Level_Select = i.gameObject;
                Level_Select.SetActive(false);
                continue;
            }
            if(i.name=="Skill_Shop")
            {
                Skill_Shop = i.gameObject;
                Skill_Shop.SetActive(false);
                continue;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Adjust
        full_scale = (float)(Screen.width / 1920.00);
        this.GetComponent<CanvasScaler>().scaleFactor = full_scale;

	}
}
