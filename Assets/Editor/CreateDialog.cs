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
    int count = 1;
    List<StepDialog> stepdialogs=new List<StepDialog>();
    StepDialog stepdialog=new StepDialog();

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
            NPC = EditorGUILayout.ObjectField("NPC GameObject", Selection.activeGameObject, typeof(GameObject));
            dialog_id=EditorGUILayout.TextField("Dialog ID",dialog_id);
        EditorGUILayout.EndVertical();

        if(GUILayout.Button("Check ID"))
        {
            if(dial.CheckIDAvailable(dialog_id)==true)
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
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
                //EditorGUILayout.IntField("Step#",count);
            EditorGUILayout.LabelField("Step#    " + count.ToString());
                stepdialog.speaker=EditorGUILayout.TextField("Speaker:",stepdialog.speaker);
                EditorGUILayout.EndHorizontal();
            stepdialog.text = EditorGUILayout.TextArea(stepdialog.text,GUILayout.Height(200f));
            AUDIO = EditorGUILayout.ObjectField("Audio", AUDIO, typeof(AudioClip));
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("ADD TO DIALOG LIST"))
            {
                stepdialog.order = count.ToString();
                if(AUDIO!=null)
                {
                    stepdialog.audio = AUDIO.ToString();
                }
                else
                {
                    stepdialog.audio = "";
                }
                stepdialogs.Add(stepdialog);
                stepdialog.Clear();
                count++;
            }
            if(GUILayout.Button("Clear"))
            {
                stepdialog.Clear();
            }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("There are "+stepdialogs.Count.ToString()+" step dialogs in this dialog.");
        //EditorGUILayout.BeginScrollView();
        EditorGUILayout.BeginVertical(GUILayout.Height(200));
            for (int i = 0; i < stepdialogs.Count;i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Toggle(stepdialogs[i].order,false);
                EditorGUILayout.LabelField(stepdialogs[i].speaker);
                EditorGUILayout.LabelField(stepdialogs[i].text);
                EditorGUILayout.EndHorizontal();
            }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Delete"))
        {
            for(int i=0;i<stepdialogs.Count;i++)
            {
                
            }
        }
        if(GUILayout.Button("Move Up"))
        {

        }
        if(GUILayout.Button("Move down"))
        {

        }
        EditorGUILayout.EndHorizontal();
        if(GUILayout.Button("Apply"))
        {

        }
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

}
