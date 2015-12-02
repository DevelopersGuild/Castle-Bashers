using UnityEngine;
using System.Collections;

public class Immolate : Skill
{
    private Player player;
    private Color tintColor;
    private float expiration;
    private float duration = 10;
    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(60, 16000, 150, "Immolate", SkillLevel.Level1);
        player = GetComponent<Player>();
        tintColor = new Color(75, 0, 0);
    }
    protected override void Update()
    {
        base.Update();

    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        gameObject.AddComponent<Burn>();
        GetComponent<Burn>().setDuration(duration);
        player.GetComponent<SpriteRenderer>().color += tintColor;
        player.GetComponent<Player>().AddStuff
        expiration = Time.time + duration;
    }
}




public class skilltesting : MonoBehaviour {

    float castInterval;
    
    private Skill testSkill;
    

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<PuritySkill>();

        testSkill = GetComponent<Skill>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time >= 1) 
	    if(testSkill.GetCoolDownTimer() <= 0)
        {
            testSkill.UseSkill(gameObject);

        }
	}
}
