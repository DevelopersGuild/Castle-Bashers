using UnityEngine;
using UnityEditor;
using System.Collections;
using Kroulis.Dialog;
using System.Threading;
using System.Collections.Generic;


public class CreateDialog : EditorWindow
{
    private NormalDialog dial = new NormalDialog(true);
    private DialogWriter dw = new DialogWriter();
    [MenuItem("Tools/Dialog System/Create Dialog")]
    static void ManageDialog () 
    {
        Rect wr = new Rect(0, 0, 500, 600);
        CreateDialog window = (CreateDialog)EditorWindow.GetWindowWithRect(typeof(CreateDialog), wr, true, "Dialog Manager - Create");
        window.Show();
    }

    Object NPC;
    Object AUDIO;
    string dialog_id="";
    string comment = "";
    private int count = 1;
    private int selection = -1;
    private List<StepDialog> stepdialogs = new List<StepDialog>();
    private StepDialog stepdialog = new StepDialog();
    private Vector2 scrollposition = new Vector2();

    void OnGUI()
    {
        //Basic information
        EditorGUILayout.BeginVertical();
            NPC = EditorGUILayout.ObjectField("NPC GameObject", Selection.activeGameObject, typeof(GameObject));
            EditorGUILayout.BeginHorizontal();
            dialog_id=EditorGUILayout.TextField("Dialog ID",dialog_id);
            comment = EditorGUILayout.TextField("Comment", comment);
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        //Check ID Available Button
        if(GUILayout.Button("Check ID"))
        {
            if (dialog_id=="")
            {
                SwNotification("Please input dialog ID first.");
            }
            else if(dial.CheckIDAvailable(dialog_id)==true)
            {
                SwNotification("Dialog ID " + dialog_id + " is available.");
            }
            else
            {
                SwNotification("Dialog ID " + dialog_id + " is not available.");
            }
        }
        //Step Dialog Create Part
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
                //EditorGUILayout.IntField("Step#",count);
                EditorGUILayout.LabelField("Step#  " + count.ToString(),GUILayout.Width(100f));
                stepdialog.speaker=EditorGUILayout.TextField("Speaker:",stepdialog.speaker);
                EditorGUILayout.EndHorizontal();
                stepdialog.text = EditorGUILayout.TextArea(stepdialog.text,GUILayout.Height(200f));
                AUDIO = EditorGUILayout.ObjectField("Audio", AUDIO, typeof(AudioClip));
            EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
            //Button Add to dialog list
            if (GUILayout.Button("ADD TO DIALOG LIST"))
            {
                stepdialog.order = count.ToString();
                if(AUDIO!=null)
                {
                    stepdialog.audio = AUDIO.name;
                }
                else
                {
                    stepdialog.audio = "";
                }
                stepdialogs.Add(stepdialog);
                stepdialog.Clear();
                count++;
            }
            //Button Clear
            if(GUILayout.Button("Clear"))
            {
                stepdialog.Clear();
            }
        EditorGUILayout.EndHorizontal();
        //List Showing Part
        EditorGUILayout.LabelField("There are "+stepdialogs.Count.ToString()+" step dialogs in this dialog.");
        scrollposition= EditorGUILayout.BeginScrollView(scrollposition,GUILayout.Height(200f));
            EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("CB", GUILayout.Width(20f));
                    EditorGUILayout.LabelField("#", GUILayout.Width(20f));
                    EditorGUILayout.LabelField("Speaker",GUILayout.Width(60f));
                    EditorGUILayout.LabelField("Text");
                EditorGUILayout.EndHorizontal();
                if(stepdialogs.Count>=1)
                    for (int i = 0; i < stepdialogs.Count;i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                            SetSelection(i, EditorGUILayout.Toggle(GetSelection(i),GUILayout.Width(20f)));
                            EditorGUILayout.LabelField(stepdialogs[i].order, GUILayout.Width(20f));
                            EditorGUILayout.LabelField(stepdialogs[i].speaker,GUILayout.Width(60f));
                            EditorGUILayout.LabelField(stepdialogs[i].text);
                        EditorGUILayout.EndHorizontal();
                    }
                else
                {

                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Toggle(false, GUILayout.Width(20f));
                        EditorGUILayout.LabelField("0", GUILayout.Width(20f));
                        EditorGUILayout.LabelField("No Step Dialogs. Please press ADD TO DIALOG LIST to add new step dialog.");
                    EditorGUILayout.EndHorizontal();
                }
            EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal();
            //Button Delete
            if(GUILayout.Button("Delete"))
            {
                if(selection==-1)
                {
                    SwNotification("No step dialog is selected.");
                }
                else
                {
                    DeleteProcess();
                    stepdialogs.Remove(stepdialogs[stepdialogs.Count-1]);
                    selection = -1;
                    count--;
                    ListSort();
                    SwNotification("Successfully deleted.");
                
                }
            }
            //Button Move Up
            if(GUILayout.Button("Move Up"))
            {
                if(stepdialogs[selection].order!="1")
                {
                    StepDialog source = stepdialogs[selection];
                    StepDialog upone = stepdialogs[selection-1];
                    selection = -1;
                    stepdialogs.Remove(source);
                    stepdialogs.Remove(upone);
                    source.order = (int.Parse(source.order) - 1).ToString();
                    upone.order = (int.Parse(upone.order) + 1).ToString();
                    stepdialogs.Add(source);
                    stepdialogs.Add(upone);
                    ListSort();
                    SwNotification("Move Up Success.");

                }
                else
                {
                    SwNotification("Cannot Move Up Because is Already At Top.");
                }
            
            }
            //Button Move Down
            if(GUILayout.Button("Move down"))
            {
                if (stepdialogs[selection].order != stepdialogs.Count.ToString())
                {
                    StepDialog source = stepdialogs[selection];
                    StepDialog downone = stepdialogs[selection + 1];
                    selection = -1;
                    stepdialogs.Remove(source);
                    stepdialogs.Remove(downone);
                    source.order = (int.Parse(source.order) + 1).ToString();
                    downone.order = (int.Parse(downone.order) - 1).ToString();
                    stepdialogs.Add(source);
                    stepdialogs.Add(downone);
                    ListSort();
                    SwNotification("Move Down Success.");

                }
                else
                {
                    SwNotification("Cannot Move Down Because is Already At Tail.");
                }
            }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Apply (Also add To NPC)"))
            {
                if(dialog_id=="")
                {
                    SwNotification("Dialog ID Missing.");
                }
                else if(dial.CheckIDAvailable(dialog_id)==false)
                {
                    SwNotification("Dialog ID Not Available.");
                }
                else if(NPC==null)
                {
                    SwNotification("NPC GameObject Missing.");
                }
                else
                {
                    Apply();
                    AddToNPC();
                }
            }
            if (GUILayout.Button("Apply (Only Add to xml)"))
            {
                if (dialog_id == "")
                {
                    SwNotification("Dialog ID Missing.");
                }
                else if (dial.CheckIDAvailable(dialog_id) == false)
                {
                    SwNotification("Dialog ID Not Available.");
                }
                else
                {
                    Apply();
                }
            }
            if (GUILayout.Button("Clear All"))
            {
                dialog_id = "";
                comment = "";
                stepdialogs = new List<StepDialog>();
                count = 1;
                selection = -1;
                SwNotification("Clear Success.");
            }
        EditorGUILayout.EndHorizontal();
        //Button Clost the window.
        if(GUILayout.Button("Close the window"))
        {
            this.Close();
        }
    }

