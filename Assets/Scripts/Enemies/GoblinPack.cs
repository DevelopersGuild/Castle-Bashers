using UnityEngine;
using System.Collections;

public class GoblinPack : MonoBehaviour {
    const int MAX_PACK_SIZE = 15;
    public int packSize;
    public float packSpread;
    public GameObject Goblin;
    private GameObject[] goblins = new GameObject[MAX_PACK_SIZE];
	// Use this for initialization
	void Start () {
        if(packSize > MAX_PACK_SIZE)
        {
            packSize = MAX_PACK_SIZE;
        }
	    for(int i = 0; i < packSize; i++)
        {
            float spawnX = Random.Range((gameObject.transform.position.x - packSpread), (gameObject.transform.position.x + packSpread));
            float spawnZ = Random.Range((gameObject.transform.position.z - packSpread), (gameObject.transform.position.z + packSpread));
            Vector3 spawnOffset = new Vector3(spawnX, gameObject.transform.position.y, spawnZ);
            goblins[i] = Instantiate(Goblin, gameObject.transform.position - spawnOffset, Quaternion.identity) as GameObject;
        }
	}

}
