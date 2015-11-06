using UnityEngine;
using System.Collections;

public class CarcassExplosion : MonoBehaviour
{

    public float animDelay;
    public GameObject rotField;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (animDelay <= 0)
        {
            Instantiate(rotField, transform.position, rotField.transform.rotation);
            Destroy(gameObject);
        }
        animDelay -= Time.deltaTime;
    }
}
