using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour
{
    private Player player;
    public int defense = 0;
    public int basePhysicalDefense = 0;
    public int baseMagicalDefense = 0;
    private int bonusPhysicalDefense = 0;
    private int bonusMagicalDefense = 0;
    
    //public int PhysicalDefense = 0;
    //public int MagicalDefense = 0;
    private Health health;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        player = gameObject.GetComponent<Player>();

    }

    public void CheckPhysicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        if(player)
            if (Check_Block_Success())
                damage = damage * 0.35f;
        damage = damage - GetPhysicalDefense();
        if (!player)
            Debug.Log(damage);
        if (damage > 0)
        {
            health.takeDamage(damage, knockback, flinch);
        }
    }

    public void CheckMagicalDefense(float damage, float knockback = 4, float flinch = 5)
    {
        if(player)
            if (Check_Block_Success())
                damage = damage * 0.5f;
        damage = damage - GetMagicalDefense();
        if(damage > 0)
        {
            Debug.Log("took " + damage);
            health.takeDamage(damage, knockback, flinch);
        } 
    }

    public void SetPhysicalDefense(int value)
    {
        Debug.Log("USING OUTDATED FUNCTION, PLEASE UPDATE CALL TO SetPhysicalDefense");
        //PhysicalDefense = value;
    }

    //Use a negative value to subtract
    public void AddBonusPhysicalDefense(int value)
    {
        bonusPhysicalDefense += value;
    }
    
    public void SetBasePhysicalDefense(int value)
    {
        basePhysicalDefense = value;
    }

    public int GetPhysicalDefense()
    {
        return basePhysicalDefense + bonusPhysicalDefense;
    }

    public int GetBasePhysicalDefense()
    {
        return basePhysicalDefense;
    }

    public int GetBonusPhysicalDefense()
    {
        return bonusPhysicalDefense;
    }

    public void SetMagicalDefense(int value)
    {

        Debug.Log("USING OUTDATED FUNCTION, PLEASE UPDATE CALL TO SetPhysicalDefense");
        //MagicalDefense = value;
    }

    public void SetBaseMagicalDefense(int value)
    {
        baseMagicalDefense = value;
    }

    //Use negative value to subtract
    public void AddBonusMagicalDefense(int value)
    {
        bonusMagicalDefense += value;
    }

    public int GetMagicalDefense()
    {
        return baseMagicalDefense + bonusMagicalDefense;
    }

    public int GetBonusMagicalDefense()
    {
        return bonusMagicalDefense;
    }

    public int GetBaseMagicalDefense()
    {
        return baseMagicalDefense;
    }

    public float GetDefense()
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
        SetBasePhysicalDefense(5 * defense + player.GetStrength() + 2 * player.GetStamina() + player.GetAgility());
        SetBaseMagicalDefense(2 * defense + 6 * player.GetIntelligence());
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
