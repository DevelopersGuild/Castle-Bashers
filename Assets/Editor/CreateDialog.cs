using UnityEngine;
using UnityEditor;
using System.Collections;
//using Kroulis.Dialog;

public class CreateDialog : EditorWindow
{
    [MenuItem("Tools/Manage Dialog")]
    static void ManageDialog () 
    {
        Rect wr = new Rect(0, 0, 500, 500);
        CreateDialog window = (CreateDialog)EditorWindow.GetWindowWithRect(typeof(CreateDialog), wr, true, "Dialog Manager");
        window.Show();
    }

    Object NPC;
    string dialog_id;
    //NormalDialog dial = new NormalDialog();

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        NPC = EditorGUILayout.ObjectField("NPC GameObject", Selection.activeGameObject, typeof(GameObject));
        dialog_id=EditorGUILayout.TextField("Dialog ID","");
        EditorGUILayout.EndVertical();

        if(GUILayout.Button("Check ID"))
        {
            //if(dial.CheckIDAvailable(dialog_id))
            //{
            //    //this.ShowNotification(new GUIContent("Dialog ID "+dialog_id+" is available."));
                
            //}
            //else
            //{
            //    //this.ShowNotification(new GUIContent("Dialog ID " + dialog_id + " is not available."));
            //}
        }
    }
}
