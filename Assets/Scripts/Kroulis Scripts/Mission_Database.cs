using UnityEngine;
using System.Collections;

public class Mission_Database : MonoBehaviour {

    public int death = 0;
    public int get_gold = 0;
    public int Aget=0, Bget=0;

    public void clear_db()
    {
        death = 0;
        get_gold = 0;
        Aget = 0;
        Bget = 0;
    }

    public void dead()
    {
        death++;
    }

    public void Add_Gold(int player_id,int ? value)
    {
        if(value==null)
        {
            get_gold++;
            if (player_id == 1)
                Aget++;
            else
                Bget++;
        }
        else
        {
            get_gold += (int)value;
            if (player_id == 1)
                Aget += (int)value;
            else
                Bget += (int)value;
        }
    }

    public double GetPercent(int player_id)
    {
        if (player_id == 1)
            return Aget / get_gold;
        else
            return Bget / get_gold;
    }
}
