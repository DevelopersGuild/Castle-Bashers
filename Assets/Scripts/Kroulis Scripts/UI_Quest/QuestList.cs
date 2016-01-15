using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestList : MonoBehaviour {

    List<int> Quest_Processing_List;
    List<int> Quest_Finished_List;
	// Use this for initialization
	void Start () 
    {
	
	}
	
    public void ImportData(int[] list)//Only for Init
    {

    }

    public bool CheckFinished(int id)
    {
        /*foreach(int i in Quest_Finished_List)
        {
            if (i == id)
                return true;
        }*/
        if (Quest_Finished_List.Exists(x => x == id))
            return true;
        return false;
    }

    public bool SetFinished(int id)
    {
        if(Quest_Finished_List.Exists(x => x==id))
        {
            return false;
        }
        Quest_Finished_List.Add(id);
        return true;
    }

    public bool AddtoProcessing(int id)
    {
        if (Quest_Processing_List.Exists(x => x == id))
            return false;
        Quest_Processing_List.Add(id);
        return true;
    }

    public bool RemovefromProcessing(int id)
    {
        if (!Quest_Processing_List.Exists(x => x == id))
            return false;
        Quest_Processing_List.Remove(id);
        return true;
    }

    public List<int> GetProcessingList()
    {
        return Quest_Processing_List;
    }

    public List<int> GetFinishedList()
    {
        return Quest_Finished_List;
    }

}
