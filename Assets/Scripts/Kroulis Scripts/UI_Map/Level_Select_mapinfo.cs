using UnityEngine;
using System.Collections;

public class Level_Select_mapinfo : MonoBehaviour {
    [System.Serializable]
    public struct Map_info
    {
        public int mapid;
        public Vector3 Position;
    }

    [System.Serializable]
    public struct Chapter_Map
    {
        public Map_info[] mapinfo;
    };
    public Chapter_Map[] Chapter;
}
