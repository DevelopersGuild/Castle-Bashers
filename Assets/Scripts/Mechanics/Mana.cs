using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    //public float StartingMana = 0;
    public int ManaRegenRate = 1;
    public float MaxMana = 0;
    private float mana;
    private float baseMana;
    private float bonusMana;
    private float bonusPercentMana;
    public float manaRegenPerSecond;
    private Player player;

    // Use this for initialization
    void Start()
    {

        mana = GetMaxMana();


    }

    /*
    public void UpdateMaxMP_And_Regen()
    {
        if (player)
        {
            MaxMana = StartingMana + 20 * player.GetIntelligence() + 10 * player.GetStamina() + player.CCI.Class_info[player.GetClassID()].accessory[player.GetAccessoriesLV()].maxmp;
            regenModifier = 2 * player.GetIntelligence();
        }
        else
        {
            MaxMana = StartingMana;
            regenModifier = 1;
        }
        mana = MaxMana;
    }*/

    // Update is called once per frame
    void Update()
    {
        if(mana < GetMaxMana())
        {
            RegenMana();
        }
    }

    public void Full_Regen()
    {
        mana = GetMaxMana(); ;
    }

    /*
    public void SetRegenModifier(float modifier)
    {
        if(modifier > 0)
        {
            manaRegenPerSecond = modifier;
        }
    }*/

    public void RegenMana()
    {
        mana += (manaRegenPerSecond) * Time.deltaTime;
        if (mana > MaxMana)
            mana = MaxMana;
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetMaxMana()
    {
        return baseMana * (1 + bonusPercentMana * 0.01f) + bonusMana;
    }

    public void addMana(int i)
    {
        mana += i;
    }
    public void SetMaxMana(int i)
    {
        MaxMana = i;
    }

    public void addBonusMana(float i) { bonusMana += i; }
    public void addBonusPercentMana(float i) { bonusPercentMana += i; }
    public float getBaseMana() { return baseMana; }
    public float getBonusMana() { return bonusMana; }
    public float getBonusPercentMana() { return bonusPercentMana; }
}
