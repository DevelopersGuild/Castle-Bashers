using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour
{
    private Player player;
    public int defense = 0;
    public int PhysicalDefense = 0;
    public int MagicalDefense = 0;
    private Health health;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        player = gameObject.GetComponent<Player>();
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

    public int GetDefense()
    {
        return defense;
    }

    public void SetDefense(int Defense)
    {
        defense = Defense;
    }

    public void AddDefense(int value)
    {
        defense = defense + value;
    }

    public void Update_Defense()
    {
        SetPhysicalDefense(5*defense+player.GetStrength()+2*player.GetStamina()+player.GetAgility());
        SetMagicalDefense(2 * defense + 6 * player.GetIntelligence());
    }
}
