using UnityEngine;
using System.Collections;
using System;



public class MeteorCaller : MonoBehaviour {
    public float castCooldown = 2;
    private float lastCast;
    public ISkill meteor;
	// Use this for initialization
	void Start () {
        meteor = gameObject.AddComponent<sMeteor>();
       
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
