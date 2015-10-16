using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    public int StartingMana = 0;
    public int ManaRegenRate = 1;
    public int MaxMana = 0;
    private float mana;
    private int regenModifier = 12;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(mana < MaxMana)
        {
            RegenMana(regenModifier);
        }
    }

    public void SetRegenModifier(int modifier)
    {
        if(modifier > 0)
        {
            regenModifier = modifier;
        }
    }

    public void RegenMana(int modifier = 1)
    {
        mana += (ManaRegenRate * modifier) * Time.deltaTime;
    }

    public float GetMana()
    {
        return mana;
    }
}
