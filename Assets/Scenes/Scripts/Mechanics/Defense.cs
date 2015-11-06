using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour
{
    public int PhysicalDefense = 0;
    public int MagicalDefense = 0;
    private Health health;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    public void CheckPhysicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        damage = damage - PhysicalDefense;
        if (damage > 0)
        {
            health.takeDamage(damage, knockback, flinch);
        }
    }

    public void CheckMagicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        damage = damage - MagicalDefense;
        if(damage > 0)
        {
            health.takeDamage(damage, knockback, flinch);
        } 
    }

    public void SetPhysicalDefense(int value)
    {
        PhysicalDefense = value;
    }
    public int GetPhysicalDefense()
    {
        return PhysicalDefense;
    }

    public void SetMagicalDefense(int value)
    {
        MagicalDefense = value;
    }
    public int GetMagicalDefense()
    {
        return MagicalDefense;
    }
}
