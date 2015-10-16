using UnityEngine;
using System.Collections;

public class Character_Class_Info : MonoBehaviour {
    [System.Serializable]
    public struct Class_INFO
    {
        public string name;
        public Sprite icon;
    };
    public Class_INFO[] Class_info;
}
