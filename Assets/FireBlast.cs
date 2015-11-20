using UnityEngine;
using System.Collections;

public class FireBlast : MonoBehaviour {
    public float burnTime = 2f;
    void Start()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if(!col.gameObject.GetComponent<Player>())
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (col.gameObject.GetComponent<Defense>())
            {
                if (col.gameObject.GetComponent<Burn>())
                {
                    Destroy(col.gameObject.GetComponent<Burn>());
                }
                col.gameObject.AddComponent<Burn>();
                col.gameObject.GetComponent<Burn>().setDuration(burnTime);
            }
            Destroy(gameObject);
            Debug.Log("Fireblast destroyed by " + col.gameObject.name);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.GetComponent<Player>())
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (col.gameObject.GetComponent<Defense>())
            {
                col.gameObject.AddComponent<Burn>();
            }
            Destroy(gameObject);

            Debug.Log("Fireblast destroyed by " + col.gameObject.name);
        }
    }
}
