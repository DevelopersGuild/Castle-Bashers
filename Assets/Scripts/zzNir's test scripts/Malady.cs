using UnityEngine;
using System.Collections;

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

    private float clawLim, swarmLim, summonLim, polyLim, teleLim;
    private float claw_CD, swarm_CD, swarm_Duration, hands_CD, summon_CD, polymorph_CD, teleport_CD;
    private float rnd, animationDelay, teleDuration;
    //Maybe no hands_CD, could be a gameObject that creates hands every few seconds while Malady exists
    private bool isTeleporting;
    private Vector3 teleTarget, startPos, center;

    //Center of room, to know where Malady can teleport;
    public GameObject CenterObj;

    public GameObject ClawSkill, SwarmObj, SummonSkill, PolySkill;
    private ISkill sClaw;

    // Use this for initialization
    void Start()
    {
        base.Start();
        sClaw = ClawSkill.GetComponent<ClawAttack>();

        claw_CD = 4 + Random.Range(0, 3);
        clawLim = claw_CD;
        swarm_Duration = 0;
        swarm_CD = 5 + Random.Range(0, 5);
        swarmLim = swarm_CD;
        //only starts counting when duration is over
        hands_CD = 3;
        summon_CD = 5 + Random.Range(0, 5);
        summonLim = summon_CD;
        polymorph_CD = 6 + Random.Range(0, 4);
        polyLim = polymorph_CD;
        teleport_CD = 6 + Random.Range(0, 4);
        teleLim = teleport_CD;
        animationDelay = 2;
        teleDuration = 1;

        isTeleporting = false;
        teleTarget = new Vector3(0, 0, 0);
        startPos = teleTarget;
        center = CenterObj.transform.position;

        moveController.canKnockBack(false, false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isTeleporting)
        {
            transform.position = Vector3.Lerp(startPos, teleTarget, teleDuration * 2);
            teleDuration += Time.deltaTime;
            if(teleDuration >= 0.5f)
            {
                teleDuration = 0;
                isTeleporting = false;
            }
        }
        else
        {
            Act(classification);
        }




        if (invTime <= 0)
        {
            isInvincible = false;
        }


       
        hands_CD += Time.deltaTime;
        animationDelay += Time.deltaTime;
        invTime -= Time.deltaTime;
    }

    public void UseSkill(GameObject caller, GameObject target = null, float coolDownTimer = 0)
    {

    }

    public float GetCoolDownTimer()
    {
        return 0;
    }

    public int GetPrice()
    {
        return 0;
    }

    public SkillLevel GetSkillLevel()
    {
        return 0;
    }

    public override void Act(Type t)
    {
        targetPos = target.transform.position;
        if (hands_CD > 2.5f)
        {
            hands_CD = 0;
            Summon();
        }

        if (animationDelay > 2f)
        {
            animationDelay = 0;
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
            else if (polymorph_CD >= polyLim)
            {
                Polymorph();
            }
            else
            {
                int x = Random.Range(0, 5);
                if (x == 0)
                    Summon();
                else if (x == 1)
                    Polymorph();
                else if (x == 2)
                    Teleport();
                else if (x == 3 && swarm_Duration <= 0)
                    Swarm();
                else if (x == 4 && zDiff < 6)
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
        }

        if (swarm_Duration > 0)
        {
            swarm_Duration -= Time.deltaTime;
        }


    }

    private void Claw()
    {
        sClaw.UseSkill(gameObject);
        claw_CD = 0;
        clawLim = 4 + Random.Range(0, 3);
    }

    private void teleClaw()
    {
        //tele to same z, but have same x and y
        //use claw, tele back


        //animation
        Vector3 tempVec = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        Claw();
        transform.position = tempVec;

    }

    private void Swarm()
    {
        swarm_CD = 0;
        swarmLim = 5 + Random.Range(0, 5);
        swarm_Duration = 8;
        //play animation
        Instantiate(SwarmObj, transform.position, transform.rotation);
    }

    private void Summon()
    {
        summon_CD = 0;
        summonLim = 5 + Random.Range(0, 5);
        float dir = Mathf.Sign(center.x - transform.position.x);

        //slightly wierd due to having a scale of 10, would be ok after we have actual stuff
        Vector3 summonPos = transform.position + new Vector3(Random.Range(5, 9) * dir, SummonSkill.transform.position.y, (Random.Range(10, 20) / 4.0f) * (target.transform.position.z - transform.position.z));
        Instantiate(SummonSkill, summonPos/10, SummonSkill.transform.rotation);
    }

    private void Teleport()
    {
        teleLim = Random.Range(0, 4) + 6;

        isTeleporting = true;
        float dir = Mathf.Sign(center.x - transform.position.x);

        startPos = transform.position;
        teleTarget = transform.position + new Vector3(Random.Range(9,13) * dir, 0, (Random.Range(5, 15) / 10.0f) * (target.transform.position.z - transform.position.z));

        teleport_CD = 0;
        claw_CD += 1;
        swarm_CD += 1;
        summon_CD += 1;
        polymorph_CD += 1;
        teleport_CD += 1;

        teleDuration = 0;
    }

    private void Polymorph()
    {
        polymorph_CD = 0;
        polyLim = 6 + Random.Range(0, 4);
        Vector3 polyPos = new Vector3(target.transform.position.x, 6, transform.position.z);
        Instantiate(PolySkill, polyPos, transform.rotation);
    }

    public float getDirection()
    {
        return Mathf.Sign(transform.localScale.x);
    }
}
