using UnityEngine;
using System.Collections;
using Kroulis.Dialog;

namespace Kroulis.Dialog.Objects
{
    public class Example_Dialog_Action : DialogAction
    {

        public override void Action()
        {
            Main_Process main_process = GetComponent<NPCDialogControl>().GetMainProcess();
            main_process.OpenDialog("10000", GetComponent<NPCDialogControl>().NPC_Name);
        }
    }
}

