using UnityEngine;
using System.Collections;
using System;

public class MaladyV2 : Boss
{

    //Weaker Malady Boss, a sort of disease witch thing


    private float clawLim, swarmLim, summonLim, teleLim, teleClawLim;
    private float claw_CD, swarm_CD, swarm_Duration, hands_CD, summon_CD, teleport_CD, teleClaw_CD, refreshPriority;
    private float rnd, animationDelay, teleDuration, lerpDuration;
    //Maybe no hands_CD, could be a gameObject that creates hands every few seconds while Malady exists
    //probably better ^^
    private bool isTeleporting;
    private Vector3 teleTarget, startPos, center;

    //Center of room, to know where Malady can teleport;
    public GameObject CenterObj;

    //Skills and scales (for facing directions)
    public GameObject ClawSkill, SwarmObj, SummonSkill, handSkill;
    private Vector3 tempVec, MaladyLeft, MaladyRight, ClawLeft, ClawRight;
    private float teleClawStage = 0;
    public GroupingManager Grouper;
    private Skill sClaw;

    private float offset1, offset2, offset3, one, two, three, four, tempThreat, tempDamage, temp1, temp2;
    private int size;
    private PlayerManager playerM;
    private bool ranged, grouping, melee, support, refresh;
    private float range, group, mel, supp;

    private Animator animator;
    //if any animations are going on, it shouldn't be doing other stuff
    //ANIMATION STUFF~~~~~~~~~~~~~~~~~ animating = true whenever animation starts, false when ends in most cases (not for combos of animations, false at end of combo)
    private bool animating = false;
    private bool running = false;
    //a for animation
    //[HideInInspector]
    //public bool aClaw, aTeleport
    private bool summoning = true;
    private bool spawnM = true;
    private bool updateTele;
    private Vector3 updateTeleVec;

    // Use this for initialization
    void Start()
    {

        base.Start();
        sClaw = ClawSkill.GetComponent<ClawAttack>();
        animator = GetComponent<Animator>();
        claw_CD = 4 + UnityEngine.Random.Range(0, 3);
        clawLim = claw_CD;
        teleClaw_CD = 4 + UnityEngine.Random.Range(0, 3);
        teleClawLim = claw_CD;
        swarm_Duration = 0;
        swarm_CD = 5 + UnityEngine.Random.Range(0, 5);
        swarmLim = swarm_CD;
        //only starts counting when duration is over
        hands_CD = 3;
        summon_CD = 5 + UnityEngine.Random.Range(0, 5);
        summonLim = summon_CD;
        teleport_CD = 6 + UnityEngine.Random.Range(0, 4);
        teleLim = teleport_CD;
        animationDelay = 2;
        teleDuration = 0f;
        //teleDuration = animation.GetClip("Teleport").length;
        refreshPriority = 10;
        refresh = false;

        isTeleporting = false;
        updateTele = false;
        teleTarget = new Vector3(0, 0, 0);
        updateTeleVec = teleTarget;
        startPos = teleTarget;
        center = CenterObj.transform.position;

        moveController.isFlinchable = false;
        moveController.isKnockbackable = false;
        lerpDuration = 0;
        grouping = false;

        ClawLeft = ClawSkill.transform.localScale;
        ClawRight = new Vector3(ClawLeft.x * -1, transform.localScale.y, transform.localScale.z);
        MaladyLeft = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        MaladyRight = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (spawnM)
        {
            playerM = FindObjectOfType<PlayerManager>();
            players = playerM.getPlayers();
            size = players.Length;
            threatLevel = damageDealt = players;
            damageDealt = new Player[size];
            for (int i = 0; i < size; i++)
            {
                players[i].Reset();
            }
            one = two = three = four = 0;
            tempThreat = 0;
            tempDamage = 0;
            temp1 = 0;
            temp2 = 0;

            Grouper.setPlayerGroup(players, size);

            grouping = false;
            getPlayerType();


            int initialTarget = UnityEngine.Random.Range(0, size);
            target = players[initialTarget].gameObject;
            spawnM = false;
        }
        targetPos = target.transform.position;
        CalcDirection();

        if (refresh)
        {
            Debug.Log(ranged + " " + grouping + " " + melee + " " + support);
            refresh = false;
        }

        if (teleClawStage > 0)
        {
            teleClawUpdate();
        }
        animator.SetBool("isTeleporting", isTeleporting);
        if (isTeleporting)
        {
            if(updateTele)
            {
                teleTarget = target.transform.position;
            }
            //ANIMATION STUFF~~~~~~~~~~~~~~~~~ 
            //How teleport works: transform to swarm -> isTel = true, swarm animations for moving around (same as
            //normal swarm animations, low speed = circular, high speed = more horizontal, accel one way + low speed other way = turning
            //remember to switch x scale on turn) -> isTel = false, transform to malady.
            //if tele claw, then claw, tele process to new location (original unless people are clumping there)
            //positions are already put in with a set time taken to get everywhere (lerp, teleduration, below) so animations have a set time

            //ANIMATION STUFF~~~~~~~~~~~~~~~~~ isTeleporting set to true at end of transforming into swarm animation
            //ANIMATION STUFF~~~~~~~~~~~~~~~~~ invincible set to true at start of transforming into swarm animation
//            Debug.Log(teleDuration + "    " + transform.position + "           vs               " + startPos + "       vs         " + teleTarget);
            transform.position = Vector3.Lerp(startPos, teleTarget + updateTeleVec, teleDuration);
            if (teleDuration >= 1)
            {
                teleDuration = 0;
                isTeleporting = false;
                isInvincible = false;
                updateTele = false;
                updateTeleVec = Vector3.zero;
                //ANIMATION STUFF~~~~~~~~~~~~~~~~~ 
                //stop current animation, start transform back to human animation

                //not needed below
                //isTeleporting set to false at start of transforming into malady animation
                //invincible set false at end of anim
            }
            else
            {
                teleDuration += Time.unscaledDeltaTime * (10/(teleTarget - startPos).magnitude);
            }

        }

        else if (!animating)
        {
            Act(classification);
        }

        if (refreshPriority >= 10)
        {
            sortPriority();
            refreshPriority = 0;
        }


        if (isInvincible && invTime <= 0)
        {
            isInvincible = false;
        }



        refreshPriority += Time.unscaledDeltaTime;
        hands_CD += Time.unscaledDeltaTime;
        animationDelay += Time.unscaledDeltaTime;
        invTime -= Time.unscaledDeltaTime;
    }

