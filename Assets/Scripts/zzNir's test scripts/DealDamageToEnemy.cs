using UnityEngine;
using System.Collections;

public class DealDamageToEnemy : MonoBehaviour
{
    //public int atk;
    public float PhysicalDamage = 1;
    public float MagicalDamage = 1;
    private float PhysicalChange = 0;
    private float MagicalChange = 0;
    private float CriticalChance = 0;
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
                //get amt (1), PhysicalDamage(1) from Enemy/Hazard
                //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                //hp.takeDamage(PhysicalDamage);
                //Add Critical Attack
                if(Check_Critical_Success())
                {
                    hp.takeDamage(Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange)*1.5f);
                }
                else
                {
                    hp.takeDamage(Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange));
                }
                
                enem.setInvTime(invTime);
                if (enemObj.GetComponent<Boss>())
                {
                    Boss b = enemObj.GetComponent<Boss>();
                    b.addDamage((int)GetComponent<ID>().getID(), Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange));
                }
            }


            if (GetComponent<Projectile>())
            {
                if (tag == "DProj")
                    Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Destructible"))
        {
            enemObj.GetComponent<EnemyHealth>().takeDamage(PhysicalDamage);
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
                    //get amt (1), PhysicalDamage(1) from Enemy/Hazard
                    //hp.findKnockback(other, transform.position, 1, 1, knockOverride);
                    //hp.takeDamage(PhysicalDamage);
                    //Add Critical Attack
                    if (Check_Critical_Success())
                    {
                        hp.takeDamage(Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange) * 1.5f);
                    }
                    else
                    {
                        hp.takeDamage(Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange));
                    }
                    enem.setInvTime(invTime);
                    if (enemObj.GetComponent<Boss>())
                    {
                        Boss b = enemObj.GetComponent<Boss>();
                        b.addDamage((int)GetComponent<ID>().getID(), Random.Range(PhysicalDamage, PhysicalDamage + PhysicalChange));
                    }
                }

                if (GetComponent<Projectile>())
                {
                    if (tag == "DProj")
                        Destroy(gameObject);
                }
            }

        if (other.gameObject.CompareTag("Destructible"))
        {
            enemObj.GetComponent<EnemyHealth>().takeDamage(PhysicalDamage);
            Debug.Log(enemObj.GetComponent<EnemyHealth>().getCurrentHp());
        }

    }

    public void UpdateDamage(float physicaldamage,float magicaldamage)
    {
        PhysicalDamage = physicaldamage;
        MagicalDamage = magicaldamage;
    }

    public void UpdateChange(float physicalchange,float magicalchange)
    {
        PhysicalChange = physicalchange;
        MagicalChange = magicalchange;
    }

    public void SetCriticalChance(float value)
    {
        CriticalChance = value;
    }

    public float GetCriticalChance()
    {
        return CriticalChance;
    }

    public float GetPhysicalAttackLeftRange()
    {
        return PhysicalDamage;
    }

    public float GetPhysicalAttackRightRange()
    {
        return PhysicalDamage + PhysicalChange;
    }

    public float GetMagicalAttackLeftRange()
    {
        return MagicalDamage;
    }

    public float GetMagicalAttackRightRange()
    {
        return MagicalDamage + MagicalChange;
    }

    bool Check_Critical_Success()
    {
        //No Critical Chance
        if (CriticalChance == 0)
            return false;
        //Must Critical
        if (CriticalChance >= 1)
            return true;
        //Has Critical Chance
        float check = Random.Range(1.0f, 1.0f / CriticalChance);
        if (Mathf.Abs(check - 0.5f * (1.0f / CriticalChance))<=0.1f)
            return true;
        else
            return false;
    }
}
