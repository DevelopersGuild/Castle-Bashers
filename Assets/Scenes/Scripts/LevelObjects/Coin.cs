using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public int coinValue;
    public AudioClip pickupSound;

    // Use this for initialization
    public void OnTriggerEnter(Collider other)
    {
        // Incremount coin count, play sound, and destroy object on pickup
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<CoinManager>())
            {
                CoinManager coinManager = other.GetComponent<CoinManager>();
                coinManager.addCoins(coinValue);
            }

            if(pickupSound)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }

}
