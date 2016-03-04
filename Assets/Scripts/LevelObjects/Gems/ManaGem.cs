using UnityEngine;
using System.Collections;


class ManaGem : Gem
{
    Mana mana;
    public override void Initialize(int quality)
    {
        base.Initialize(quality);
        mana = player.GetComponent<Mana>();
        gemName = "Mana Gem";
        bonus = quality * 20;
        description = "Increases Base Mana by " + bonus + " %";

    }

    public override void activate()
    {
        if (active == false)
        {
            mana.addBonusPercentMana(bonus);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            mana.addBonusPercentMana(-bonus);
            active = false;
        }
    }
}