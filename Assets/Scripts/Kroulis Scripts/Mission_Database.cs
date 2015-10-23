using UnityEngine;
using System.Collections;

public class Mission_Database : MonoBehaviour {

    public int death = 0;
    public int get_gold = 0;

    public void clear_db()
    {
        death = 0;
        get_gold = 0;
    }

    public void dead()
    {
        death++;
    }

    public void Add_Gold(int ? value)
    {
        if(value==null)
        {
            get_gold++;
        }
        else
        {
            get_gold += (int)value;
        }
    }
}
