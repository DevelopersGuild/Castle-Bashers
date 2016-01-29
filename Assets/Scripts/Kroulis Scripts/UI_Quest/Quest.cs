using UnityEngine;
using System.Collections;

namespace Kroulis.Quest
{
    public struct QuestNeed //Must use a lot of this structure objects instead of one that have coins, levels and items.
    {
        public bool needCoins,needLevel;
        public int item_id;
        public int item_number;
        public int coins;
        public int level_id;
        public bool finished; //Only For the levels.

        public void Clear()
        {
            needCoins = false;
            needLevel = false;
            finished = false;
            item_id = item_number = coins = level_id = 0;
        }
    }

    public struct QuestRewardEXP
    {
        public int value;
    }

    public struct QuestRewardGold
    {
        public int value;
    }

    public struct QuestRewardItem
    {
        public int item_id;
        public int item_number;
    }

    public struct QuestPrerequisite
    {
        public int[] id;
        public QuestPrerequisite(int size)
        {
            id = new int[size];
        }
    }


    public class Quest : MonoBehaviour
    {
        private int quest_id = 0;
        private string quest_name = "";
        private string quest_description = "";
        private QuestPrerequisite quest_prerequisite;
        private QuestNeed[] quest_needs;
        private QuestRewardGold quest_reward_gold;
        private QuestRewardEXP quest_reward_exp;
        private QuestRewardItem[] quest_reward_items;
        private bool on = false;
        private int need_counter=0;
        private int reward_items_counter = 0;
        private GameObject MainProcess=null;

        public void SetBasicInfo(int id, string name, string description)
        {
            quest_id = id;
            quest_name = name;
            quest_description = description;
        }

        public void SetPrerequisite(int[] id)
        {
            quest_prerequisite = new QuestPrerequisite(id.Length);
            quest_prerequisite.id = id;
        }

        public void SetNeedNumbers(int number)
        {
            quest_needs = new QuestNeed[number];
            need_counter=0;
        }

        public void SetNeedMoney(int value)
        {
            if(need_counter>=quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[need_counter].Clear();
            quest_needs[need_counter].needCoins = true;
            quest_needs[need_counter].coins = value;
            need_counter++;
        }

        public void SetNeedMoney(int id,int value)
        {
            if(id >= quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[id].Clear();
            quest_needs[id].needCoins = true;
            quest_needs[id].coins = value;
        }

        public void SetNeedLevel(int value)
        {
            if (need_counter >= quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[need_counter].Clear();
            quest_needs[need_counter].needLevel = true;
            quest_needs[need_counter].level_id = value;
            need_counter++;
        }

        public void SetNeedLevel(int id, int value)
        {
            if (id >= quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[id].Clear();
            quest_needs[id].needLevel = true;
            quest_needs[id].level_id = value;
        }

        public void SetNeedItem(int item_id,int item_number=1)
        {
            if (need_counter >= quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[need_counter].Clear();
            quest_needs[need_counter].item_id = item_id;
            quest_needs[need_counter].item_number = item_number;
            need_counter++;
        }

        public void SetNeedItem(int id, int item_id, int item_number=1)
        {
            if (id >= quest_needs.Length)
            {
                Debug.Log("Need out of range! Please change the number in function:SetNeedNumbers.");
                return;
            }
            quest_needs[id].Clear();
            quest_needs[id].item_id = item_id;
            quest_needs[id].item_number = item_number;
        }

        public void SetRewardGold(int value)
        {
            quest_reward_gold.value = value;
        }

        public void SetRewardEXP(int value)
        {
            quest_reward_exp.value = value;
        }

        public void SetRewardItemsNumbers(int size)
        {
            quest_reward_items = new QuestRewardItem[size];
            reward_items_counter = 0;
        }

        public void SetRewardItems(int item_id,int item_number=1)
        {
            if(reward_items_counter>=quest_reward_items.Length)
            {
                Debug.Log("Reward Items out of range! Please change the number in function:SetRewardItemsNumbers.");
                return;
            }
            quest_reward_items[reward_items_counter].item_id = item_id;
            quest_reward_items[reward_items_counter].item_number = item_number;
        }

        public void SetRewardItems(int id, int item_id, int item_number = 1)
        {
            if (id >= quest_reward_items.Length)
            {
                Debug.Log("Reward Items out of range! Please change the number in function:SetRewardItemsNumbers.");
                return;
            }
            quest_reward_items[id].item_id = item_id;
            quest_reward_items[id].item_number = item_number;
        }

        public void StartQuest(GameObject _MainProcess)
        {
            MainProcess = _MainProcess;
            if(!on && CheckPrerequisite())
            {
                on = true;
                MainProcess.GetComponent<Main_Process>().GetQuestList().AddtoProcessing(quest_id);
            }
        }
        public bool CheckQuestNeed()
        {

            return false;
        }

        public bool CheckPrerequisite()
        {
            QuestList ql = MainProcess.GetComponent<Main_Process>().GetQuestList();
            for (int i = 0; i < quest_prerequisite.id.Length;i++ )
            {
                if (!ql.CheckFinished(quest_prerequisite.id[i]))
                    return false;
            }
                return true;
        }

        public bool Finish(GameObject PlayerHolder,GameObject MainProcess)
        {
            if(on)
                if(CheckQuestNeed())
                {
                    //stop processing
                    on = false;
                    //give the player reward (here only exp and gold)
                    Player[] playerlist = PlayerHolder.GetComponentsInChildren<Player>();
                    foreach(Player player in playerlist)
                    {
                        player.GetComponent<Experience>().AddExperience(quest_reward_exp.value);
                        player.GetComponent<CoinManager>().addCoins(quest_reward_gold.value);
                    }
                    //give the player items

                    //move the this quest out of the processing list and add to the finished list.
                    MainProcess.GetComponent<Main_Process>().GetQuestList().RemovefromProcessing(quest_id);
                    MainProcess.GetComponent<Main_Process>().GetQuestList().SetFinished(quest_id);

                    return true;
                }
            return false;
        }

        public void ForceFinish(GameObject PlayerHolder, GameObject MainProcess)
        {
            if(on)
            {
                //stop processing
                on = false;
                //give the player reward (here only exp and gold)
                Player[] playerlist = PlayerHolder.GetComponentsInChildren<Player>();
                foreach (Player player in playerlist)
                {
                    player.GetComponent<Experience>().AddExperience(quest_reward_exp.value);
                    player.GetComponent<CoinManager>().addCoins(quest_reward_gold.value);
                }
                //give the player items

                //move the this quest out of the processing list and add to the finished list.
                MainProcess.GetComponent<Main_Process>().GetQuestList().RemovefromProcessing(quest_id);
                MainProcess.GetComponent<Main_Process>().GetQuestList().SetFinished(quest_id);

            }
        }

        public void GiveUp(GameObject MainProcess)
        {
            if(on)
            {
                //stop the peocessing
                on = false;
                //Removing this quest from the processing list
                MainProcess.GetComponent<Main_Process>().GetQuestList().RemovefromProcessing(quest_id);
            } 
        }
        // Update is called once per frame
        void Update()
        {
            if(on)
            {

            }
        }
    }
}

