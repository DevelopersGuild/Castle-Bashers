using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public int coinValue;
    public AudioClip pickupSound;

    // Use this for initialization
    public void OnTriggerEnter(Collider other)
    {
        // Play sound and destory objecton pickup
        if (other.gameObject.CompareTag("Player"))
        {
            if(pickupSound)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }

}
