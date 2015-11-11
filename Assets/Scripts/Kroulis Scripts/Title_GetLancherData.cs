using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;
using Kroulis.Verify;

public class Title_GetLancherData : MonoBehaviour {
    XmlDocument config=new XmlDocument();
    XmlReaderSettings set = new XmlReaderSettings();
    int screen_resolution=0;
    int full_screen=0;
    int refresh_rate=0;

	// Use this for initialization
	void Start () {
        string path = "";
        path = FileVerify.GetPath();
        Debug.Log(path + "/config.xml");
        if (File.Exists(path + "/config.xml"))
        {
            set.IgnoreComments = true;
            config.Load(XmlReader.Create((path + "/config.xml"), set));
            //string configdata;
            //StreamReader r = new StreamReader(path + "/config.xml");
            //configdata = r.ReadToEnd();
            //Debug.Log(configdata);
            //config.LoadXml(configdata);
            XmlNodeList main_nodes = config.SelectSingleNode("config").ChildNodes;
            foreach(XmlElement xl in main_nodes)
            {
                //Debug.Log(xl.Name);
                if(xl.Name=="screen_resolution")
                {
                    screen_resolution = int.Parse(xl.InnerText);
                    continue;
                }
                if(xl.Name=="full_screen")
                {
                    full_screen = int.Parse(xl.InnerText);
                    continue;
                }
                if(xl.Name=="RefreshRate")
                {
                    full_screen = int.Parse(xl.InnerText);
                    continue;
                }
                if(xl.Name=="playerid")
                {
                    Globe.Character_Data_File = xl.InnerText;
                    continue;
                }
                if(xl.Name=="savefile")
                {
                    Globe.Character_Data_File = xl.InnerText;
                }
            }
            if (screen_resolution == 0)
                screen_resolution = 1;

            // 1:1280*720 2:1366*768 3:1920*1080 
            switch(screen_resolution)
            {
                case 1:
                    Screen.SetResolution(1280, 720, Convert.ToBoolean(full_screen));
                    break;
                case 2:
                    Screen.SetResolution(1366, 768, Convert.ToBoolean(full_screen));
                    break;
                case 3:
                    Screen.SetResolution(1920, 1080, Convert.ToBoolean(full_screen));
                    break;
                default:
                    Screen.SetResolution(1280, 720, Convert.ToBoolean(full_screen));
                    break;
            }
 
        }
        else
        {
            Debug.LogWarning("Xml File Not Exist");
            if (Application.isEditor == false)
            {
                //Application.OpenURL("www.kroulisworld.com/");
                Application.Quit();
            }
        }
        
        //if(Debug.isDebugBuild==false)
        //{

        //}
	}
	
}
