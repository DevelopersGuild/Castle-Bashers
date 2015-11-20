using UnityEngine;
using System.Collections;

public class MeteorShowerSkill : Skill {


    private float nextMeteorTime;
    private float meteorInterval = 0.5f;
    private Skill meteor;
    GameObject target;
    private float xRange = 7;   //distance from target to end of meteor shower zone
    private float zRange = 7;
    private float duration = 7;
    private float endTime;
    

    // Use this for initialization
    protected override void Start () {
        base.Start();
        base.SetBaseValues(15, 16000, 85, "Meteor Shower", SkillLevel.EnemyOnly);
        meteor = gameObject.AddComponent<sMeteor>();

	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (Time.time >= nextMeteorTime)
        {
            nextMeteorTime += meteorInterval;
            if (target)
            {

                spawnRandomMeteor(target.transform);
            }
            else
            {
                spawnRandomMeteor(gameObject.transform);
            }
        }
        if(Time.time > endTime)
        {
            nextMeteorTime = float.MaxValue;
        }
    }

    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        Debug.Log("Using meteorshower skill!");
        base.UseSkill(caller, target, optionalParameters);

        this.target = target;
        nextMeteorTime = Time.time + meteorInterval;
        endTime = Time.time + duration;

    }

    void spawnRandomMeteor(Transform targetPos)
    {
        float maxX = targetPos.position.x + xRange;
        float minX = targetPos.position.x - xRange;
        float maxZ = targetPos.position.z + zRange;
        float minZ = targetPos.position.z - zRange;
        float minY = targetPos.position.y;
        Vector3 meteorPos = new Vector3(Random.Range(minX, maxX), minY, Random.Range(minZ, maxZ));
        GameObject newMeteor = new GameObject();
        newMeteor.transform.position = meteorPos;
        meteor.UseSkill(gameObject, newMeteor);
        Destroy(newMeteor);
    }
}
