using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

    private Player[] players;
    private int size;
    private bool deathsign = false;

    // Use this for initialization
    void Start()
    {
        players = GetComponentsInChildren<Player>();
        size = 0;
        foreach (Player player in players)
        {
            player.setManagerID(size);
            player.GetComponent<ID>().SetID(size);
            size++;
        }
        Invoke("GetAllDown", 1.00f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetAllDown()
    {
        int playernumber=0,deathnumber=0;

        foreach(Player player in players)
        {
            if(player.gameObject.activeInHierarchy==true)
            {
                playernumber++;
                if(player.getDown())
                {
                    deathnumber++;
                }
            }
        }
        if (playernumber != 0 && playernumber == deathnumber)
        {
            if (!deathsign)
            {
                GameObject.Find("Main Process").GetComponent<Main_Process>().UI_Death_Window_Open_Withmusic();
            }
            deathsign = true;
        }
        else
            deathsign = false;
        Invoke("GetAllDown", 1.00f);
    }

    public Vector3 getPosition(int f = 0)
    {
        if (f == 0)
        {
            //return average position for camera
            return new Vector3(0, 0, 0);
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
        players = GetComponentsInChildren<Player>();
        size = 0;
        foreach (Player player in players)
        {
            player.setManagerID(size);
            player.GetComponent<ID>().SetID(size);
            size++;
        }
        return players;
    }

    public Player getUpPlayer()
    {
        foreach (Player player in players)
        {
            if (!player.getDown())
                return player;
        }
        return null;
    }

    public float getAvgLevel()
    {
        float f = 0;
        float i = 0;
        foreach(Player p in players)
        {
            f += p.GetComponent<Experience>().GetCurrentLevel();
            i++;
        }
        return f / i;
    }

    /// <summary>
    /// 1 for threat, 2 for damage, 3 for priority
    /// </summary>
    public Player[] getSortedPlayers(int f)
    {
        Player[] temp = players;
        if (f == 1)
            System.Array.Sort(temp, SortByThreat);
        else if(f == 2)
            System.Array.Sort(temp, SortByDamage);
        else if(f == 3)
            System.Array.Sort(temp, SortByPriority);

        return temp;
    }

    private static int SortByThreat(Player p1, Player p2)
    {
        return p1.threatLevel.CompareTo(p2.threatLevel);
    }

    private static int SortByDamage(Player p1, Player p2)
    {
        return p1.damageDealt.CompareTo(p2.damageDealt);
    }

    private static int SortByPriority(Player p1, Player p2)
    {
        return p1.getPriorityID().CompareTo(p2.getPriorityID());
    }
}
