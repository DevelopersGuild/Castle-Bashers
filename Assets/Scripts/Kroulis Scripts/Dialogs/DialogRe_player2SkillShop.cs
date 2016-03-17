using UnityEngine;
using System.Collections;
using Kroulis.Dialog;

namespace Kroulis.Demo
{
    public class DialogRe_player2SkillShop : DialogRequirement
    {
        public override bool MeetRequirement()
        {
            Main_Process mainprocess = GameObject.Find("Main Process").GetComponent<Main_Process>();
            if (mainprocess.One_player_per_client == false)
                return true;
            return false;
        }
        // Return a int that is the difference of your requirement value with the character's already have value. Negative meens didn't reach the requirement.
        public override int MeetRequirementI()
        {
            return 0;
        }
    }
}
