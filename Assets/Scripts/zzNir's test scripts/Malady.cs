using UnityEngine;
using System.Collections;
using System;

public class Malady : Boss
{

    //Malady Boss, a sort of disease witch thing
    //Maybe has a fan behind her head to make her hair fly
    //Skills:
    //Field Effect  - Windy + leaves flying around, no real effect on fight. Just for visuals
    //Claw attack   - relatively low cd, nails extend to deal damage and causing the player to be weakened, taking extra damage in the next 4 seconds.
    //Release swarm - Swarm of flies come out of mouth, go after the player for 8 seconds, then come back to Malady
    //              - Swarm cannot be attacked (maybe on attack backs away for a second then keeps attacking)
    //              - Swarm either : adds direction instead of settings (easier to dodge, slower) or goes in a line back and forth, like a continious boomerang( easy to dodge too)
    //              - Poisons player, dealing damage over time and slowing.
    //Summon Hands  - Rotten hands come out of the ground to (slow/stun?) and damage the player
    //Summon Dudes  - Rotten Carcases climb out of the ground and chase the enemy. Their attacks slow the player
    //              - either on death or after 5s, explode dealing damage and poisoning. Also leave a poisoned area where they exploded
    //Polymorph     - Creates a cloud over all players. After 1.5s, cloud zaps those below, turning them into frogs (unable to attack) and slowing them by 90% for 3s. Cloud does not follow player
    //Teleport      - Every 8-11s, transforms into a swarm and moves to a different location on the map. Random, but at least 1/3 map away.
    //              - Reduces other cooldowns by half
    //More? beetles attack, beetles spawn below Malady, run wildly and pop leaving small poisoned areas.

    //On death, swarm flies out of mouth if not in existence, Flies at Malady and eats her, flies away leaving bones to fall on the ground

    private float clawLim, swarmLim, summonLim, polyLim, teleLim, teleClawLim, hairGLim, hairALim;
    private float claw_CD, swarm_CD, swarm_Duration, hands_CD, summon_CD, polymorph_CD, teleport_CD, teleClaw_CD, hairG_CD, hairA_CD, refreshPriority;
    private float rnd, animationDelay, teleDuration;
    //Maybe no hands_CD, could be a gameObject that creates hands every few seconds while Malady exists
    private bool isTeleporting;
    private Vector3 teleTarget, startPos, center;

    //Center of room, to know where Malady can teleport;
    public GameObject CenterObj;

    public GameObject ClawSkill, SwarmObj, SummonSkill, PolySkill;
    public GroupingManager Grouper;
    private ISkill sClaw;

    private float offset1, offset2, offset3, one, two, three, four, tempThreat, tempDamage, temp1, temp2;
    private int size;
    private PlayerManager playerM;
    private bool ranged, grouping, melee, support, refresh;
    private float range, group, mel, supp;

    // Use this for initialization
    void Start()
    {
        base.Start();
        sClaw = ClawSkill.GetComponent<ClawAttack>();

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
        polymorph_CD = 6 + UnityEngine.Random.Range(0, 4);
        polyLim = polymorph_CD;
        teleport_CD = 6 + UnityEngine.Random.Range(0, 4);
        teleLim = teleport_CD;
        hairA_CD = 8 + UnityEngine.Random.Range(0, 5);
        hairALim = hairA_CD;
        hairG_CD = 4 + UnityEngine.Random.Range(0, 7);
        hairGLim = hairG_CD;
        animationDelay = 2;
        teleDuration = 1;
        refreshPriority = 10;

        refresh = false;

        isTeleporting = false;
        teleTarget = new Vector3(0, 0, 0);
        startPos = teleTarget;
        center = CenterObj.transform.position;

        moveController.canKnockBack(false, false);

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
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (refresh)
        {
            Debug.Log(ranged + " " + grouping + " " + melee + " " + support);
            refresh = false;
        }
        if (isTeleporting)
        {
            transform.position = Vector3.Lerp(startPos, teleTarget, teleDuration * 2);
            teleDuration += Time.deltaTime;
            if (teleDuration >= 0.5f)
            {
                teleDuration = 0;
                isTeleporting = false;
            }
        }
        else
        {
            Act(classification);
        }

        if (refreshPriority >= 10)
        {
            sortPriority();
            refreshPriority = 0;
        }


        if (invTime <= 0)
        {
            isInvincible = false;
        }

  

        refreshPriority += Time.deltaTime;
        hands_CD += Time.deltaTime;
        animationDelay += Time.deltaTime;
        invTime -= Time.deltaTime;
    }

