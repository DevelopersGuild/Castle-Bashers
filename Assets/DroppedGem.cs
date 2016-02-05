using UnityEngine;
using System.Collections;

public class DroppedGem : MonoBehaviour {

    int quality; // 1: normal
                 // 2: Rare
                 // 3: Epic

    Gem gem;
    int gemType;
    Player player;

	// Use this for initialization
	void Start () {
        quality = Random.Range(1, 3); //randomizes normal, rare, epic. Will change to incorporate percent chance of each
        if(quality >= 2)
        {
            gemType = Random.Range(1, 4); // If rare or epic, can roll skill gem
        }
        else
        {
            gemType = Random.Range(1, 3);
        }
        //Roll stats for gem
        Debug.Log("stats rolled!");
	}

    void OnTriggerEnter(Collider col)
    {

        if (player = col.gameObject.GetComponent<Player>())
        {
            Debug.Log("Detected the collision!");
            PickedUp(player);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //include setters for manually setting stats

    void PickedUp(Player player)
    {

        //if(gemType == 1)
        {
            gem = new StrengthGem();
            Debug.Log("Created strength gem");
        }

        gem.setOwner(player);
        Debug.Log("Set Player properly");
        //Set stats for gem before adding to play
 
        player.GetComponent<GemManager>().addGem(gem);
        Destroy(gameObject);
    }
}
