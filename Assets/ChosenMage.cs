using UnityEngine;
using System.Collections;


public class MeteorShower : MonoBehaviour, ISkill
{
    private sMeteor meteor;
    private int price = 0;
    private float cooldown = 20;
    private int numMeteors = 7;
    private float xRange = 7;   //distance from caller to end of meteor shower zone
    private float zRange = 7;
    GameObject nextMeteor;
    //public GameObject projectile;
    public void UseSkill(GameObject caller, GameObject target, float coolDownTimer = 0)
    {
        /*
        float maxX = target.transform.position.x + xRange;
        float minX = target.transform.position.x - xRange;
        float maxZ = target.transform.position.z + zRange;
        float minZ = target.transform.position.z - zRange;

        for (int i = 0; i < numMeteors; i++)
        {
            Vector3 meteorPos = new Vector3(Random.Range(minX, maxX), target.transform.position.y, Random.Range(minZ, maxZ));
            nextMeteor = new GameObject();
            nextMeteor.transform.position = meteorPos;
            meteor.UseSkill(caller, nextMeteor);
        }
        */
    }
    public float GetCoolDownTimer()
    {
        //TODO Temporary value change 
        return 0;
    }
    public int GetPrice()
    {
        //TODO Temporary value change 
        return price;
    }
    public SkillLevel GetSkillLevel()
    {
        //TODO Temporary value change 
        return SkillLevel.EnemyOnly;
    }
}


public class ChosenMage : MonoBehaviour {
    private float lastCast = 0;
    private float meteorShowerCooldown;
    private MeteorShower meteorShower;
	// Use this for initialization
	void Start () {
        meteorShower = new MeteorShower();
        meteorShowerCooldown = meteorShower.GetCoolDownTimer();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > lastCast + meteorShowerCooldown)
        {
            Debug.Log("Casting meteorShower");
            lastCast = Time.time;
            meteorShower.UseSkill(gameObject, gameObject);
        }
	}
}
