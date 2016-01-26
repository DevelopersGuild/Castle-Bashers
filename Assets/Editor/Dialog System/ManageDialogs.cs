using UnityEngine;
using System.Collections;
using UnityEditor;
using Kroulis.Dialog;
using System.Threading;
using System.Collections.Generic;

public class ManageDialogs : EditorWindow
{
    private DialogWriter dw = new DialogWriter();
    private DialogReader dr = new DialogReader();
    [MenuItem("Tools/Dialog System/Dialog Manage")]
    static void ManageDialog()
    {
        Rect wr = new Rect(0, 0, 500, 430);
        ManageDialogs window = (ManageDialogs)EditorWindow.GetWindowWithRect(typeof(ManageDialogs), wr, true, "Dialog Manager - Manage");
        window.Show();
    }

    private bool inited = false;
    private List<DialogShortcut> dialog_list = new List<DialogShortcut>();
    private Vector2 scrollposition = new Vector2();
    private int selection=-1;
    string searchid = "";

    void OnGUI()
    {
        if(!inited)
        {
            dialog_list = dr.GetDialogsShortcut();
            inited = true;
        }
        EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Search Information:");
            searchid=EditorGUILayout.TextField("Search ID:",searchid);
            if(GUILayout.Button("SEARCH"))
            {
                Search(searchid);
            }
            EditorGUILayout.LabelField("Dialog List:");
        EditorGUILayout.EndVertical();
        scrollposition = EditorGUILayout.BeginScrollView(scrollposition,false,true,GUILayout.Height(300f),GUILayout.Width(500f));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("CB", GUILayout.Width(20f));
            EditorGUILayout.LabelField("ID", GUILayout.Width(80f));
            EditorGUILayout.LabelField("Comment", GUILayout.Width(150f));
            EditorGUILayout.LabelField("First Text");
            EditorGUILayout.EndHorizontal();
            if (dialog_list.Count >= 1)
                for (int i = 0; i < dialog_list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    SetSelection(i, EditorGUILayout.Toggle(GetSelection(i), GUILayout.Width(20f)));
                    EditorGUILayout.LabelField(dialog_list[i].id, GUILayout.Width(80f));
                    EditorGUILayout.LabelField(dialog_list[i].comment, GUILayout.Width(150f));
                    EditorGUILayout.LabelField(dialog_list[i].firsttext);
                    EditorGUILayout.EndHorizontal();
                }
            else
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Toggle(false, GUILayout.Width(20f));
                EditorGUILayout.LabelField("NULL", GUILayout.Width(80f));
                EditorGUILayout.LabelField("Cannot find any dialog.");
                EditorGUILayout.EndHorizontal();
            }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Delete"))
            {
                if(selection==-1)
                {
                    SwNotification("Please select a dialog first.");
                }
                else
                {
                    int result= dw.RemoveFromXML(dialog_list[selection].id);
                    if(result== DialogWriter.IDNOTEXIST || result == DialogWriter.WRITEFAILED)
                    {
                        SwNotification("Something wrong in the delete process.");
                        selection = -1;
                    }
                    else
                    {
                        selection = -1;
                        Refresh();
                        SwNotification("Delete Success.");
                    }
                }
                
            }
            if(GUILayout.Button("Edit"))
            {
                if(selection==-1)
                {
                    SwNotification("Please select a dialog first.");
                }
                else
                {
                    EditDialog.edit_id = dialog_list[selection].id;
                    EditDialog.StartEdit();
                }
            }
            if(GUILayout.Button("Add to Current NPC"))
            {
                if(selection==-1)
                {
                    SwNotification("Please select a dialog first.");
                }
                else if(Selection.activeGameObject==null)
                {
                    SwNotification("Please select a GameObject that you want to make it a NPC.");
                }
                else
                {
                    GameObject trans = Selection.activeGameObject;
                    NPCDialogControl npc_control = trans.GetComponent<NPCDialogControl>();
                    if (npc_control == null)
                    {
                        npc_control = trans.AddComponent<NPCDialogControl>();
                    }
                    NPCDialog newNPCDialog = new NPCDialog();
                    newNPCDialog.id = dialog_list[selection].id;
                    npc_control.Dialogs.Add(newNPCDialog);
                }
            }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Refresh"))
            {
                Refresh();
            }
            if(GUILayout.Button("Close the Window"))
            {
                this.Close();
            }
            EditorGUILayout.EndHorizontal();
    }

    void OnInspectorUpdate()
    {
        this.Repaint();
    }

    void Refresh()
    {
        dr.RefreshXML();
        dialog_list = dr.GetDialogsShortcut();
    }

    void Search(string id)
    {
        if(id=="")
        {
            SwNotification("Pleast input id first.");
            return;
        }
        for(int i=0;i<dialog_list.Count;i++)
        {
            if(dialog_list[i].id==id)
            {
                selection = i;
                scrollposition = new Vector2(scrollposition.x, (i * 1.000f / (dialog_list.Count + 1)));
                return;
            }
        }
        SwNotification("ID Not Exist.");
    }

    void SwNotification(string content)
    {
        ShowNotification(new GUIContent(content));
        Thread newThread = new Thread(new ThreadStart(RmNotification));
        newThread.Start();
    }

    void RmNotification()
    {
        Thread.Sleep(2000);
        this.RemoveNotification();
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

}
