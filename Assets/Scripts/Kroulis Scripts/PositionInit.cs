using UnityEngine;
using System.Collections;

public class PositionInit : MonoBehaviour {

    private PlayerHolder plh;

	// Use this fplayerholderor initialization
	void Start () {
        GameObject playerholder = GameObject.Find("PlayerHolder");
        //playerholder.transform.position = transform.position;
        plh = playerholder.GetComponent<PlayerHolder>();
        plh.resetPositions();
	}
	
    void Update()
    {
        if(plh.GetComponentInChildren<Player>().transform.position.y<-50)
        {
            Debug.Log("Current Level: " + Globe.Map_Load_id);
            plh.resetPositions();
        }
            
    }
}
