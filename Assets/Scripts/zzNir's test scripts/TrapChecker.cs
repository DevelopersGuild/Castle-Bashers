using UnityEngine;
using System.Collections;

public class TrapChecker : MonoBehaviour {

    public float timeToLive;
    public GameObject parent;

    private bool activated;

	// Use this for initialization
	void Start () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        GameObject enemObj = other.gameObject;
        if (enemObj.CompareTag("Player"))
        {
            parent.GetComponent <DealDamageToPlayer>().enabled = true;
            //instead of animation
            parent.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            Destroy(parent, timeToLive);
            Destroy(gameObject);
        }

       
    }
}
