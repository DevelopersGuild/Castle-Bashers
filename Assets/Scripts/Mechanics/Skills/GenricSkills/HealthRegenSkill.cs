using UnityEngine;
using System.Collections;

public class HealthRegenSkill : ISkill
{
    public void UseSkill(GameObject caller, GameObject target = null)
    {
        //TODO Fix this once new health script is created, add ability to pass in regen amount.
        if(caller.tag == "Player")
        {
            caller.GetComponent<PlayerHealth>().regen();
        }
    }
}