    public override void Act(Type t)
    {
        targetPos = target.transform.position;
        if (hands_CD > 2.5f)
        {
            hands_CD = 0;
            //Debug.Log("Hands " + Time.time); //HandsAttack();
        }
        if (animationDelay > 1f + hp.GetCurrentHealth()/hp.GetMaxHP())
        {
            //getPlayerType();
            float randNum = UnityEngine.Random.Range(1, 100);
            if (randNum < 5 - 5 * (3 - size))
            {
                target = players[3].gameObject;
            }
            else if (randNum < 20 - 10 * (3 - size))
            {
                target = players[2].gameObject;
            }
            else if (randNum < 45 - 15 * (3 - size))
            {
                target = players[1].gameObject;
            }
            else
            {
                target = players[0].gameObject;
            }

            animationDelay = 0;
            if (claw_CD >= clawLim)
            {
                Claw();
            }
            else if (hairG_CD >= hairGLim)
            {
                HairGrab();
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
            else if(hairA_CD >= hairALim)
            {
                HairAttack();
            }
            else if (polymorph_CD >= polyLim)
            {
                Polymorph();
            }
            else
            {
                int x = UnityEngine.Random.Range(0, 5);
                if (x == 0)
                    Summon();
                else if (x == 1)
                    Polymorph();
                else if (x == 2)
                    Teleport();
                else if (x == 3 && swarm_Duration <= 0)
                    Swarm();
                else if (x == 4 && zDiff < 4)
                    Claw();
                else
                    teleClaw();


                //animationDelay = 1;
            }
            claw_CD += 1;
            swarm_CD += 1;
            summon_CD += 1;
            polymorph_CD += 1;
            teleport_CD += 1;
            hairG_CD += 1;
            hairA_CD += 1;
        }

        if (swarm_Duration > 0)
        {
            swarm_Duration -= Time.deltaTime;
        }


    }

    private void Claw()
    {
        claw_CD = 0;
        clawLim = 4 + UnityEngine.Random.Range(0, 3);
        distance = target.transform.position.x - transform.position.x;

        if(ranged || support)
        {
            clawLim -= 1;
            teleClaw();
        }
        else if(melee)
        {
            if (zDiff < 4 && distance < 3)
            {
                clawLim -= 1;
                Debug.Log("Claw " + Time.time); //sClaw.UseSkill(gameObject);
            }
            else
            {
                teleClaw();
            }
        }
        else if(grouping)
        {
            if (zDiff < 4 && distance < 3)
            {
                clawLim -= 2;
                Debug.Log("Claw " + Time.time); //sClaw.UseSkill(gameObject);
            }
            else
            {
                teleClaw();
            }
        }
        else
        {
            if (zDiff < 4 && distance < 3)
                Debug.Log("Claw " + Time.time); //sClaw.UseSkill(gameObject);
            else
                teleClaw();
        }
        //if ranged/support, tele claw
        //if grouping, if close claw if far tele claw
        //if melee, claw
    }

    private void teleClaw()
    {
        //tele to same z, close x, orig y
        //use claw, tele back


        //animation
        Vector3 tempVec = transform.position;
        float f = (UnityEngine.Random.Range(100, 400) /100.0f)* ((UnityEngine.Random.Range(0, 2) - 0.5f)* 2);
        //transform.position = new Vector3(target.transform.position.x + f, transform.position.y, target.transform.position.z);
        Debug.Log("TeleClaw " + Time.time); //sClaw.UseSkill(gameObject);
        //transform.position = tempVec;

    }

    private void Swarm()
    {
        swarm_CD = 0;
        swarmLim = 11 + UnityEngine.Random.Range(0, 5);
        Debug.Log("Swarm " + Time.time); //sClaw.UseSkill(gameObject);//SwarmBehaviour swarm = Instantiate(SwarmObj, transform.position, transform.rotation) as SwarmBehaviour;
        //swarm_Duration = swarm.Duration;
        swarm_Duration = 8;
        //swarm.setTarget(target);

        if (ranged && support && !grouping)
            swarmLim += 1;
        else if(ranged || support || grouping)
            swarmLim -= 2;
        if(melee)
            swarmLim -= 1;
        //play animation
    }

    private void Summon()
    {
        summon_CD = 0;
        summonLim = 7 + UnityEngine.Random.Range(0, 6);
        float dir = Mathf.Sign(center.x - transform.position.x);

        if(grouping)
            summonLim -= 3;
        if(ranged || support)
            summonLim -= 1;
        if(melee && !grouping)
            summonLim += 1;

        //slightly wierd due to having a scale of 10, would be ok after we have actual stuff
        Vector3 summonPos = transform.position + new Vector3(UnityEngine.Random.Range(5, 9) * dir, SummonSkill.transform.position.y, (UnityEngine.Random.Range(10, 20) / 4.0f) * (target.transform.position.z - transform.position.z));
        Debug.Log("Summon " + Time.time); //sClaw.UseSkill(gameObject);SummoningPortal sp = Instantiate(SummonSkill, summonPos / 10, SummonSkill.transform.rotation) as SummoningPortal;
        //sp.setTarget(target);
    }

    private void Teleport()
    {
        teleLim = UnityEngine.Random.Range(0, 5) + 6;
        Debug.Log("Teleport " + Time.time);  //isTeleporting = true;
        float f = 0;
        foreach(Player p in players)
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
        polymorph_CD += 1;
        teleport_CD += 1;
        hairG_CD += 1;
        hairA_CD += 1;

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

    private void Polymorph()
    {
        polymorph_CD = 0;
        polyLim = 9 + UnityEngine.Random.Range(0, 5);

        if (melee && support)
            polyLim += 4;
        else if (ranged && grouping)
            polyLim -= 2;
        else if (ranged || support)
            polyLim -= 1;
        else if (melee)
            polyLim += 2;

        Vector3 polyPos = new Vector3(target.transform.position.x, 6, transform.position.z);
        Debug.Log("Poly " + Time.time);  //Instantiate(PolySkill, polyPos, transform.rotation);
    }

    public float getDirection()
    {
        return Mathf.Sign(transform.localScale.x);
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
        Debug.Log("Reverse order of priority players : " + players);
    }

    private void HairAttack()
    {
        Debug.Log("HairAttack " + Time.time);
        hairA_CD = 0;
        hairALim = 8 + UnityEngine.Random.Range(0, 5);
        if (melee || grouping)
            hairALim -= 2;
        else if (ranged)
            hairALim -= 1;
        if (support)
            hairALim += 1;

    }
    
    private void HairGrab()
    {
        hairG_CD = 0;
        hairGLim = 4 + UnityEngine.Random.Range(0, 7);
        //low cooldown (2 attacks)
        //ranged grab can only target someone ___+ dist away
        //ranged grab has high hp steal (you can hit her and she can't move, so it's ok)
        //ranged grab collider turned on 1/3rd of distance to target so it won't collide with melee guys (closed fist -> open hand?)
        if (grouping)
        {
            if(ranged && support)
            {
                hairGLim -= 2;
                Debug.Log("Ranged || Throw grab " + Time.time); //ranged grab || grab closest and throw (rand chance)
            }
            else if(ranged)
            {
                hairGLim -= 1;
                Debug.Log("Ranged || Throw grab " + Time.time); //ranged grab || grab closest and throw (rand chance)
            }
            else if(support)
            {
                hairGLim += 1;
                Debug.Log("tp > Throw grab " + Time.time); //tp grab if someone is far away, either throw or cc, else grab closest and throw
            }
            else if(melee)
            {
                hairGLim += 4;
                Debug.Log("tp > Throw grab " + Time.time); //tp grab if someone is far away, either throw or cc, else grab closest and throw
            }
        }
        else
        {
            if(ranged && support)
            {
                hairGLim -= 2;
                Debug.Log("Ranged || tp grab " + Time.time); //tp grab || ranged grab
            }
            else if(melee && !(support || ranged))
            {
                hairGLim += 3;
                Debug.Log("tp > Throw grab " + Time.time); //tp grab if someone is far away, else grab closest and throw
            }
            else if(support && !ranged)
            {
                hairGLim += 1;
                Debug.Log("tp > Throw grab " + Time.time); //tp grab if someone is far away, else grab closest and throw
            }
            else if(ranged)
            {
                hairGLim -= 3;
                Debug.Log("Ranged || tp grab " + Time.time); //ranged grab or tp grab
            }
            else
            {
                Debug.Log("Throw grab " + Time.time);
            }
        }


        //throw? aoe (cc + damage) ranged || support || grouping
        //when melee, if someone far away tp to them and throw/hold
        //hold? single (cc + damage) ranged || support
        //throw also done when top half priority has either ranged or support
    }

    private void HandsAttack()
    {
        if (ranged || grouping)
            hands_CD -= 0.5f;
        if (melee)
            hands_CD += 0.5f;
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
    Dialogue
    encounter = died, came back to fight
    stage = 0;

    Stage 0 - no dialogue (so that it can check all the time)
    Stage 1 - (first encounter) Finally, a new face in this lonely forest
    Stage 2 - (second encounter) I'm not contagious, honest. I'm 
    Stage 3 -  (third encounter) You again? I must be seeing things
    Stage 4 - (fourth encounter) Who's that behind you? Is that spirit messing around in my forest again?
    Stage 5 - (fifth encounter) You're really doing a number on my schedule.

    Stage 21 - (first death) Oh, I just remembered why it was so lonely
    Stage 22 - (second death) Looks like you got a lot more than just cooties.
    Stage 23 - (third death)  Ugh, Deja-vu
    Stage 24 - (fourth death) Do you think the ghostbusters reach here?
    Stage 25 - (fifth death) ~Yawn~ (special yawn font animation) I wish Magrat would visit more often
    Stage 26 - (sixth death)

    Stage 41 - (during fight) 
    //use player name sometimes
    //poly taunt

    Stage 61 - (first kill)
    */
}
