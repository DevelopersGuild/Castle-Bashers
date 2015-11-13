using UnityEngine;
using System.Collections;

public class MultiShot : Skill {
    GameObject arrow;
    public int numArrows = 5;
    //Ideally, this would be half the int in degrees above the target and half below
    //However, I'm not very good at physics so this is going to require me to come back
    //And fix the code later on. For now, you have to just experiment with the number.
    //The issue comes with modifying the X directly. An ideal fix would probably be
    //To use transform.forward and rotate. 
    public int spreadSize = 60;
                                   
	// Use this for initialization
	protected override void Start () {
        base.Start();
        SetBaseValues(7, 3500, 35, "Multi Shot", SkillLevel.EnemyOnly);
	}
	
	// Update is called once per frame
    
    public override void UseSkill(GameObject caller, GameObject target = null, System.Object optionalParameters = null)
    {
        base.UseSkill(caller, target);
        Debug.Log("Using skill!");
        int startOfArrowSpread = spreadSize / 2;
        int spreadDecrementAmount = spreadSize / numArrows;
        for(int i = 0; i < numArrows; i++)
        {
            arrow = Instantiate(Resources.Load("Arrow")) as GameObject;
            arrow.transform.position = caller.transform.position;

            float forceX = -1;
            if (target.transform.position.x > target.transform.position.x) {
                forceX = 1;
            }

            //Should probablyy figure out why this needs to be multiplied by such a high number to work
            forceX *= arrow.GetComponent<Projectile>().projectileSpeed * 1.5f; // updating it to projectile's speed
            float forceY = 0;
            float forceZ = startOfArrowSpread - (spreadDecrementAmount* i);
            Vector3 force = new Vector3(forceX, forceY, forceZ);
            arrow.GetComponent<Rigidbody>().velocity = force;


            //arrow.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
