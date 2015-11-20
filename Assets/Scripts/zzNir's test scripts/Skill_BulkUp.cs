using UnityEngine;
using System.Collections;

public class Skill_BulkUp : Skill
{
    private Player[] players = new Player[4];
    private float partyVal = 1.2f;
    private bool on = false;
    private bool regen = false;
    private float regenTicker, regenAmount;
    private int defDelta;

    /*
        Bulk Up skill (buff) - Raises max hp of the player
        Augments:
        Orange - Regens a portion of your hp over the duration
        Purple - Defense increases as well
        Teal - Max hp increase is reduced, but affects every player instead of just the caller (party buff)
    */

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(30, 1000, 25, "Bulk Up", SkillLevel.Level1);
    }

    protected override void Update()
    {
        base.Update();
        if (on)
        {
            if (regen)
            {
                if (regenTicker <= 0)
                {
                    regenTicker = 0.25f;
                    players[0].GetComponent<Health>().AddHealth(regenAmount);
                }
                regenTicker -= Time.deltaTime;
            }
            if (GetCoolDownTimer() == 0)
            {
                on = false;
                float f = 1.4f;
                if (augment == Augment.Teal)
                    f = partyVal;

                foreach (Player player in players)
                {
                    modHealth(player.gameObject, false, f);
                    //to reduce hp back to max if it's overflowing
                    player.GetComponent<Health>().AddHealth(0);
                    if (augment == Augment.Purple)
                        modDefense(player.gameObject, false);
                }
            }
        }
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        on = true;
        players = new Player[4];
        if (augment == Augment.Neutral)
        {
            modHealth(caller);
            players[0] = caller.GetComponent<Player>();
        }
        else if (augment == Augment.Orange)
        {
            modHealth(caller);
            regenTicker = 0.25f;
            regen = true;
            regenAmount = regenTicker * caller.GetComponent<Health>().GetMaxHP() / (GetCoolDown() * 3) ;
            players[0] = caller.GetComponent<Player>();
        }
        else if(augment == Augment.Purple)
        {
            modHealth(caller);
            modDefense(caller);
            players[0] = caller.GetComponent<Player>();
        }
        else if(augment == Augment.Teal)
        {
            players = FindObjectOfType<PlayerManager>().getPlayers();
            skillType = Type.Support;
            foreach(Player player in players)
            {
                modHealth(player.gameObject, true, partyVal);
            }
        }

    }

    private void modHealth(GameObject caller, bool add = true, float val = 1.4f)
    {
        if (!add)
            val = 1 / val;

        caller.GetComponent<Health>().SetMaxHP(caller.GetComponent<Health>().GetStartingHealth() * val);

    }

    private void modDefense(GameObject caller, bool add = true)
    {
        int val;
        float f;
        if (add)
            f = 1.3f;
        else
            f = (1.0f / 1.3f);

        val = (int)(caller.GetComponent<Defense>().GetBasePhysicalDefense() * f);
        if (add)
            defDelta = val;
        else
            defDelta *= -1;



        caller.GetComponent<Defense>().SetPhysicalDefense(caller.GetComponent<Defense>().GetPhysicalDefense() + defDelta);
    }



    private void AddHealth(GameObject caller)
    {
        //heals 1/3 of starting health, which is ~1/4 of temp max hp
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetStartingHealth() / 3);
    }
}
