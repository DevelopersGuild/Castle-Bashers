using UnityEngine;
using System.Collections;

public class test : Skill
{
    private float duration = 10f;
    private float expiration;
    bool active;
    Player player;

    private int bonusStrength;
    protected override void Start()
    {
        base.Start();
   
        base.SetBaseValues(15, 16000, 150, "Ignite", SkillLevel.Level1);
        player = GetComponent<Player>();

    }
    protected override void Update()
    {
        base.Update();
        if(active && Time.time >= expiration)
        {
            active = false;
            player.transform.GetChild(0).GetComponent<DealDamage>().isMagic = false;
            player.SetStrength(player.GetStrength() - bonusStrength);

        }

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        expiration = Time.time + duration;
        active = true;
        player.transform.GetChild(0).GetComponent<DealDamage>().isMagic = true;
        bonusStrength = player.GetIntelligence() / 2;
        player.SetStrength(player.GetStrength() + bonusStrength);


    }
}

public class skilltesting : MonoBehaviour {

    float castInterval;
    
    private Skill testSkill;
    

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
        gameObject.AddComponent<FireBlast>();
=======
        gameObject.AddComponent<FireballSkill>();
>>>>>>> c574f937d0867388a4cf76c2ff37f058b895a52d
        testSkill = GetComponent<Skill>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(testSkill.GetCoolDownTimer() <= 0)
        {
            testSkill.UseSkill(gameObject);

        }
	}
}
