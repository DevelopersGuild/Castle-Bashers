using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject[] skills;
    private CoinManager playerCoins;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCoins = other.GetComponent<CoinManager>();
        }

        //Test
        //buySkill(0);
    }

    void buySkill(int skillIndex)
    {
        int price = skills[skillIndex].GetComponent<ISkill>().GetPrice();

        //TODO: Also check if that skill has already been unlocked once this systems been implemented
        if(price < playerCoins.getCoins())
        {
            playerCoins.subtractCoins(price);
        }
        else
        {
            Debug.Log("You cant afford this skill!");
        }
    }

    // Protection so anything that doesnt implement ISkill in the skills array is removed
    void OnValidate()
    {
        for(int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetComponent<ISkill>() == null)
            {
                skills[i] = null;
            }
        }
    }
}
