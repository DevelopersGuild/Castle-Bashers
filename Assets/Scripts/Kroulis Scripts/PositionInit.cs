using UnityEngine;
using System.Collections;

public class PositionInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject playerholder = GameObject.Find("PlayerHolder");
        //playerholder.transform.position = transform.position;
        Player[] pls = playerholder.GetComponentsInChildren<Player>();
        foreach(Player pl in pls)
        {
            pl.transform.position = new Vector3(12.4f, 0f, 0f);
        }
	}
	
}
