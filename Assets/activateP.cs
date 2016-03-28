using UnityEngine;
using System.Collections;

public class activateP : MonoBehaviour
{

    private bool enabled = true;
    private Player player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (enabled)
        {
            if (player)
            {
                Debug.Log((player.transform.position - transform.position).magnitude);
                if ((player.transform.position - transform.position).magnitude < 15)
                {
                    enabled = false;
                    GetComponent<MaladyV2>().enabled = true;
                    FindObjectOfType<Camera>().orthographicSize = 8;
                }
            }
            else
            {
                player = FindObjectOfType<Player>();
            }
        }
    }
}
