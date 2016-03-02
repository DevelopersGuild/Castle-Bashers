using UnityEngine;
using System.Collections;


class IntelligenceGem : Gem
{

    public override void Initialize(int quality)
    {
        base.Initialize(quality);
        gemName = "Intelligence Gem";
        bonus = quality * 10;
        description = "Increases Intelligence by " + bonus;

    }

    public override void activate()
    {
        if (active == false)
        {
            player.AddBonusIntelligence(bonus);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            player.AddBonusIntelligence(-bonus);
            active = false;
        }
    }
}