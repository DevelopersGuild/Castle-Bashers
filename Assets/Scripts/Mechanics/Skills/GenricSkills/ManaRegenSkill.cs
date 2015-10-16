using UnityEngine;
using System.Collections;

public class ManaRegenSkill : ISkill
{
    public void UseSkill(GameObject caller, GameObject target = null)
    {
        if(caller.tag == "Player")
        {
            //TODO Added Mana regen to player.
        }
    }
}
