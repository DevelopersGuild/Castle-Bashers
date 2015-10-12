using UnityEngine;
using System.Collections;

public class ManaRegenSkill : ISkill
{
    public void UseSkill(GameObject caller)
    {
        if(caller.tag == "Player")
        {
            //TODO Added Mana regen to player.
        }
    }
}
