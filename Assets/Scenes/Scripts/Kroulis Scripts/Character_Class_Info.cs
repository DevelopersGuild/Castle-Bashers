using UnityEngine;
using System.Collections;

public class Character_Class_Info : MonoBehaviour {
    [System.Serializable]
    public struct Equipment_info
    {
        public string name;
        public Sprite icon;
        public int lim_lv, up_gold;
        public int atk, def;
        public int batk,matk,pdef,mdef,maxhp,maxmp,cri;
    }

    [System.Serializable]
    public struct LevelUpAddStats
    {
        public int atk, def, sta, spi, agi;
    }

    [System.Serializable]
    public struct Class_INFO
    {
        public string name;
        public Sprite icon;
        public int[] skillid;
        public LevelUpAddStats ClassAddStats;
        public Equipment_info[] weapon;
        public Equipment_info[] armor;
        public Equipment_info[] accessory;
    };

    public Class_INFO[] Class_info;
}
