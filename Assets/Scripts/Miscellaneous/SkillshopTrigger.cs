using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillshopTrigger : MonoBehaviour {

    private Main_Process mainprocess;
    private List<Skill> allSkills = new List<Skill>();
    public int skillClassID; // 1 is weapon, 2 is armor, 3 is accessories
    public int[] skillArray;

    void Start()
    {
        mainprocess = FindObjectOfType<Main_Process>();
        allSkills.Add(gameObject.AddComponent<HealthRegenSkill>());
        allSkills.Add(gameObject.AddComponent<Skill_TimeStop>());
        //allSkills.Add(gameObject.AddComponent<Skill_BoostV2>());
        allSkills.Add(gameObject.AddComponent<Skill_BulkUp>());
        allSkills.Add(gameObject.AddComponent<IgniteSkill>());
        allSkills.Add(gameObject.AddComponent<FireballSkill>());
        allSkills.Add(gameObject.AddComponent<FireblastSkill>());
        allSkills.Add(gameObject.AddComponent<ShockSkill>());
        allSkills.Add(gameObject.AddComponent<sMeteor>());


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainprocess.UI_SkillShop_Open(skillClassID);
        }
    }

    public List<Skill> GetSkillList()
    {
        return allSkills;
    }

}
