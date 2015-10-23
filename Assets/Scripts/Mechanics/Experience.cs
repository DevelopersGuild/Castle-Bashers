using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{
    private int currentExperince;
    private int currentLevel = 1;
    private int experinceUntilNextLevel = 1000;

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
        currentExperince =+ amountToAdd;
        LevelUp();
    }

    public void LevelUp()
    {
        if(currentExperince > experinceUntilNextLevel)
        {
            experinceUntilNextLevel = experinceUntilNextLevel * 2;
            currentLevel++;
        }
    }

}
