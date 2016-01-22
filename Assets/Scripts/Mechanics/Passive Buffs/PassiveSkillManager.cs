using UnityEngine;
using System.Collections;

/*
class PassiveNode : MonoBehaviour
{
    protected Player player;
    int level;
    int maxLevel;
    string description;
    
    protected virtual void Start()
    {
        player = GetComponent<Player>();
    }
    

    public PassiveNode(int maxLvl, string descript)
    {
        level = 0;
        maxLevel = maxLvl;
        description = descript;
    }
    
    virtual public bool addPoint()
    {
        if (level < maxLevel)
        {
            level++;
            return true;
        }
        return false;
    }

    //Use a high number if you wish to clear all points, if the points
    //go below 0, they will be set to 0.
    public void removePoint(int i)
    {
        level -= i;
        if(level < 0)
            level = 0;
    }

    public int getLevel()
    {
        return level;
    }

    
}
*/

    /*
class strengthBonus : PassiveNode
{
    public strengthBonus():base(3, "Adds 10 strength per level")
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool addPoint()
    {
        //Check general add point logic
        if (base.addPoint())
        {
            //Logic related to specific skill
            player.AddStrength(10);
            return true;
        }

        return false;
    }

}

    */

public class PassiveSkillManager : MonoBehaviour {
    public enum Passives : int
    {
        StrengthBonus = 1,
        HealthBonus
    };



    const int MAX_PASSIVE_SKILLS = 50;
    //PassiveNode[] passives;

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
        passiveMaxLevel[1] = 3;
    }

    void update()
    {
        if (Input.GetKeyDown("U")){
            Debug.Log("U PRESSED");
            addPointToSkill(Passives.StrengthBonus);
        }
    }


    //Attempts to increase level of passive passed in. Upon failure,
    //will return -1 if the user lacks available points and
    //            -2 if the passive is max level
    //             1 upon success
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
        }
        else
        {
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
            Debug.Log("Running HealthBonus logic!");
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
}

