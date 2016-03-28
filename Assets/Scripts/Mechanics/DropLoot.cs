using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropLoot : MonoBehaviour
{
    public int coinValue;
    private GameObject diamond;
    private GameObject gold;
    private GameObject silver;
    private GameObject bronze;

    public List<GameObject> items = new List<GameObject>();
    public List<int> itemDropRates = new List<int>();
    private List<GameObject> itemDrop = new List<GameObject>();

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
        DropOther();
    }

    public void DropOther()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int currentDropChance = Random.Range(0, 100);
            if (currentDropChance <= itemDropRates[i])
            {
                Instantiate((GameObject) items[i], transform.position, Quaternion.identity);
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

        GameObject coin;
        // TODO: Make this cleaner
        for(int i = 0; i < numDiamond; i++)
        {
            coin = (GameObject)Instantiate(diamond, transform.position + 
                new Vector3(Random.RandomRange(-.2f,.2f), Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f)), 
                transform.rotation);
            coin.GetComponent<Rigidbody>().AddExplosionForce(150, transform.position, 5);
        }
        for (int i = 0; i < numGold; i++)
        {
            coin = (GameObject)Instantiate(gold, transform.position +
                new Vector3(Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f)), transform.rotation);
            coin.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 5);
        }
        for (int i = 0; i < numSilver; i++)
        {
            coin = (GameObject)Instantiate(silver, transform.position +
                new Vector3(Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f)), transform.rotation);
            coin.GetComponent<Rigidbody>().AddExplosionForce(125, transform.position, 5);
        }
        for (int i = 0; i < numBronze; i++)
        {
            coin = (GameObject)Instantiate(bronze, transform.position +
                new Vector3(Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f), Random.RandomRange(-.2f, .2f)), transform.rotation);
            coin.GetComponent<Rigidbody>().AddExplosionForce(175, transform.position, 5);
        }
    }

}
