using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Map_Transfer_DB : MonoBehaviour {
    [System.Serializable]
    public struct map_info
    {
       public string name;
       public Texture bg_texture;
       public bool System_Map;
       public int limit_time;
       public int limit_death;
       public int limit_gold;
       public int[] Reward_EXP;
       public int[] Reward_Gold;
    };
    [System.Serializable]
    public struct map_tag
    {
        public string text;
    };
    public map_info[] mapinfo;
    public map_tag[] maptag;


}
