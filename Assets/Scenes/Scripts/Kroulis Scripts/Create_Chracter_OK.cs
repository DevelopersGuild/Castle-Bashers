using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Create_Chracter_OK : MonoBehaviour {
    public GameObject Input_Text;
    public GameObject Sex_Control;
    string character_name;
    int character_sex=0;
	// Use this for initialization

    void Updata()
    {

    }
	
    public void Click()
    {
        character_name = Input_Text.GetComponent<InputField>().text;
        if(Sex_Control.GetComponent<Toggle>().isOn==true)
        {
            character_sex = 1;
        }
        else
        {
            character_sex = 2;
        }
        if (character_name != "" && character_name != "Please Input Your Name ")
        {
            Debug.Log("Character name:"+character_name);
            Debug.Log("Character sex:" + character_sex.ToString());
            Debug.Log("Todo on click");
        }
        
    }
}
