using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public int coinValue;
    public AudioClip pickupSound;

    // Use this for initialization
    public void OnCollisionEnter(Collision other)
    {
        // Incremount coin count, play sound, and destroy object on pickup
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<CoinManager>())
            {
                CoinManager coinManager = other.gameObject.GetComponent<CoinManager>();
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
