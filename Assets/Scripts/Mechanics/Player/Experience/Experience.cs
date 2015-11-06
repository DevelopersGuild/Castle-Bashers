using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{
    public int currentLevel = 1;
    public int ExperincePointsPerLevel = 2;
    private int ExperincePoints;
    private int ExperincePointUsed = 0;
    private int currentExperince;
    private int experinceUntilNextLevel = 1000;
    
    void Start()
    {
        ExperincePoints = currentLevel * ExperincePointsPerLevel;
    }

    public int GetExperience()
    {
        return currentExperince;
    }

    public int GeCurrentLevel()
    {
        return currentLevel;
    }

    public void AddExperince(int amountToAdd)
    {
        currentExperince += amountToAdd;
        LevelUp();
    }

    public void LevelUp()
    {
        if(currentExperince >= experinceUntilNextLevel)
        {
            experinceUntilNextLevel = currentExperince * 2;
            currentLevel++;
            ExperincePoints = currentLevel * ExperincePointsPerLevel;
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
