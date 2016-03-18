using UnityEngine;
using System.Collections;

public class dealConsistentDamage : MonoBehaviour
{
    public AudioClip hitAudio;
    public float dmgAmount = 1;
    public bool isMagic = false;
    public bool damagesPlayers = false;
    public bool damagesEnemies = false;
    public int flinchPower;
    public float invTime = -1;
    private float invTimer = 0;

    public float GetDamageF()
    {
        return dmgAmount;
    }

    void Update()
    {
        //Debug.Log(dmgAmount);
        invTimer += Time.deltaTime;
    }

    public int GetDamageI()
    {
        return (int)dmgAmount;
    }

    void OnTriggerStay(Collider col)
    {
        if (invTimer >= 0)
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
                            invTimer = -invTime;
                            if (isMagic)
                            {
                                targetDefense.CheckMagicalDefense(dmgAmount, flinchPower, invTime);
                            }
                            else
                            {
                                targetDefense.CheckPhysicalDefense(dmgAmount, flinchPower, invTime);
                            }
                        }
                    }
                }
            }
        }
    }

    public void setDamage(float i)
    {
        dmgAmount = i;
    }
    public float getDamage()
    {
        return dmgAmount;
    }
}