    public override void Act(Type t)
    {
        if (hands_CD > 2.5f)
        {
            hands_CD = 0;
            //Debug.Log("Hands " + Time.time); 
            HandsAttack();//
        }
        if (animationDelay > (1f + hp.GetCurrentHealth() / hp.GetMaxHP()))
        {
            getPlayerType();
            float randNum = UnityEngine.Random.Range(1, 100);
            if (randNum < 5 - 5 * (4 - size))
            {
                target = players[3].gameObject;
            }
            else if (randNum < 20 - 10 * (4 - size))
            {
                target = players[2].gameObject;
            }
            else if (randNum < 45 - 15 * (4 - size))
            {
                target = players[0].gameObject;
            }
            else
            {
                target = players[0].gameObject;
            }

            animationDelay = 0;
            //Debug.Log(claw_CD + " " + swarm_CD + " " + swarm_Duration + " " + summon_CD + " " + teleport_CD);
            if (claw_CD >= clawLim)
            {
                Claw();
            }
            else if (swarm_CD >= swarmLim && swarm_Duration <= 0)
            {
                Swarm();
            }
            else if (summon_CD >= summonLim)
            {
                Summon();
            }
            else if (teleport_CD >= teleLim)
            {
                Teleport();
            }
            else
            {
                int x = UnityEngine.Random.Range(0, 4);
                if (x == 0)
                    Summon();
                else if (x == 1)
                    Teleport();
                else if (x == 2 && swarm_Duration <= 0)
                    Swarm();
                else if (x == 3 && zDiff < 4)
                    Claw();
                else
                    teleClaw();


                //animationDelay = 1;
            }
            claw_CD += 1;
            swarm_CD += 1;
            summon_CD += 1;
            teleport_CD += 1;
        }

        if (swarm_Duration > 0)
        {
            swarm_Duration -= Time.unscaledDeltaTime;
        }


    }


    private void setTelTrue(float f)
    {
        isTeleporting = true;
    }

    private void setTelFalse(float f)
    {
        isTeleporting = false;
    }


