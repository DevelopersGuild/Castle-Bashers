using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    public int StartingMana = 0;
    public int ManaRegenRate = 1;
    public int MaxMana = 0;
    private float mana;
    private float regenModifier = 12;
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        if(player != null)
        {
            mana = StartingMana + player.GetIntelligence();
            regenModifier = player.GetIntelligence();
        }
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
    }

    public float GetMana()
    {
        return mana;
    }
}
