using UnityEngine;
using System.Collections;
using System.Xml;
using Kroulis.Verify;
using System.Collections.Generic;

namespace Kroulis.Dialog
{
    public class NormalDialog
    {
        XmlDocument file = null;
        XmlElement current_dialog=null;
        XmlNodeList dialog_list=null;
        int current_index=0;

        public NormalDialog()
        {

        }

        public NormalDialog(bool autoinit)
        {
            if (autoinit)
                Init();
        }

        public void Init()
        {
            file=new XmlDocument();
            Object obj=Resources.Load("dialog");
            //Debug.Log(obj.ToString());
            if(obj)
                file.LoadXml(obj.ToString());
        }

        public bool SetDialogTo(string id)
        {
            if(file==null)
            {
                Debug.LogError("DialogFileNotExistException: The Dialog file not exist!");
                return false;
            }
            XmlNodeList list = file.SelectSingleNode("dialog").ChildNodes;
            foreach(XmlElement xe in list)
            {
                if(xe.GetAttribute("id")==id)
                {
                    current_dialog = xe;
                    dialog_list = xe.ChildNodes;
                    current_index = 1;
                    return true;
                }
            }
            Debug.LogError("DialogNotExistException: The Dialog id:" + id + " not exist.");
            return false;
        }

        public bool CheckIDAvailable(string id)
        {
            Debug.Log(id);
            if (file == null)
            {
                Debug.LogError("DialogFileNotExistException: The Dialog file not exist!");
                return false;
            }
            XmlNodeList list = file.SelectSingleNode("dialog").ChildNodes;
            foreach (XmlElement xe in list)
            {
                //Debug.Log("Dialog id:" + xe.GetAttribute("id"));
                if (xe.GetAttribute("id").ToString() == id)
                {
                    return false;
                }
            }
            return true;
        }


        public string GetCurrentSpeaker()
        {
            if (current_dialog.GetAttribute("id") != null)
            {
                foreach(XmlElement xe in dialog_list)
                {
                    if(xe.GetAttribute("order")==current_index.ToString())
                    {
                        return xe.GetAttribute("speaker");
                    }
                }
                return "";
            }
            else
                return "";
        }

        public string GetCurrentDialog()
        {
            if (current_dialog.GetAttribute("id") != null)
            {
                foreach (XmlElement xe in dialog_list)
                {
                    if (xe.GetAttribute("order") == current_index.ToString())
                    {
                        return xe.InnerText;
                    }
                }
                return "";
            }
            else
                return "";
        }

        public string GetCurrentAudio()
        {
            if (current_dialog.GetAttribute("id") != null)
            {
                foreach (XmlElement xe in dialog_list)
                {
                    if (xe.GetAttribute("order") == current_index.ToString())
                    {
                        if(xe.GetAttribute("aduio")!=null)
                        {
                            return xe.GetAttribute("audio");
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                return "";
            }
            else
                return "";
        }

        public bool CouldNext()
        {
            if (current_dialog.GetAttribute("id") != null)
            {
                foreach (XmlElement xe in dialog_list)
                {
                    if (xe.GetAttribute("order") == (current_index+1).ToString())
                    {
                        return true;
                    }
                }
                return false;
            }
            else
                return false;
        }

        public void SetNext()
        {
            if (CouldNext())
                current_index += 1;
        }
    }

    public class ChoosingDialog
    {

    }

    public struct StepDialog
    {
        public string order;
        public string speaker;
        public string text;
        public string audio;
        public void Clear()
        {
            speaker = "";
            text = "";
            audio = "";
            order = "";
        }
    }

    public struct ADialog
    {
        public List<StepDialog> dialog;
        public static bool WriteToXML(ADialog dia)
        {
            return true;
        }
    }
}

