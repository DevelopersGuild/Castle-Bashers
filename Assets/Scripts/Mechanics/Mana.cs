using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    public int StartingMana = 0;
    public int ManaRegenRate = 1;
    public float MaxMana = 0;
    private float mana;
    private float regenModifier = 12;
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
    }


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
    }

    // Update is called once per frame
    void Update()
    {
        if(mana < MaxMana)
        {
            RegenMana(regenModifier);
        }
    }

    public void SetRegenModifier(float modifier)
    {
        if(modifier > 0)
        {
            regenModifier = modifier;
        }
    }

    public void RegenMana(float modifier = 1f)
    {
        mana += (ManaRegenRate + modifier) * Time.deltaTime;
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
}
