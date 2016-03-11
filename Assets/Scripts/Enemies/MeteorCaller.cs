using UnityEngine;
using System.Collections;
using System;

public class MeteorCaller : MonoBehaviour
{
    public float castCooldown = 3;
    private float lastCast;
    public Skill meteor;
    private MoveController movecontroller;
    // Use this for initialization
    void Start()
    {
        meteor = gameObject.AddComponent<sMeteor>();
        lastCast = Time.time;
        movecontroller = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movecontroller.GetFlinched()) return;

        if (Time.time - lastCast < castCooldown || GetComponent<RangedEnemy>().GetTarget() == null) return;
        meteor.UseSkill(gameObject, GetComponent<RangedEnemy>().GetTarget());
        lastCast = Time.time;
    }

}
