using UnityEngine;
using System.Collections;

public class Skill_info : MonoBehaviour {

    [System.Serializable]
    public struct skill_info
    {
        [HideInInspector]
        public int skillid;
        [HideInInspector]
        public string skillname;
        public Sprite skillicon;
        public Skill skill_script;
    };

    public skill_info[] skill;

    void Start()
    {
        int length=skill.Length;
        for(int i=0;i<length;i++)
        {
            skill[i].skillid = i;
            skill[i].skillname = skill[i].skill_script.GetSkillName();
        }
    }

    public int GetSkillPrice(int skill_id)
    {
        if (skill_id >= skill.Length)
        {
            Debug.LogError("DataBase: Skill_info.cs -> GetSkikPrice : skill_id overload.");
            return 0x3f3f3f3f;
        }
        else
            return skill[skill_id].skill_script.GetPrice();
    }

}
