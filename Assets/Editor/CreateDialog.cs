using UnityEngine;
using UnityEditor;
using System.Collections;
using Kroulis.Dialog;
using System.Threading;

public class CreateDialog : EditorWindow
{
    NormalDialog dial = new NormalDialog(true);
    [MenuItem("Tools/Manage Dialog")]
    static void ManageDialog () 
    {
        Rect wr = new Rect(0, 0, 500, 500);
        CreateDialog window = (CreateDialog)EditorWindow.GetWindowWithRect(typeof(CreateDialog), wr, true, "Dialog Manager");
        window.Show();
    }

    Object NPC;
    string dialog_id;

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        NPC = EditorGUILayout.ObjectField("NPC GameObject", Selection.activeGameObject, typeof(GameObject));
        dialog_id=EditorGUILayout.TextField("Dialog ID","");
        EditorGUILayout.EndVertical();

        if(GUILayout.Button("Check ID"))
        {
            if(dial.CheckIDAvailable(dialog_id))
            {
                ShowNotification(new GUIContent("Dialog ID "+dialog_id+" is available."));
                Thread.Sleep(1000);
                RmNotification();
            }
            else
            {
                ShowNotification(new GUIContent("Dialog ID " + dialog_id + " is not available."));
                Thread.Sleep(1000);
                RmNotification();
            }
        }
    }

    void RmNotification()
    {
        this.RemoveNotification();
    }
}
