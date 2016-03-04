using UnityEngine;
using System.Collections;

public class PositionInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject playerholder = GameObject.Find("PlayerHolder");
        //playerholder.transform.position = transform.position;
        PlayerHolder plh = playerholder.GetComponent<PlayerHolder>();
        plh.resetPositions();
	}
	
}
