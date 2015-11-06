using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SaveAndLoad : MonoBehaviour {
    XmlDocument character_data=new XmlDocument();
    string path = "";
	// Use this for initialization
	void Start () {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
            Globe.Character_id = "GM000001";
            Globe.Character_Data_File = "GM000001.xml";

        }

        //character_data.Load(path + "/" + Globe.Character_Data_File);
        //character_data.Save();
	}
	
	public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
