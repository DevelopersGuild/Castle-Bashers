using UnityEngine;
using System.Collections;
using UnityEditor;
using Kroulis.Dialog;
using System.Threading;
using System.Collections.Generic;

public class EditDialog : EditorWindow
{
    public static string edit_id = "";
    private DialogWriter dw = new DialogWriter();
    private DialogReader dr = new DialogReader();
    public static void StartEdit()
    {
        Rect wr = new Rect(0, 0, 500, 580);
        EditDialog window = (EditDialog)EditorWindow.GetWindowWithRect(typeof(EditDialog), wr, true, "Dialog Manager - Edit");
        window.Show();
    }

    private bool inited = false;
    Object AUDIO;
    string comment = "";
    private int count = 1;
    private int selection = -1;
    private List<StepDialog> stepdialogs = new List<StepDialog>();
    private StepDialog stepdialog = new StepDialog();
    private Vector2 scrollposition = new Vector2();

    void OnGUI()
    {
        if(!inited)
        {
            ADialog source = dr.GetDialog(edit_id);
            comment = source.comment;
            stepdialogs = source.dialog;
            count = stepdialogs.Count + 1;
            inited = true;
        }
        //Basic information
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dialog ID   " + edit_id,GUILayout.Width(200f));
        comment = EditorGUILayout.TextField("Comment", comment);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        //Step Dialog Create Part
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.IntField("Step#",count);
        EditorGUILayout.LabelField("Step#  " + count.ToString(), GUILayout.Width(100f));
        stepdialog.speaker = EditorGUILayout.TextField("Speaker:", stepdialog.speaker);
        EditorGUILayout.EndHorizontal();
        stepdialog.text = EditorGUILayout.TextArea(stepdialog.text, GUILayout.Height(200f));
        AUDIO = EditorGUILayout.ObjectField("Audio", AUDIO, typeof(AudioClip));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
        //Button Add to dialog list
        if (GUILayout.Button("ADD TO DIALOG LIST"))
        {
            stepdialog.order = count.ToString();
            if (AUDIO != null)
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
        if (GUILayout.Button("Clear"))
        {
            stepdialog.Clear();
        }
        EditorGUILayout.EndHorizontal();
        //List Showing Part
        EditorGUILayout.LabelField("There are " + stepdialogs.Count.ToString() + " step dialogs in this dialog.");
        scrollposition = EditorGUILayout.BeginScrollView(scrollposition, GUILayout.Height(200f));
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CB", GUILayout.Width(20f));
        EditorGUILayout.LabelField("#", GUILayout.Width(20f));
        EditorGUILayout.LabelField("Speaker", GUILayout.Width(60f));
        EditorGUILayout.LabelField("Text");
        EditorGUILayout.EndHorizontal();
        if (stepdialogs.Count >= 1)
            for (int i = 0; i < stepdialogs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                SetSelection(i, EditorGUILayout.Toggle(GetSelection(i), GUILayout.Width(20f)));
                EditorGUILayout.LabelField(stepdialogs[i].order, GUILayout.Width(20f));
                EditorGUILayout.LabelField(stepdialogs[i].speaker, GUILayout.Width(60f));
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
        if (GUILayout.Button("Delete"))
        {
            if (selection == -1)
            {
                SwNotification("No step dialog is selected.");
            }
            else
            {
                DeleteProcess();
                stepdialogs.Remove(stepdialogs[stepdialogs.Count - 1]);
                selection = -1;
                count--;
                ListSort();
                SwNotification("Successfully deleted.");

            }
        }
        //Button Move Up
        if (GUILayout.Button("Move Up"))
        {
            if (stepdialogs[selection].order != "1")
            {
                StepDialog source = stepdialogs[selection];
                StepDialog upone = stepdialogs[selection - 1];
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
        if (GUILayout.Button("Move down"))
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
        if (GUILayout.Button("Apply"))
        {
                Apply();
        }
        if (GUILayout.Button("Clear All"))
        {
            comment = "";
            stepdialogs = new List<StepDialog>();
            count = 1;
            selection = -1;
            SwNotification("Clear Success.");
        }
        EditorGUILayout.EndHorizontal();
        //Button Clost the window.
        if (GUILayout.Button("Close the window"))
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

    void SetSelection(int order, bool value)
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

    void DeleteProcess()
    {
        //Move to tail and delete
        while (stepdialogs[selection].order != stepdialogs.Count.ToString())
        {
            if (int.Parse(stepdialogs[selection].order) > stepdialogs.Count)
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
        ADialog ad = new ADialog();
        ad.id = edit_id;
        ad.comment = comment;
        ad.dialog = stepdialogs;
        dw.RemoveFromXML(edit_id);
        int write = dw.AddToXML(ad);
        if (write == DialogWriter.IDNOTAVAILBLE)
        {
            SwNotification("Something Wrong in the Apply Process.");
        }
        else if (write == DialogWriter.WRITEFAILED)
        {
            SwNotification("Cannot Find the Dialog File.");
        }
        else
        {
            SwNotification("Successfully Apply the Dialog.");
        }
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


}
