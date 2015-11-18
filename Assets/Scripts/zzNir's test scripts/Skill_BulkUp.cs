using UnityEngine;
using System.Collections;

public class Skill_BulkUp : Skill
{
    private Player[] players = new Player[4];
    private float partyVal = 1.2f;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(30, 1000, 25, "Bulk Up", SkillLevel.Level1);
        
    }

    protected override void Update()
    {
        base.Update();
        if(GetCoolDownTimer() == 0)
        {
            float f = 1.4f;
            if (augment == Augment.Teal)
                f = partyVal;

            foreach(Player player in players)
            {
                modHealth(player.gameObject, false, f);
                if (augment == Augment.Purple)
                    modDefense(player.gameObject, false);
            }
        }
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        players = new Player[4];
        if (augment == Augment.Neutral)
        {
            modHealth(caller);
            players[0] = caller.GetComponent<Player>();
        }
        else if (augment == Augment.Orange)
        {
            modHealth(caller);
            players[0] = caller.GetComponent<Player>();
        }
        else if(augment == Augment.Purple)
        {
            modHealth(caller);
            modDefense(caller);
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
        float f;
        if (add)
            f = 1.3f;
        else
            f = (1.0f / 1.3f);

        caller.GetComponent<Defense>().SetDefense(caller.GetComponent<Defense>().GetDefense() * f);
    }



    private void AddHealth(GameObject caller)
    {
        //heals 1/3 of starting health, which is ~1/4 of temp max hp
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetStartingHealth() / 3);
    }
}
