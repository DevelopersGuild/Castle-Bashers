using UnityEngine;
using System.Collections;

public class BGM_Manage : MonoBehaviour {
    [System.Serializable]
    public struct BGM_Info
    {
        public string name;
        public AudioClip aud; 
    };
    public BGM_Info[] BGM;

	
}
