using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningPortal : Skill
{

    public List<Enemy> Summons = new List<Enemy>();
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        offset = new Vector3(0, 2, 0);
        //objects would actually be empty objects that show the spawning animations, then spawn the enemy
        foreach (Enemy obj in Summons)
        {
            //add some randomness to spawn
            //play animation for each one
            //end of animation, spawn enemy (in the animation frame it calls a method that spawns the enemy, this is hear for temp purposes)
            Instantiate(obj, transform.position + offset, obj.transform.rotation);
        }
        //delay to show it exists, in actual game destroy after enemy animation ends
        Destroy(gameObject, 2f);
        //Assign the value of coolDownTimer to the coolDown varible so we can check the cooldown.
    }
}