    void RmNotification()
    {
        Thread.Sleep(2000);
        this.RemoveNotification();
    }

    void SwNotification(string content)
    {
        ShowNotification(new GUIContent(content));
        Thread newThread = new Thread(new ThreadStart(RmNotification));
        newThread.Start();
    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }

    void SetSelection(int order,bool value)
    {
        if (value == true)
            selection = order;
        if (!value && selection == order)
            selection = -1;
    }

    bool GetSelection(int order)
    {
        return selection == order;
    }

    void ListSort()
    {
        stepdialogs.Sort((a, b) =>
        {
            if (int.Parse(a.order) > int.Parse(b.order))
                return 1;
            if (int.Parse(a.order) == int.Parse(b.order))
                return 0;
            return -1;
        });
    }

    void DeleteProcess()
    {
        //Move to tail and delete
        while (stepdialogs[selection].order != stepdialogs.Count.ToString())
        {
            if(int.Parse(stepdialogs[selection].order)>stepdialogs.Count)
            {
                ShowNotification(new GUIContent("Something wrong in delete process."));
                Thread newThread = new Thread(new ThreadStart(RmNotification));
                newThread.Start();
            }
            StepDialog source = stepdialogs[selection];
            StepDialog downone = stepdialogs[selection + 1];
            selection++;
            stepdialogs.Remove(source);
            stepdialogs.Remove(downone);
            source.order = (int.Parse(source.order) + 1).ToString();
            downone.order = (int.Parse(downone.order) - 1).ToString();
            stepdialogs.Add(source);
            stepdialogs.Add(downone);
            ListSort();
        }
    }

    void Apply()
    {
        ADialog ad=new ADialog();
        ad.id=dialog_id;
        ad.comment=comment;
        ad.dialog=stepdialogs;
        int write = dw.AddToXML(ad);
        if(write==DialogWriter.IDNOTAVAILBLE)
        {
            SwNotification("Dialog ID Not Available.");
        }
        else if(write==DialogWriter.WRITEFAILED)
        {
            SwNotification("Cannot Find the Dialog File.");
        }
        else
        {
            SwNotification("Successfully Add the Dialog into the System.");
        }
    }

    void AddToNPC()
    {

    }

}
