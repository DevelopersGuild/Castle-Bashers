using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    private Player[] players;
    private int size;

	// Use this for initialization
	void Start () {
        players = GetComponentsInChildren<Player>();
        size = 0;
        foreach(Player player in players)
        {
            player.setManagerID(size);
            player.GetComponent<ID>().SetID(size);
            size++;
        }
     
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 getPosition(int f = 0)
    {
        if(f == 0)
        {
            //return average position for camera
            return new Vector3(0,0,0);
        }
        else
        {
            //return that player's position
            return players[f].transform.position;
        }
    }

    public int getSize()
    {
        return size;
    }

    public Player[] getPlayers()
    {
        return players;
    }
}
