using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    //public float StartingMana = 0;
    public int ManaRegenRate = 1;
    public float MaxMana = 0;
    private float mana;
    public float manaRegenPerSecond;
    private Player player;

    // Use this for initialization
    void Start()
    {

        mana = MaxMana;


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
        if(mana < MaxMana)
        {
            RegenMana();
        }
    }

    public void Full_Regen()
    {
        mana = MaxMana;
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
        return MaxMana;
    }

    public void addMana(int i)
    {
        mana += i;
    }
    public void SetMaxMana(int i)
    {
        MaxMana = i;
    }
}
