using UnityEngine;
using UnityEditor;
using System.Collections;
using Kroulis.Dialog;
using System.Threading;
using System.Collections.Generic;


public class CreateDialog : EditorWindow
{
    NormalDialog dial = new NormalDialog(true);
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
    int count = 1;
    int selection = -1;
    List<StepDialog> stepdialogs=new List<StepDialog>();
    StepDialog stepdialog=new StepDialog();
    Vector2 scrollposition=new Vector2();

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
                ShowNotification(new GUIContent("Please input dialog ID first."));
                Thread newThread = new Thread(new ThreadStart(RmNotification));
                newThread.Start();
            }
            else if(dial.CheckIDAvailable(dialog_id)==true)
            {
                ShowNotification(new GUIContent("Dialog ID "+dialog_id+" is available."));
                Thread newThread = new Thread(new ThreadStart(RmNotification));
                newThread.Start();
            }
            else
            {
                ShowNotification(new GUIContent("Dialog ID " + dialog_id + " is not available."));
                Thread newThread = new Thread(new ThreadStart(RmNotification));
                newThread.Start();
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
                    ShowNotification(new GUIContent("No step dialog is selected."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
                else
                {
                    stepdialogs.Remove(stepdialogs[selection]);
                    selection = -1;
                    count--;
                    ListSort();
                    ShowNotification(new GUIContent("Successfully deleted."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                
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
                    ShowNotification(new GUIContent("Move Up Success."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();

                }
                else
                {
                    ShowNotification(new GUIContent("Cannot Move Up Because is Already At Top."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
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
                    ShowNotification(new GUIContent("Move Down Success."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();

                }
                else
                {
                    ShowNotification(new GUIContent("Cannot Move Down Because is Already At Tail."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
            }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Apply (Also add To NPC)"))
            {
                if(dialog_id=="")
                {
                    ShowNotification(new GUIContent("Dialog ID Missing."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
                else if(dial.CheckIDAvailable(dialog_id)==false)
                {
                    ShowNotification(new GUIContent("Dialog ID Not Available."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
                else if(NPC==null)
                {
                    ShowNotification(new GUIContent("NPC GameObject Missing."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
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
                    ShowNotification(new GUIContent("Dialog ID Missing."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
                else if (dial.CheckIDAvailable(dialog_id) == false)
                {
                    ShowNotification(new GUIContent("Dialog ID Not Available."));
                    Thread newThread = new Thread(new ThreadStart(RmNotification));
                    newThread.Start();
                }
                else
                {
                    Apply();
                }
            }
            if (GUILayout.Button("Clear All"))
            {
                stepdialogs = new List<StepDialog>();
                count = 1;
                selection = -1;
                ShowNotification(new GUIContent("Clear Success."));
                Thread newThread = new Thread(new ThreadStart(RmNotification));
                newThread.Start();
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

    void Apply()
    {

    }

    void AddToNPC()
    {

    }

}
