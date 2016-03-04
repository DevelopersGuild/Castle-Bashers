using UnityEngine;
using System.Collections;

public class PassiveSkillManager : MonoBehaviour {
    public enum Passives : int
    {
        StrengthBonus = 0,
        AgilityBonus,
        IntelligenceBonus,
        HealthBonus,
        ManaBonus,
    };

    string Name;
    string Description;

    Player player;
    Health health;
    Mana mana;


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
        passiveName = new string[MAX_PASSIVE_SKILLS];
        passiveDescription = new string[MAX_PASSIVE_SKILLS];

        passiveName[(int)Passives.StrengthBonus] = "Strength Bonus";
        passiveName[(int)Passives.AgilityBonus] = "Agility Bonus";
        passiveName[(int)Passives.IntelligenceBonus] = "Intelligence Bonus";
        passiveName[(int)Passives.HealthBonus] = "Health Bonus";
        passiveName[(int)Passives.ManaBonus] = "Mana Bonus";

        passiveDescription[(int)Passives.StrengthBonus] = "Adds 5% bonus Strength per level";
        passiveDescription[(int)Passives.AgilityBonus] = "Adds 5% bonus Agility per level";
        passiveDescription[(int)Passives.IntelligenceBonus] = "Adds 5% bonus Intelligence per level";
        passiveDescription[(int)Passives.HealthBonus] = "Adds 15% bonus Health per level";
        passiveDescription[(int)Passives.ManaBonus] = "Adds 20% bonus Mana per level";
        //passiveMaxLevel[0] = 3;
        //passiveMaxLevel[1] = 3;

        for (int i = 0; i < MAX_PASSIVE_SKILLS; i++)
        {
            passiveLevel[i] = 0;           //Initialize current level to 0   (I think theres a more efficient way to do this, not sure in C#)
            passiveMaxLevel[i] = 3;      //All passives set to 3 for now. can change later
        }

        player = GetComponent<Player>();
        health = player.GetComponent<Health>();
        mana = player.GetComponent<Mana>();
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
        switch (passiveID)
        {
            case Passives.StrengthBonus:
                player.AddBonusPercentStrength(5);    //5% strength per level
                break;

            case Passives.AgilityBonus:
                player.AddBonusPercentAgility(5);     //5% agility per level
                break;
            case Passives.IntelligenceBonus:
                player.AddBonusIntelligence(5);         //5% intelligence per level
                break;
            case Passives.HealthBonus:
                //Check for preRequisits
                if (passiveLevel[(int)Passives.StrengthBonus] > 0)
                {
                    health.addBonusPercentHP(15);      //15% health per level
                    Debug.Log("Running HealthBonus logic!");
                }
                else
                    return -3;
                
                break;
            case Passives.ManaBonus:
                mana.addBonusPercentMana(20);       //20% mana per level
                break;

                            
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

