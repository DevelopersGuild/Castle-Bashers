using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour {
    public GameObject spike;
    public float delayTime;
    private bool triggered = false;
    private float activationTime;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            triggered = true;
            activationTime = Time.time;
        }
    }

    void Update()
    {
        if(triggered == true)
        {
            if(Time.time - activationTime >= delayTime)
            {
                Instantiate(spike, gameObject.transform.position, Quaternion.identity);
                triggered = false;
            }
        }
    }
}
