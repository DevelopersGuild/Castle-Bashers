using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropLoot : MonoBehaviour
{
    private GameObject item;
    public int dropChance;

    public List<GameObject> items = new List<GameObject>();
    public List<int> itemDropRates = new List<int>();
    private List<GameObject> itemDrop = new List<GameObject>();

    private int itemDropRate; // Probability that item will drop out of 10

    public void DropItem()
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

}
