using UnityEngine;
using System.Collections;

class AgilityGem : Gem
{

    public override void Initialize(int quality)
    {
        base.Initialize(quality);
        gemName = "Agility Gem";
        bonus = quality * 10;
        description = "Increases Agility by " + bonus;

    }

    public override void activate()
    {
        if (active == false)
        {
            player.AddBonusAgility(bonus);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            player.AddBonusAgility(-bonus);
            active = false;
        }
    }
}