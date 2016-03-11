using UnityEngine;
using System.Collections;

public class Projectile : SkillOld
{
    public float projectileSpeed;
    public bool destroyOnCollision;
    public GameObject destroyedParticle;
    public AudioClip destroyedSound;
    public float TimeToLive;
    private DealDamage dealDamage;

    public void Start()
    {
        dealDamage = GetComponent<DealDamage>();
        Destroy(transform.gameObject, TimeToLive);
    }

    //add rotation too
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().velocity = dir * projectileSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (((other.GetComponent<Player>() && dealDamage.damagesPlayers) || (other.GetComponent<Enemy>() && dealDamage.damagesEnemies)) && destroyOnCollision)
        {
            if (destroyedParticle)
            {
                GameObject particle = (GameObject)Instantiate(destroyedParticle, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            }
            if (destroyedSound)
            {
                AudioSource.PlayClipAtPoint(destroyedSound, transform.position, 1);
            }
            Destroy(gameObject);
        }
    }

}



