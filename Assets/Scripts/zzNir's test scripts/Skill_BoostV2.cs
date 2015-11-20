﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_BoostV2 : Skill
{
    private GameObject call;
    private Animator anim;
    List<UnityEditor.Animations.AnimatorControllerLayer> layers = new List<UnityEditor.Animations.AnimatorControllerLayer>();
    List<UnityEditor.Animations.AnimatorState> animationsStates_Base = new List<UnityEditor.Animations.AnimatorState>();
    List<UnityEditor.Animations.AnimatorState> animationsStates_Attack = new List<UnityEditor.Animations.AnimatorState>();

    /*
       Boost Up skill (buff) - Increases attack speed of the caller
       Augments:
       Orange - Also increases movement speed
       Purple - Increases attack speed bonus even further
       Teal - Increases your cast speed too

    */

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(15, 1000, 15, "Boost", SkillLevel.Level1);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            UnityEditor.Animations.AnimatorController ac = (UnityEditor.Animations.AnimatorController)anim.runtimeAnimatorController;
            foreach (UnityEditor.Animations.AnimatorControllerLayer acl in ac.layers)
            {
                layers.Add(acl);
            }

            // Base layer states
            foreach (UnityEditor.Animations.ChildAnimatorState s in layers[0].stateMachine.states)
            {
                animationsStates_Base.Add(s.state);
                Debug.Log(s.state.speed);
            }


        }
    }

    protected override void Update()
    {
        base.Update();
        if (GetCoolDownTimer() == 0)
        {
            float f = 0.75f;
            if (augment == Augment.Purple)
                f = 0.5f;

            if (augment == Augment.Teal)
                modAnimSpd(call, false);
            else
            {
                modAtkSpd(call, false, f);
                if (augment == Augment.Orange)
                    modSpd(call, false);
            }

        }
    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        call = caller;
        if (augment == Augment.Neutral)
        {
            modAtkSpd(caller);
        }
        else if (augment == Augment.Orange)
        {
            modAtkSpd(caller);
            modSpd(caller);
        }
        else if (augment == Augment.Purple)
        {
            modAtkSpd(caller, true, 0.5f);
            value *= 1.3f;
        }
        else if (augment == Augment.Teal)
        {
           // modAtkSpd(caller);
            modAnimSpd(caller);
            skillType = Type.Ranged;
        }

    }

    private void modAtkSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (!add)
            val = 1 / val;


        foreach (UnityEditor.Animations.AnimatorState state in animationsStates_Base)
        {
            if (state.name == "BasicAttack" || state.name == "PowerAttack")
            {
              //  state.speed *= val;
            }
        }

        //caller.GetComponent<Animation>().GetClip("BasicAttack").frameRate = (caller.GetComponent<Animation>().GetClip("BasicAttack").frameRate * val);
        //caller.GetComponent<Animation>().GetClip("PowerAttack").frameRate = (caller.GetComponent<Animation>().GetClip("PowerAttack").frameRate * val);
    }

    private void modAnimSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (!add)
            val = 1 / val;

        caller.GetComponent<Animator>().speed = caller.GetComponent<Animator>().speed * val;
    }

    private void modSpd(GameObject caller, bool add = true, float val = 0.75f)
    {
        if (add)
            val = 1 / val;

        //caller.GetComponent<Player>().SetAgility(caller.GetComponent<Player>().GetAgility() * val);

        foreach (UnityEditor.Animations.AnimatorState state in animationsStates_Base)
        {
            if (state.name == "Walk" || state.name == "Jump")
            {
              //  state.speed = 1;
            }
        }
    }



    private void AddHealth(GameObject caller)
    {
        //heals 1/3 of starting health, which is ~1/4 of temp max hp
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetStartingHealth() / 3);
    }
}

