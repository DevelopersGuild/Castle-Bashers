using UnityEngine;
using System.Collections;

public class Skill_TimeStop : Skill
{
    public bool test = false;
    public GameObject layer, player, lay;
    private float timer = 0;
    private float mult = 0.2f;


    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(30, 1000, 25, "Time Acceleration", SkillLevel.Level1);
        base.SetSkillIcon(Resources.Load<Sprite>("Skillicons/timeacceleration"));
    }

    void Awake()
    {
        //player = FindObjectOfType<Camera>().gameObject;
    }

    protected override void Update()
    {
        base.Update();
        if (test)
            UseSkill(this.gameObject);

        if (timer > 0)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0)
            {
                Destroy(lay);
                timer = 0;
                if (augment == Augment.Orange)
                    Time.timeScale = 1;
                else
                    Time.timeScale = 1;
            }
        }
    }


    //Add ripples later
    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target, optionalParameters);
        test = false;
        lay = Instantiate(layer, transform.position, layer.transform.rotation) as GameObject;
        timer = 3f;
        if (augment == Augment.Orange)
        {
            mult = 0.0f;
        }
        else if (augment == Augment.Teal)
        {
            AddHealth(caller);
        }
        else if (augment == Augment.Purple)
        {
            timer = 8f;
        }
        Time.timeScale = Time.timeScale * mult;


    }

    private void modHealth(GameObject caller, bool add = true, float val = 1.4f)
    {
        if (!add)
            val = 1 / val;

        caller.GetComponent<Health>().SetMaxHP(caller.GetComponent<Health>().GetMaxHP() * val);

    }

       private void AddHealth(GameObject caller)
    {
        //heals 1/3 of starting health, which is ~1/4 of temp max hp
        caller.GetComponent<Health>().AddHealth(caller.GetComponent<Health>().GetMaxHP() / 3);
    }
}

