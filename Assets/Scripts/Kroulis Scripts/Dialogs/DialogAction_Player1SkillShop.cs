using UnityEngine;
using System.Collections;
using Kroulis.Dialog;


namespace Kroulis.Demo
{
    public class DialogAction_Player1SkillShop : DialogAction
    {
        public override void Action()
        {
            Main_Process mainprocess = GameObject.Find("Main Process").GetComponent<Main_Process>();
            mainprocess.UI_SkillShop_Open(1);
        }
    }
}

