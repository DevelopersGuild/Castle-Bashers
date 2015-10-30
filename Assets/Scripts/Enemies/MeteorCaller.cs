using UnityEngine;
using System.Collections;
using System;

public class sMeteor : MonoBehaviour, ISkill
{
    //public GameObject projectile;
    public void UseSkill(GameObject caller, GameObject target, float coolDownTimer = 0)
    {
        GameObject projectile = Instantiate(Resources.Load("Meteor")) as GameObject;
        projectile.transform.position = target.transform.position + new Vector3(3, 6, 0);

    }
    public float GetCoolDownTimer()
    {
        //TODO Temporary value change 
        return 0;
    }
    public int GetPrice()
    {
        //TODO Temporary value change 
        return 0;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return SkillLevel.EnemyOnly;
    }



}
public class MeteorCaller : MonoBehaviour {
    public float castCooldown = 2;
    private float lastCast;
    public ISkill meteor;
	// Use this for initialization
	void Start () {
        meteor = new sMeteor();
        lastCast = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastCast >= castCooldown && GetComponent<RangedEnemy>().GetTarget() != null) {
            meteor.UseSkill(gameObject, GetComponent<RangedEnemy>().GetTarget());
            lastCast = Time.time;
        }
	}

}