    private void Claw()
    {
        claw_CD = 0;
        clawLim = 4 + UnityEngine.Random.Range(0, 3);
        distance = target.transform.position.x - transform.position.x;
        if (distance < 0)
            ClawSkill.transform.localScale = ClawLeft;
        else
            ClawSkill.transform.localScale = ClawRight;

        if (ranged || support)
        {
            clawLim -= 1;
            teleClaw();
        }
        else if (melee)
        {
            if (zDiff < 4 && Math.Abs(distance) < 3)
            {
                clawLim -= 1;
                //ANIMATION STUFF~~~~~~~~~~~~~~~~~ 
                //play animation                                        -----------------------
                //animation sets animating to true using setAnimating()
                //animating false at end
                //for all animations
                animator.SetTrigger("useClaw");
            }
            else
            {
                teleClaw();
            }
        }
        else if (grouping)
        {
            if (zDiff < 4 && distance < 3)
            {
                clawLim -= 2;
                //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation                                        -----------------------
                animator.SetTrigger("useClaw");
            }
            else
            {
                teleClaw();
            }
        }
        else
        {
            if (zDiff < 4 && distance < 3)
            {
                //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation                                        -----------------------
                animator.SetTrigger("useClaw");
            }
            else
            {
                teleClaw();
            }
        }
        //if ranged/support, tele claw
        //if grouping, if close claw if far tele claw
        //if melee, claw
    }

    private void InstantiateClaw()
    {
        Instantiate(ClawSkill, transform.position, ClawSkill.transform.rotation);

    }

    //ANIMATION STUFF~~~~~~~~~~~~~~~~~ run at end of teleClaw animations (so transform to human animation and claw swipe animation)
    private void updateTeleClaw()
    {
        if (teleClawStage > 0)
        {
            teleClawStage++;
            running = false;
        }
    }

    public void setAnimating(int bl)
    {

        //ANIMATION STUFF~~~~~~~~~~~~~~~~~  to keep animating on during the teleClaw combo
        if (teleClawStage > 0 || bl == 1)
        {
            animating = true;
        }
        else
            animating = false;
    }

    private void teleClaw()
    {
        setAnimating(1);
        teleClawStage = 1;
        teleClawUpdate();
        updateTele = true;
    }

    private void teleClawUpdate()
    {
        if (!running)
        {
            if (teleClawStage == 1)
            {
                tempVec = transform.position;
                //if player next to wall, tele to other side
                running = true;
                float f = (UnityEngine.Random.Range(100, 200) / 100.0f) * ((UnityEngine.Random.Range(0, 2) - 0.5f) * 2);
                //transform.position = new Vector3(target.transform.position.x + f, transform.position.y, target.transform.position.z);
                updateTeleVec = new Vector3(f, 0, 0);
                Teleport(new Vector3(target.transform.position.x + f, transform.position.y, target.transform.position.z));
            }
            else if (teleClawStage == 2)
            {
                distance = target.transform.position.x - transform.position.x;
                if (distance < 0)
                    ClawSkill.transform.localScale = ClawLeft;
                else
                    ClawSkill.transform.localScale = ClawRight;

                running = true;
                Instantiate(ClawSkill, transform.position, ClawSkill.transform.rotation);
                //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation                                        -----------------------
                animator.SetTrigger("useClaw");
            }
            else if (teleClawStage == 3)
            {
                float camp = 0;
                bool camped = false;
                running = true;
                foreach (Player player in players)
                {
                    if (Math.Abs((player.transform.position - tempVec).magnitude) < 2.5f)
                    {
                        camp++;
                    }
                }
                if (camp >= players.Length / 2.0)
                    camped = true;

                Teleport(tempVec, camped);
            }
            else if (teleClawStage > 3)
            {
                teleClawStage = 0;
                animating = false;
                running = false;

            }
            else
            {
                Debug.Log("How did I get here?...here");
            }
        }
    }

    private void Swarm()
    {
        swarm_CD = 0;
        swarmLim = 11 + UnityEngine.Random.Range(0, 5);

        swarm_Duration = 8;

        if (ranged && support && !grouping)
            swarmLim += 1;
        else if (ranged || support || grouping)
            swarmLim -= 2;
        if (melee)
            swarmLim -= 1;

        animator.SetTrigger("usePuke");

        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation                          run spawnSwarm at end
    }

    private void spawnSwarm()
    {
        //spawn with an offset to match animation position
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ create vector3 offset to match animation
        SwarmBehaviour swarm = Instantiate(SwarmObj, transform.position, transform.rotation) as SwarmBehaviour;
        swarm_Duration = swarm.Duration;
        swarm.setTarget(target);

    }

    private void Summon()
    {
        summon_CD = 0;
        summonLim = 7 + UnityEngine.Random.Range(0, 6);

        if (grouping)
            summonLim -= 3;
        if (ranged || support)
            summonLim -= 1;
        if (melee && !grouping)
            summonLim += 1;

        //slightly wierd due to having a scale of 10, would be ok after we have actual stuff
        summoning = true;
        animator.SetTrigger("useSpell");
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation              ----------------------- run SummonPortal at end

    }

