using UnityEngine;
using System.Collections;

public class DealDamageToEnemy : MonoBehaviour
{
    public int atk;
    public float dmgAmount = 1;
    public float invTime = 0.1f;
    //If we make colliders appear on attacks, create OnCollisionEnter and OnTriggerEnter collisions
    //destroy collider after they hit something

    public void OnCollisionStay(Collision other)
    {
        GameObject enemObj = other.gameObject;
        //Check for player collision
        if (other.gameObject.GetComponent<Enemy>())
        {
            //Find components necessary to take damage and knockback

            Enemy enem = enemObj.GetComponent<Enemy>();
            Health hp = enemObj.GetComponent<Health>();
            //Take damage if the player isnt already currently invincible
            if (!enem.GetInvincible())
            {
                //Deal damage, knockback
                //get amt (1), dmgAmount(1) from Enemy/Hazard
                //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                hp.takeDamage(dmgAmount);
                enem.setInvTime(invTime);
            }

            if (GetComponent<Projectile>())
            {
                if (tag == "DProj")
                    Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Destructible"))
        {
            enemObj.GetComponent<EnemyHealth>().takeDamage(dmgAmount);
            Debug.Log(enemObj.GetComponent<EnemyHealth>().getCurrentHp());
        }

    }

    //Same code just make sure it happens
    public void OnTriggerEnter(Collider other)
    {

        GameObject enemObj = other.gameObject;
        //Check for player collision
        if (other.gameObject.GetComponent<Enemy>())
            //Check for player collision
            if (other.gameObject.GetComponent<Enemy>())
            {
                //Find components necessary to take damage and knockback

                Enemy enem = enemObj.GetComponent<Enemy>();
                Health hp = enemObj.GetComponent<Health>();
                //Take damage if the player isnt already currently invincible
                if (!enem.GetInvincible())
                {
                    //Deal damage, knockback
                    //get amt (1), dmgAmount(1) from Enemy/Hazard
                    //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                    hp.takeDamage(dmgAmount);
                    enem.setInvTime(invTime);
                }

                if (GetComponent<Projectile>())
                {
                    if (tag == "DProj")
                        Destroy(gameObject);
                }
            }

        if (other.gameObject.CompareTag("Destructible"))
        {
            enemObj.GetComponent<EnemyHealth>().takeDamage(dmgAmount);
            Debug.Log(enemObj.GetComponent<EnemyHealth>().getCurrentHp());
        }

    }
}
