using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        //int length=skill.Length;
        //for(int i=0;i<length;i++)
        //{
        //    skill[i].skillid = i;
        //    if (skill[i].skill_script)
        //        skill[i].skillname = skill[i].skill_script.GetSkillName();
        //}
    }

    //Warning: this script can only run once, or big trouble will comes.
    public void init(int amount,List<Skill> skills)
    {
        Sprite tempType = new Sprite();
        tempType = Resources.Load("null", tempType.GetType()) as Sprite;
        if (!tempType)
            Debug.Log("Load null Failed.");
        skill = new skill_info[amount];
        for (int i = 0; i < amount; i++)
        {
            skill[i].skillid = i;
            skill[i].skill_script = skills[i];
            skill[i].skillicon = tempType;
        }
        Invoke("NameUpdate", 1.0f);
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

    public void NameUpdate()
    {
        for(int i=0;i<skill.Length;i++)
        {
            skill[i].skillname = skill[i].skill_script.GetSkillName();
            Debug.Log(skill[i].skillname);
        }
        CancelInvoke();
    }
}
