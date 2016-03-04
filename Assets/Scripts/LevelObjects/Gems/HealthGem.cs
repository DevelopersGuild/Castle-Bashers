using UnityEngine;
using System.Collections;

class HealthGem : Gem
{
    Health health;
    public override void Initialize(int quality)
    {
        base.Initialize(quality);
        health = player.GetComponent<Health>();
        gemName = "Health Gem";
        bonus = quality * 15;
        description = "Increases Base Health by " + bonus + " %";

    }

    public override void activate()
    {
        if (active == false)
        {
            health.addBonusPercentHP(bonus);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            health.addBonusPercentHP(-bonus);
            active = false;
        }
    }
}