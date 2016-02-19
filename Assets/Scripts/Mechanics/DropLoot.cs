using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropLoot : MonoBehaviour
{
    private GameObject item;
    public int dropChance;
    public int coinValue;
    private GameObject diamond;
    private GameObject gold;
    private GameObject silver;
    private GameObject bronze;

    public List<GameObject> items = new List<GameObject>();
    public List<int> itemDropRates = new List<int>();
    private List<GameObject> itemDrop = new List<GameObject>();

    private int itemDropRate; // Probability that item will drop out of 10

    public void Start()
    {
        diamond = Resources.Load("LevelObjects/DiamondCoin") as GameObject;
        gold = Resources.Load("LevelObjects/GoldCoin") as GameObject;
        silver = Resources.Load("LevelObjects/SilverCoin") as GameObject;
        bronze = Resources.Load("LevelObjects/BronzeCoin") as GameObject;
    }

    public void DropItem()
    {
        DropCoins();
        //DropOther();
    }

    public void DropOther()
    {
        // Check if an item will drop
        int currentDropChance = Random.Range(0, 100);

        // If an item does drop, pick one randomly with the given drop chances
        if (currentDropChance <= dropChance)
        {
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < itemDropRates[i]; j++)
                {
                    itemDrop.Add(items[i]);
                }
            }

            //If it will drop an item, set the item it will drop to a random item that it can drop
            itemDropRate = Random.Range(0, itemDrop.Count);
            item = itemDrop[itemDropRate];

            //Spawn the item
            if (item != null)
            {
                Instantiate(item, transform.position, transform.rotation);
            }
        }
    }

    public void DropCoins()
    {
        int currentValue =  coinValue;
        int numDiamond = currentValue / 500;
        currentValue %= 500;
        int numGold = currentValue / 100;
        currentValue %= 100;
        int numSilver = currentValue / 25;
        currentValue %= 25;
        int numBronze = currentValue / 5;
        currentValue %= 5;

        for(int i = 0; i < numDiamond; i++)
        {
            Instantiate(diamond, transform.position, transform.rotation);
        }
        for (int i = 0; i < numGold; i++)
        {
            Instantiate(gold, transform.position, transform.rotation);
        }
        for (int i = 0; i < numGold; i++)
        {
            Instantiate(silver, transform.position, transform.rotation);
        }
        for (int i = 0; i < numSilver; i++)
        {
            Instantiate(bronze, transform.position, transform.rotation);
        }
    }

}
