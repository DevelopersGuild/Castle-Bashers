﻿using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour
{
    private Player player;
    public float defense = 0;
    public float PhysicalDefense = 0;
    public float MagicalDefense = 0;
    private Health health;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        player = gameObject.GetComponent<Player>();
    }

    public void CheckPhysicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        if (Check_Block_Success())
            damage = damage * 0.35f;
        damage = damage - PhysicalDefense;
        if (damage > 0)
        {
            health.takeDamage(damage, knockback, flinch);
        }
    }

    public void CheckMagicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        if (Check_Block_Success())
            damage = damage * 0.5f;
        damage = damage - MagicalDefense;
        if(damage > 0)
        {
            health.takeDamage(damage, knockback, flinch);
        } 
    }

    public void SetPhysicalDefense(float value)
    {
        PhysicalDefense = value;
    }
    public float GetPhysicalDefense()
    {
        return PhysicalDefense;
    }

    public void SetMagicalDefense(float value)
    {
        MagicalDefense = value;
    }
    public float GetMagicalDefense()
    {
        return MagicalDefense;
    }

    public float GetDefense()
    {
        return defense;
    }

    public void SetDefense(float Defense)
    {
        defense = Defense;
    }

    public void AddDefense(float value)
    {
        defense = defense + value;
    }

    public void Update_Defense()
    {
        SetPhysicalDefense(5*defense+player.GetStrength()+2*player.GetStamina()+player.GetAgility());
        SetMagicalDefense(2 * defense + 6 * player.GetIntelligence());
    }

    bool Check_Block_Success()
    {
        float BlockChance = player.GetBlockChance();
        //No Critical Chance
        if (BlockChance == 0)
            return false;
        if (BlockChance >= 1)
            return true;
        //Has Critical Chance
        float check = Random.Range(1.0f, 1.0f / BlockChance);
        if (Mathf.Abs(check - 0.5f * (1.0f / BlockChance))<=0.1f)
            return true;
        else
            return false;
    }
}
