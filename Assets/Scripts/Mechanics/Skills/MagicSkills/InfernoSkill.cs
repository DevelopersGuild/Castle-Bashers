using UnityEngine;
using System.Collections;


public class Inferno : Skill
{
    private float duration = 7f;
    private float expiration;
    private float spawnInterval = 1.5f;
    private int numSpawned = 0;
    private const int numProjectiles = 6;
    private int numDestroyed = 0;
    private float nextSpawn;
    public float speed = 10;
    bool active;

    private GameObject[] projectiles;
    private GameObject projectile;

    protected override void Start()
    {
        base.Start();
        base.SetBaseValues(15, 16000, 150, "Inferno", SkillLevel.Level1);
        projectile = Resources.Load("Fireball") as GameObject;
        Destroy(projectile.GetComponent<SimpleObjectMover>());
        projectiles = new GameObject[numProjectiles];



    }
    protected override void Update()
    {
        base.Update();
        if (active && Time.time <= expiration)
        {

            if (Time.time >= nextSpawn)
            {
                nextSpawn += spawnInterval;
                projectiles[numSpawned] = Instantiate(projectile, gameObject.transform.position, Quaternion.identity) as GameObject;
                Destroy(projectiles[numSpawned], 5);

                numSpawned++;
            }

        }
        for (int i = 0; i < numSpawned; i++)
        {
            if (projectiles[i])
            {
                projectiles[i].transform.Translate(projectiles[i].transform.forward * Time.deltaTime * speed);
                projectiles[i].transform.Rotate(projectiles[i].transform.up * 35 * Time.deltaTime);
            }
        }

        if (numSpawned >= numProjectiles)
        {
            active = false;
        }

    }
    public override void UseSkill(GameObject caller, GameObject target = null, object optionalParameters = null)
    {
        base.UseSkill(gameObject);
        expiration = Time.time + duration;
        active = true;
        nextSpawn = Time.time;



    }
}