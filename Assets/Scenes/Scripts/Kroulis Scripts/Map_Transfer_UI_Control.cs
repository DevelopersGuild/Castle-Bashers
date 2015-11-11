using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Map_Transfer_UI_Control : MonoBehaviour {
    
    //Main Control
    float full_scale;
    //UI Control
    RawImage M_T_BG;
    Image M_T_Bar;
    Text M_T_Hint;
    Map_Transfer_DB Map_Transfer_DB_Script;
    //
    public float Progress;
	// Use this for initialization
	void Start () {
        Text[] finds1;
        Image[] finds2;
        RawImage[] finds3;
        GameObject GOResult;
        //Link Database
        GOResult = GameObject.Find("Map_Trans_Database");
        Map_Transfer_DB_Script = GOResult.GetComponent<Map_Transfer_DB>();
        //Link UI
        finds1=this.gameObject.GetComponentsInChildren<Text>();
        foreach(Text t in finds1)
        {
            if(t.name=="M_T_Hint")
            {
                //Debug.Log("Tag Found");
                M_T_Hint = t;
                break;
            }
        }
        finds2 = this.gameObject.GetComponentsInChildren<Image>();
        foreach (Image I in finds2)
        {
            if (I.name == "M_T_BAR")
            {
                //Debug.Log("Bar Found");
                M_T_Bar = I;
                break;
            }
        }
        finds3 = this.gameObject.GetComponentsInChildren<RawImage>();
        foreach (RawImage R in finds3)
        {
            if (R.name == "M_T_BG")
            {
               //Debug.Log("BG Found");
                M_T_BG = R;
                break;
            }
        }
        M_T_BG.texture = Map_Transfer_DB_Script.mapinfo[Globe.Map_Load_id].bg_texture;
        /*if(M_T_BG.texture.GetType().ToString()=="MovieTexture")
        {
             M_T_BG.texture.play();
        }*/
        int randtag;
        randtag = Random.Range(0, Map_Transfer_DB_Script.maptag.Length);
        M_T_Hint.text = Map_Transfer_DB_Script.maptag[randtag].text;

	}
	
	// Update is called once per frame
	void Update () {
        //Scale
        full_scale = (float)(Screen.width / 1920.00);
        this.GetComponent<CanvasScaler>().scaleFactor = full_scale;
        //Control
        M_T_Bar.fillAmount = Progress;
	}
}
