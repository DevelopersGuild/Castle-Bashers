using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {
    public AudioClip hitAudio;
    public float dmgAmount = 1;
    public bool isMagic = false;
    public bool damagesPlayers = false;
    public bool damagesEnemies = false;
    public int flinchPower;

    public float GetDamageF()
    {
        return dmgAmount;
    }

    void Update()
    {
        //Debug.Log(dmgAmount);
    }

    public int GetDamageI()
    {
        return (int)dmgAmount;
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject target = col.gameObject;
        Health targetHealth = target.GetComponent<Health>();
        Defense targetDefense = target.GetComponent<Defense>();

        if (targetHealth)
        {
            // Check for whether the target is an enemy or a player
            if (!targetHealth.getInvincibility())
            {
                if (damagesPlayers)
                {
                    if (target.GetComponent<Player>())
                    {
                        if (isMagic)
                        {
                            targetDefense.CheckMagicalDefense(dmgAmount, flinchPower);
                        }
                        else
                        {
                            targetDefense.CheckPhysicalDefense(dmgAmount, flinchPower);
                        }
                    }
                }
                if (damagesEnemies)
                {
                    if (target.GetComponent<Enemy>())
                    {
                        if (isMagic)
                        {
                            targetDefense.CheckMagicalDefense(dmgAmount, flinchPower);
                        }
                        else
                        {
                            targetDefense.CheckPhysicalDefense(dmgAmount, flinchPower);
                        }
                    }
                }
                if (target.CompareTag("Destructible"))
                {
                    targetHealth.takeDamage(dmgAmount);
                    //Debug.Log(target.GetComponent<Health>().getCurrentHp());
                }
            }
        }

    }

    //void OnCollisionEnter(Collision col)
    //{
    //    GameObject target = col.gameObject;
    //    Health targetHealth = target.GetComponent<Health>();
    //    Defense targetDefense = target.GetComponent<Defense>();

    //    if (targetHealth)
    //    {
    //        if (damagesPlayers)
    //        {
    //            if (target.GetComponent<Player>())
    //            {
    //                if (isMagic)
    //                {
    //                    targetDefense.CheckMagicalDefense(dmgAmount);
    //                }
    //                else
    //                {
    //                    targetDefense.CheckPhysicalDefense(dmgAmount);
    //                }
    //                target.GetComponent<Player>().SetInvTime(1.5f);
    //                Debug.Log("asdasda");

    //            }

    //        }
    //        if (damagesEnemies)
    //        {
    //            if (target.GetComponent<Enemy>())
    //            {
    //                if (isMagic)
    //                {
    //                    Debug.Log("enemy took magic");
    //                    targetDefense.CheckMagicalDefense(dmgAmount);
    //                }
    //                else
    //                {
    //                    Debug.Log("Enemy took physical");
    //                    targetDefense.CheckPhysicalDefense(dmgAmount);
    //                }
    //                target.GetComponent<Enemy>().setInvTime(0.1f);


    //            }
    //        }
    //        if (target.CompareTag("Destructible"))
    //        {
    //            targetHealth.takeDamage(dmgAmount);
    //            //Debug.Log(target.GetComponent<Health>().getCurrentHp());
    //        }
    //    }
    //}

    public void setDamage(float i)
    {
        dmgAmount = i;
    }
    public float getDamage()
    {
        return dmgAmount;
    }
}
