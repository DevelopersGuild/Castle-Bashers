using UnityEngine;
using System.Collections;

public class GroupingManager : MonoBehaviour
{

    private Player[] players;
    private float size, current, ratio, radius;

    // Use this for initialization
    void Start()
    {
        ratio = 0.5f;
        players = null;
        size = current = 0;
        radius = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Can also be objects that follow the player, say when they collide with other grouping objects	
    }

    public void setPlayerGroup(Player[] p, float r = 2)
    {
        players = p;
        size = players.Length;
        current = 0;
        radius = r;
    }

    public bool Check()
    {
        if (size > 1)
        {
            float temp = 0;
            current = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!players[j].getDown())
                    {
                        float f = (players[i].transform.position - players[j].transform.position).magnitude;
                        if (f <= radius)
                            temp++;
                    }
                }
                if (temp > current)
                    current = temp;

                temp = 0;
            }

            return (current / size) > ratio;
        }
        return false;
    }

}
