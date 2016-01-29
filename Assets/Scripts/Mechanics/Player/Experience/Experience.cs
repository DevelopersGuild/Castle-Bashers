using UnityEngine;
using System.Collections;
using System;

public class Experience : MonoBehaviour
{
    public int currentLevel = 1;
    public int ExperincePointsPerLevel = 2;
    private int ExperincePoints;
    private int ExperincePointUsed = 0;
    private int currentExperince;
    private int experinceUntilNextLevel = 1000;
    private Character_Class_Info class_info;
    private Player player;
    
    void Start()
    {
        ExperincePoints = currentLevel * ExperincePointsPerLevel;
        //class_info = GameObject.Find("Main Process").GetComponentInChildren<Character_Class_Info>();
        player = GetComponent<Player>();
    }

    public int GetExperience()
    {
        return currentExperince;
    }

    public void SetExperience(int EXP)
    {
        currentExperince = EXP;
        //LevelUp();
    }

    public int GetNEXP()
    {
        return experinceUntilNextLevel;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetLevel(int lv)
    {
        currentLevel = lv;
        experinceUntilNextLevel = (int)(Math.Pow(10, (int)(lv / 10) + 1) * Math.Pow(1.2 + 0.001 * (lv % 10), lv % 10));
    }

    public void AddExperince(int amountToAdd)
    {
        currentExperince += amountToAdd;
        LevelUp();
    }

    public void LevelUp()
    {
        bool upgrade_flag = false;
        while(currentExperince >= experinceUntilNextLevel)
        {
            upgrade_flag = true;
            //experinceUntilNextLevel = currentExperince * 2;
            ++currentLevel;
            experinceUntilNextLevel = (int)(Math.Pow(10, (int)(currentLevel / 10) + 1) * Math.Pow(1.2 + 0.001 * (currentLevel % 10), currentLevel % 10));
            //ExperincePoints = currentLevel * ExperincePointsPerLevel;
            if(class_info && player)
            {
                player.AddStrength(class_info.Class_info[player.GetClassID()].ClassAddStats.atk);
                player.AddDefense(class_info.Class_info[player.GetClassID()].ClassAddStats.def);
                player.AddStamina(class_info.Class_info[player.GetClassID()].ClassAddStats.sta);
                player.AddIntelligence(class_info.Class_info[player.GetClassID()].ClassAddStats.spi);
                player.AddAgility(class_info.Class_info[player.GetClassID()].ClassAddStats.agi);
            }
        }
        if(upgrade_flag && player)
        {
            player.Fully_Update();
        }
    }
    
    //Returns true if skill point was avalible and applied to the chosen skill.
    public bool SpendPoint(ExperiencePointType spendPointOn)
    {
        if(ExperincePoints > ExperincePointUsed)
        {
            if (spendPointOn == ExperiencePointType.Strength)
            {
                gameObject.GetComponent<Player>().SetStrength(gameObject.GetComponent<Player>().GetStrength() + 1);
            }
            if (spendPointOn == ExperiencePointType.Agility)
            {
                gameObject.GetComponent<Player>().SetAgility(gameObject.GetComponent<Player>().GetAgility() + 1);
            }
            if (spendPointOn == ExperiencePointType.Intelligence)
            {
                gameObject.GetComponent<Player>().SetIntelligence(gameObject.GetComponent<Player>().GetIntelligence() + 1);
            }
            if (spendPointOn == ExperiencePointType.PhysicalDefense)
            {
                gameObject.GetComponent<Defense>().SetPhysicalDefense(gameObject.GetComponent<Defense>().GetPhysicalDefense() + 1);
            }
            if(spendPointOn == ExperiencePointType.MagicalDefense)
            {
                gameObject.GetComponent<Defense>().SetMagicalDefense(gameObject.GetComponent<Defense>().GetMagicalDefense() + 1);
            }
            ExperincePointUsed++;
            return true;
        }
        return false;
    }
}
