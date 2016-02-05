using UnityEngine;
using System.Collections;

public class PassiveSkillManager : MonoBehaviour {
    public enum Passives : int
    {
        StrengthBonus = 1,
        HealthBonus
    };

    string Name;
    string Description;


    const int MAX_PASSIVE_SKILLS = 50;
    //PassiveNode[] passives;

    string[] passiveName;
    string[] passiveDescription;
    int[] passiveLevel;
    int[] passiveMaxLevel;
    


    
    public int availablePoints = 1; //Amount of skill points available to use
    
    void Start()
    {
      
        /*
        passives = new PassiveNode[MAX_PASSIVE_SKILLS];
        passives[1] = new strengthBonus();
        */
        passiveLevel = new int[MAX_PASSIVE_SKILLS];
        passiveMaxLevel = new int[MAX_PASSIVE_SKILLS];
        passiveLevel[1] = 0;
        passiveLevel[2] = 0;
        passiveName[1] = "Strength Bonus";
        passiveDescription[1] = "Adds 10 bonus strength";
        passiveMaxLevel[1] = 3;
        passiveMaxLevel[2] = 3;
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown("u"))
        {
            Debug.Log("U PRESSED");
            addPointToSkill(Passives.StrengthBonus);
        }
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("I PRESSED");
            addPointToSkill(Passives.HealthBonus);
        }
        */
    }


    //Attempts to increase level of passive passed in. Upon failure,
    //will return -1 if the user lacks available points and
    //            -2 if the passive is max level
    //             1 upon success
    //            -3 if prerequisite not met
    public int addPointToSkill(Passives passiveID)
    {
        if(availablePoints <= 0)
        {
            Debug.Log("Insufficient points!");
            return -1;
        }

        if (passiveLevel[(int)passiveID] < passiveMaxLevel[(int)passiveID]){
            Debug.Log("Increasing passive level!");
            passiveLevel[(int)passiveID]++;
            availablePoints--;
        }else{
            Debug.Log("Passive has reached max level!");
            return -2;
        }
        

        //Begin very long chain of ugly if statements that are probably far more comprehensable than
        //the equivelent would have been using classes

        if(passiveID == Passives.StrengthBonus)
        {
            Debug.Log("Running StrengthBonus logic!");
        }
        if(passiveID == Passives.HealthBonus)
        {
            //Check for preRequisits
            if (passiveLevel[(int)Passives.StrengthBonus] != 0)
                Debug.Log("Running HealthBonus logic!");
            else
                return -3;
        }
        return 1;
    }

    public void addPoints(int i)
    {
        availablePoints += i;
    }

    public int getAvailablePoints()
    {
        return availablePoints;
    }

    public string getName(int index)
    {
        return passiveName[index];
    }

    public string getDescription(int index)
    {
        return passiveDescription[index];
    }

    public int getLevel(int index)
    {
        return passiveLevel[index];
    }

    public int getMaxLevel(int index)
    {
        return passiveMaxLevel[index];
    }

}

