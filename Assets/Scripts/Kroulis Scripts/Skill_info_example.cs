using UnityEngine;
using System.Collections;

public class Skill_info_example : MonoBehaviour {

    [System.Serializable]
    public struct skill_info
    {
        public int skillid;
        public string skillname;
        public int price;
        public int mana_use;
        public float cooldown;
        public float now_cooldown;
        public int phy_damage;
        //...
    };

    public skill_info[] skill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
