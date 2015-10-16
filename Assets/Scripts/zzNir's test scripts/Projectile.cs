using UnityEngine;
using System.Collections;

public class Projectile : Skill
{
     public float projectileSpeed;
     public float damageAmount;
     public float TimeToLive;
    public bool destroyOnCollision;

     public void Start()
     {
          Destroy(transform.gameObject, TimeToLive);
     }

     public void Update()
     {
          
     }

     //add rotation too
     public void Shoot(Vector3 dir)
     {
          GetComponent<Rigidbody>().velocity = dir * projectileSpeed;
     }

    public void OnTriggerEnter(Collider other)
    {
        if(destroyOnCollision)
        {
            Destroy(gameObject);
        }
    }

}