    public void endFunction()
    {
        if(summoning)
        {
            SummonPortal();
        }
        //if(poly) 
        //{
        //    PolyCloud
        //}
    }

    public void SummonPortal()
    {
        float dir = Mathf.Sign(center.x - transform.position.x);
        Vector3 summonPos = new Vector3(UnityEngine.Random.Range(5, 9) * dir,0, (UnityEngine.Random.Range(10, 20) / 4.0f) * (target.transform.position.z - transform.position.z));
        summonPos /= 2;
        summonPos += transform.position;
        SummoningPortal sp = Instantiate(SummonSkill, summonPos, SummonSkill.transform.rotation) as SummoningPortal;
        sp.setTarget(target);
    }


    //Maybe not used, we'll see
    //used for teleClaw. Claws in direction (true for left, false for right)
    //make hitbox slightly inside malady?
    private void Claw(bool left)
    {
        float f = 1;
        if (!left)
            f *= -1;

        //set facing direction
        //create claw
        //set claw's x scaling to pos or neg based on bool
    }


    private void Tele()
    {

    }

    //used for teleClaw
    private void Teleport(GameObject targ)
    {
        Debug.Log("TELEPORT");
        //set is Tel to true at end of anim, set true here for testing
        //isTeleporting = true;
        lerpDuration = teleDuration;
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play transform animation                     set isTeleporting = true at end of anim
        float dir = Mathf.Sign(center.x - targ.transform.position.x);
        if (dir == 0)
        {
            dir = 1;
        }

        startPos = transform.position;
        teleTarget = targ.transform.position + new Vector3(dir * UnityEngine.Random.Range(0, 10) / 10f, 0, 0);
        Debug.Log(dir + "   " + (teleTarget - startPos));
    }

    //for teleClaw. camped is if people around target, if true go to a different location (for return of teleClaw, for now camped always is false)
    private void Teleport(Vector3 targ, bool camped = false)
    {
        //set true for testing, should be set at end of animation
        //isTeleporting = true;
        lerpDuration = teleDuration;
        startPos = transform.position;
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play transforming animation                       set isTel true at end of anim
        animator.SetTrigger("useTransform");
        if (!camped)
        {
            teleTarget = targ;
        }
        else
        {
            float dir = Mathf.Sign(center.x - target.transform.position.x);
            if (dir == 0)
            {
                dir = 1;
            }

            startPos = transform.position;
            teleTarget = new Vector3(target.transform.position.x + dir * UnityEngine.Random.Range(0, 10) / 10.0f, transform.position.y, target.transform.position.z);
            //teleTarget = target.transform.position + new Vector3(dir * UnityEngine.Random.Range(0, 10) / 10f, 0, 0);
            Debug.Log(teleTarget);
        }
        

    }

    private void Teleport()
    {
        teleLim = UnityEngine.Random.Range(0, 5) + 6;
        lerpDuration = teleDuration;
        //isTeleporting = true;
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play animation                       set isTel true if end of anim
        animator.SetTrigger("useTransform");
        float f = 0;
        foreach (Player p in players)
        {
            if (center.x - p.transform.position.x > 0)
                f--;
            else
                f++;

        }
        float dir = Mathf.Sign(f);

        startPos = transform.position;
        teleTarget = transform.position + new Vector3(UnityEngine.Random.Range(3, 13) * dir + center.x - transform.position.x
            , 0, (UnityEngine.Random.Range(5, 15) / 10.0f) * (target.transform.position.z - transform.position.z));

        teleport_CD = 0;
        claw_CD += 1;
        swarm_CD += 2;
        summon_CD += 2;
        teleport_CD += 1;

        if (ranged)
            teleLim = UnityEngine.Random.Range(0, 3) + 4;
        else if (grouping || melee)
            teleLim = UnityEngine.Random.Range(0, 3) + 8;
        if (support && !ranged)
            teleLim += 1;

        if (hp.GetCurrentHealth() < hp.GetMaxHP() / 4)
            teleLim -= 3;
        else if (hp.GetCurrentHealth() < hp.GetMaxHP() / 2)
            teleLim -= 2;

        teleDuration = 0;

    }



    public float getDirection()
    {
        return Mathf.Sign(transform.localScale.x);
    }

    public void CalcDirection()
    {
        if (targetPos.x - transform.position.x > 0)
            transform.localScale = MaladyRight;
        else
            transform.localScale = MaladyLeft;
    }

