using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningPortal : Skill
{

    public List<Enemy> Summons = new List<Enemy>();
    private Vector3 offset;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = null;

    }

    // Update is called once per frame
    void Update()
    {

    }


        //objects would actually be empty objects that show the spawning animations, then spawn the enemy
        
        //delay to show it exists, in actual game destroy after enemy animation ends
        //use basic functions to destroy self
        //maybe keep this, just get correct time for spawn animation
        //Destroy(gameObject, 2f);
        //Assign the value of coolDownTimer to the coolDown varible so we can check the cooldown.
    

    public void setTarget(GameObject p)
    {
        player = p;
    }

    public void spawn()
    {
        offset = new Vector3(0, 2, 0);

        foreach (Enemy obj in Summons)
        {
            //play animation for enemy spawn (crawls out of portal or whatever)
            //if no animation exists, I guess just stun enemy, disable it's collider and slowly move it up until it is standing on the floor, then enable it
            //end of animation, spawn enemy (in the animation frame it calls a method that spawns the enemy, this is hear for temp purposes)
            if (player != null)
                obj.SetTarget(player);

            Instantiate(obj, transform.position, obj.transform.rotation);
        }
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
