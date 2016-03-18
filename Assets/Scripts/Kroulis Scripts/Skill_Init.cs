using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_Init : MonoBehaviour {

    public static int classes = 1;
    private List<Skill> allSkills = new List<Skill>();
    private Skill_info si;
    private Character_Class_Info cci;
    [HideInInspector]
    public int[] skillArray;

    void Start()
    {
        //Init all skills
        allSkills.Add(gameObject.AddComponent<HealthRegenSkill>());
        allSkills.Add(gameObject.AddComponent<Skill_TimeStop>());
        //allSkills.Add(gameObject.AddComponent<Skill_BoostV2>());
        allSkills.Add(gameObject.AddComponent<Skill_BulkUp>());
        allSkills.Add(gameObject.AddComponent<IgniteSkill>());
        allSkills.Add(gameObject.AddComponent<FireballSkill>());
        allSkills.Add(gameObject.AddComponent<FireblastSkill>());
        allSkills.Add(gameObject.AddComponent<ShockSkill>());
        allSkills.Add(gameObject.AddComponent<MeteorShowerSkill>());

        si = GetComponent<Skill_info>();
        si.init(allSkills.Count, allSkills);
        cci = GetComponentInParent<Main_Process>().gameObject.GetComponentInChildren<Character_Class_Info>();
        cci.SkillUpdate(0,allSkills.Count);
        for (int i = 0; i <= allSkills.Count - 1; i++)
            cci.Class_info[0].skillid[i] = i;

        

    }

    //public List<Skill> GetSkillList(int cid)
    //{
    //    if (cid > classes)
    //        return null;
    //    else
    //        return allSkills[cid];
    //}

}
