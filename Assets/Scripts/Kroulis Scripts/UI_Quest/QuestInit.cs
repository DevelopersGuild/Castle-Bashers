using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kroulis.Quest;

public class QuestInit: MonoBehaviour{

    private Dictionary<int, Quest> QuestList=new Dictionary<int,Quest>();
    Quest tempq=null;
    int id=0;
    void Start()
    {
        //Create a new Quest
        tempq = new Quest();
        id=10000;
        tempq.SetBasicInfo(id, "Test", "This is a quest only for test");
        tempq.SetNeedNumbers(3);
        tempq.SetNeedMoney(100);
        tempq.SetNeedLevel(4);
        tempq.SetNeedItem(1);
        tempq.SetRewardEXP(123);
        tempq.SetRewardGold(321);
        tempq.SetRewardItemsNumbers(1);
        tempq.SetRewardItems(2);
        QuestList.Add(id, tempq);
        
        //
    }
    

}
