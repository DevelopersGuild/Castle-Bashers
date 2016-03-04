using UnityEngine;
using System.Collections;

class StrengthGem : Gem
{

    public override void Initialize(int quality)
    {
        base.Initialize(quality);
        gemName = "Strength Gem";
        bonus = quality * 10;
        description = "Increases Strength by " + bonus;

    }

    public override void activate()
    {
        if (active == false)
        {
            player.AddBonusStrength(bonus);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            player.AddBonusStrength(-bonus);
            active = false;
        }
    }

}