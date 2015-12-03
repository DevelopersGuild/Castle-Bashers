using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{

    public List<Skill> unlockedSkills;
    public Skill[] currentSkillLoadout;

    //Nir stuff
    private bool checkSkill1, checkSkill2, checkSkill3, checkSkill4;
    private Player player;

    // Use this for initialization
    void Start()
    {
        unlockedSkills = new List<Skill>();
        currentSkillLoadout = new Skill[4];
        unlockedSkills.Add(gameObject.AddComponent<HealthRegenSkill>());
        currentSkillLoadout[0] = unlockedSkills[0];
        player = GetComponent<Player>();
        checkSkill1 = checkSkill2 = checkSkill3 = checkSkill4 = false;
        /*if (player)
        {
            foreach (Skill sk in currentSkillLoadout)
            {
                sk.GetComponent<ID>().SetID(player.getManagerID());
            }
        }
        */
    }

    public void UnlockSkill(Skill newSkill)
    {
        unlockedSkills.Add(newSkill);
    }

    public void UnlockSkillI(Skill newSkill)
    {
        unlockedSkills.Add(newSkill);
    }

    public Skill UnlockSkillC(Skill newSkill)
    {
        return (Skill)gameObject.AddComponent(newSkill.GetType());
    }

    public void ChangeSkill(int skillID, int replaceIndex)
    {
        currentSkillLoadout[replaceIndex] = unlockedSkills[skillID];
        //currentSkillLoadout[replaceIndex].GetComponent<ID>().SetID(player.getManagerID());
    }
    public void ChangeSkill(Skill skill, int replaceIndex)
    {
        unlockedSkills.Add(UnlockSkillC(skill));
        ChangeSkill(unlockedSkills.Count - 1, replaceIndex);
    }

    public void FindAndChangeSkill(Skill skill, int replaceIndex)
    {
        bool flag = false;
        System.Type type = skill.GetType();
        foreach(Skill ski in unlockedSkills)
        {
            if(ski.GetType()==type)
            {
                flag = true;
                currentSkillLoadout[replaceIndex] = ski;
                break;
            }
        }
        if(flag)
        {
            //currentSkillLoadout[replaceIndex].GetComponent<ID>().SetID(player.getManagerID());
        }
        else
        {
            ChangeSkill(skill, replaceIndex);
        }
    }

    public Skill GetSlotSkill(int id)
    {
        return currentSkillLoadout[id];
    }

    public void UseSkill1()
    {
        if (currentSkillLoadout[0].GetCoolDownTimer() <= 0)
        {
            currentSkillLoadout[0].UseSkill(gameObject, null);
            AudioSource.PlayClipAtPoint(currentSkillLoadout[0].useSkillAudio, transform.position);
            Debug.Log("Use Skill 1");

            if (player || true)
            {

                if (!checkSkill1)
                {
                    unlockedSkills[0].UseSkill(gameObject, null);
                    player.addSkill(unlockedSkills[0], 0);
                    checkSkill1 = true;
                }
            }
        }

    }

    public void UseSkill2()
    {
        if (currentSkillLoadout[1].GetCoolDownTimer() <= 0)
        {
            currentSkillLoadout[1].UseSkill(gameObject, null);
            AudioSource.PlayClipAtPoint(currentSkillLoadout[1].useSkillAudio, transform.position);
            Debug.Log("Use Skill 2");

            if (player)
            {
                if (!checkSkill2)
                {
                    player.addSkill(unlockedSkills[1], 1);
                    checkSkill2 = true;
                }
            }
        }

    }

    public void UseSkill3()
    {
        if (currentSkillLoadout[2].GetCoolDownTimer() <= 0)
        {
            currentSkillLoadout[2].UseSkill(gameObject, null);
            AudioSource.PlayClipAtPoint(currentSkillLoadout[2].useSkillAudio, transform.position);
            Debug.Log("Use Skill 3");

            if (player)
            {
                if (!checkSkill3)
                {
                    player.addSkill(unlockedSkills[2], 2);
                    checkSkill3 = true;
                }
            }
        }
    }

    public void UseSkill4()
    {
        if (currentSkillLoadout[3].GetCoolDownTimer() <= 0)
        {
            currentSkillLoadout[3].UseSkill(gameObject, null);
            AudioSource.PlayClipAtPoint(currentSkillLoadout[3].useSkillAudio, transform.position);
            Debug.Log("Use Skill 4");

            if (player)
            {
                if (!checkSkill4)
                {
                    player.addSkill(unlockedSkills[3], 3);
                    checkSkill4 = true;
                }
            }
        }
    }

    public void Reset()
    {
        checkSkill1 = false;
        checkSkill2 = false;
        checkSkill3 = false;
        checkSkill4 = false;
    }

}
