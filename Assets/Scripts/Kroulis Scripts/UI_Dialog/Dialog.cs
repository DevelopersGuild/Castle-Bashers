using UnityEngine;
using System.Collections;
using System.Xml;
using Kroulis.Verify;
using System.Collections.Generic;
using System.IO;

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
        public string id;
        public string comment;
        public List<StepDialog> dialog;
    }

    public class DialogWriter
    {
        public static int WRITESUCCESS = 0;
        public static int IDNOTEXIST = 1;
        public static int IDNOTAVAILBLE = 1;
        public static int WRITEFAILED = 2;
        XmlDocument file = null;
        XmlNode dialog_node = null;
        public DialogWriter()
        {
            file = new XmlDocument();
            Object obj = Resources.Load("dialog");
            //Debug.Log(obj.ToString());
            if (obj)
                file.LoadXml(obj.ToString());
            dialog_node = file.SelectSingleNode("dialog");
        }

        private bool CheckIDAvailble(string id)
        {
            XmlNodeList dl = dialog_node.ChildNodes;
            foreach(XmlElement di in dl)
            {
                if (di.GetAttribute("id") == id)
                    return false;
            }
            return true;
        }
        public int AddToXML(ADialog _dialog)
        {
            if(file == null)
                return WRITEFAILED;
            if (!CheckIDAvailble(_dialog.id))
                return IDNOTAVAILBLE;
            //root of the data
            XmlElement data = file.CreateElement("data");
            data.SetAttribute("id",_dialog.id);
            data.SetAttribute("comment", _dialog.comment);
            dialog_node.AppendChild(data);
            foreach(StepDialog sd in _dialog.dialog)
            {
                XmlElement stepdialog = file.CreateElement("text");
                stepdialog.SetAttribute("order",sd.order);
                stepdialog.SetAttribute("speaker", sd.speaker);
                stepdialog.SetAttribute("aud", sd.audio);
                stepdialog.InnerText = sd.text;
                data.AppendChild(stepdialog);
            }
            file.Save("Assets\\Resources\\dialog.xml");
            if (File.Exists("Assets\\Resources\\dialog.xml.meta"))
                File.Delete("Assets\\Resources\\dialog.xml.meta");
            return WRITESUCCESS;
        }

        public int RemoveFromXML(string id)
        {
            if (file == null)
                return WRITEFAILED;
            XmlNodeList dl = dialog_node.ChildNodes;
            foreach (XmlElement di in dl)
            {
                if (di.GetAttribute("id") == id)
                {
                    dialog_node.RemoveChild(di);
                    file.Save("Assets\\Resources\\dialog.xml");
                    if (File.Exists("Assets\\Resources\\dialog.xml.meta"))
                        File.Delete("Assets\\Resources\\dialog.xml.meta");
                    return WRITESUCCESS;
                }
                    
            }
            return IDNOTEXIST;
        }

    }

    public struct DialogShortcut
    {
        public string id;
        public string comment;
        public string firsttext;
    }

    public class DialogReader
    {
        XmlDocument file = null;
        XmlNode dialog_node = null;
        public DialogReader()
        {
            file = new XmlDocument();
            Object obj = Resources.Load("dialog");
            //Debug.Log(obj.ToString());
            if (obj)
                file.LoadXml(obj.ToString());
            dialog_node = file.SelectSingleNode("dialog");
        }

        public void RefreshXML()
        {
            file = new XmlDocument();
            Object obj = Resources.Load("dialog");
            //Debug.Log(obj.ToString());
            if (obj)
                file.LoadXml(obj.ToString());
            dialog_node = file.SelectSingleNode("dialog");
        }

        public List<DialogShortcut> GetDialogsShortcut()
        {
            List<DialogShortcut> list = new List<DialogShortcut>();
            XmlNodeList dialogs = dialog_node.ChildNodes;
            foreach(XmlElement di in dialogs)
            {
                DialogShortcut newds = new DialogShortcut();
                newds.id = di.GetAttribute("id");
                newds.comment = di.GetAttribute("comment");
                newds.firsttext = di.ChildNodes[0].InnerText;
                list.Add(newds);
            }
            list.Sort((a, b) =>
                {
                    return string.Compare(a.id, b.id);
                });
            return list;
        }

        public ADialog GetDialog(string id)
        {
            XmlNodeList dialogs = dialog_node.ChildNodes;
            ADialog result = new ADialog();
            result.dialog = new List<StepDialog>();
            foreach (XmlElement di in dialogs)
            {
                if(di.GetAttribute("id")==id)
                {
                    result.id = id;
                    result.comment = di.GetAttribute("comment");
                    XmlNodeList steps = di.ChildNodes;
                    foreach(XmlElement sd in steps)
                    {
                        StepDialog stepdialog = new StepDialog();
                        stepdialog.order = sd.GetAttribute("order");
                        stepdialog.speaker = sd.GetAttribute("speaker");
                        stepdialog.audio = sd.GetAttribute("aud");
                        stepdialog.text = sd.InnerText;
                        result.dialog.Add(stepdialog);
                    }
                    result.dialog.Sort((a, b) =>
                    {
                        if (int.Parse(a.order) > int.Parse(b.order))
                            return 1;
                        if (int.Parse(a.order) == int.Parse(b.order))
                            return 0;
                        return -1;
                    });
                    return result;
                }
            }
            result.id = "null";
            return result;
        }


    }

    [System.Serializable]
    public struct NPCDialog
    {
        public string id;
        public DialogRequirement requirement;
        public DialogAction action;
    }

    public class DialogRequirement : MonoBehaviour
    {
        public virtual bool MeetRequirement()
        {
            return true;
        }
        // Return a int that is the difference of your requirement value with the character's already have value. Negative meens didn't reach the requirement.
        public virtual int MeetRequirementI() 
        {
            return 0;
        }
    }

    public class DialogAction : MonoBehaviour
    {
        public virtual void Action()
        {

        }
    }
}