    private void sortPriority()
    {
        //get skill type from player
        players = playerM.getPlayers();
        threatLevel = playerM.getSortedPlayers(1);
        damageDealt = playerM.getSortedPlayers(2);

        one = two = three = four = 0;
        for (int i = 0; i < size; i++)
        {
            temp1 = players[i].getManagerID();
            temp2 = players[i].getManagerID();

            if (i == 0)
                one += temp1 + temp2;
            else if (i == 1)
                two += temp1 + temp2;
            else if (i == 2)
                three += temp1 + temp2;
            else if (i == 3)
                four += temp1 + temp2;

        }

        for (int i = 0; i < size; i++)
        {
            float tempVar = 0;
            if (i == 0)
                tempVar = one;
            if (i == 1)
                tempVar = two;
            if (i == 2)
                tempVar = three;
            if (i == 3)
                tempVar = four;

            players[i].setPriorityID((tempVar));
        }
        players = playerM.getSortedPlayers(3);
        //Debug.Log("Reverse order of priority players : " + players);
    }

    //can adjust number of hairs attacking through numHairs (1 = middle hair, 2 = middle, above, and below, 3 = middle, above, abover, below, belower, 4 = etc) 


    private void HandsAttack()
    {
        Vector3 offset = new Vector3(target.transform.position.x + UnityEngine.Random.Range(-300, 300) / 100.0f, 7,target.transform.position.z + UnityEngine.Random.Range(-110, 110) / 100.0f);

        if (ranged || grouping)
            hands_CD -= 0.5f;
        if (melee)
            hands_CD += 0.5f;
        //ANIMATION STUFF~~~~~~~~~~~~~~~~~ play create hands animation, if one exists 
        //instantiate collider
        Instantiate(handSkill, offset, handSkill.transform.rotation);
    }

    private void getPlayerType()
    {
        range = mel = supp = group = 0;

        foreach (Player p in players)
        {
            range += p.GetRanged() + p.GetOther() / 4;
            mel += p.GetMelee() + p.GetOther() / 2;
            supp += p.GetSupport() + p.GetOther() / 3;
        }

        ranged = melee = support = false;

        if (range != Mathf.Min(range, mel, supp) && range > 25)
            ranged = true;
        if (mel != Mathf.Min(range, mel, supp) && mel > 25)
            melee = true;
        if (supp != Mathf.Min(range, mel, supp) && supp > 25)
            support = true;

        grouping = Grouper.Check();

    }

    /*


    **Ignore this
    **just bouncing off ideas, more in notebooks

    //not sure what these animations are for, so purposing them
    //Ready spell -> spawn summoning circle or Poly (I think that's it's actual purpose)
    //Ready spellcast -> spawn summing circle or Poly while in spell aura mode
    //Spell Aura Idle -> Used for stronger Malady (can use most/all abilities (maybe have a no hair attack version), takes less damage, deals more, subject to change)


    //Use for flinch animation (since bosses shouldn't really flinch) (flash when hit?) :
    //on first meeting, throw rock at malady or something after a bit of dialogue
    //Malady runs through flinch animation, stays at last frame
    //Malady: "....." (time for dots takes longer and is spaced out (first dot, wait 0.8s, second dot, wait 1.3s, third, etc)
    //press enter or whatever to go to next dialogue
    //flinch animation plays backwards (last to first) and Malady smiling again in normal idle
    //some more dialogue


    Dialogue
    encounter = died, came back to fight
    stage = 0;

    //OLD, DUE FOR REVISION TO MAINTAIN LORE AND GAME CONTINUITIES
    Stage 0 - no dialogue (so that it can check all the time)
    Stage 1 - (first encounter) Finally, a new face in this lonely forest
    Stage 2 - (second encounter) I'm not contagious, honest. I'm 
    Stage 3 -  (third encounter) You again? I must be seeing things
    Stage 4 - (fourth encounter) Who's that behind you? Is that spirit messing around in my forest again? //SCRAPPED CHARACTER
    Stage 5 - (fifth encounter) You're really doing a number on my schedule.

    Stage 21 - (first death) Oh, I just remembered why it was so lonely //REVISE?
    Stage 22 - (second death) Looks like you got a lot more than just cooties.
    Stage 23 - (third death)  Ugh, Deja-vu
    Stage 24 - (fourth death) Do you think the ghostbusters reach here?
    Stage 25 - (fifth death) ~Yawn~ (special yawn font animation) I wish _ would visit more often //NO (yawn is a nice touch though)
    Stage 26 - (sixth death)

    Stage 41 - (during fight) 
    //use player name sometimes
    //poly taunt

    Stage 61 - (first kill)
    */
}
