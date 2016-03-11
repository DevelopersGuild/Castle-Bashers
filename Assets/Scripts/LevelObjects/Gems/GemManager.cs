using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gem
{
    protected bool active = false;
    protected Player player;
    protected string gemName = "";
    protected Sprite gemIcon;
    protected int gemType; // 1: ATK 2. DEF 3.SUP 4.ADS
    protected string description;
    protected int quality = 0; // 1: Normal 2: Rare  3: 
    protected int bonus;

    public virtual void Initialize(int q)   //Be sure to always call base.Initialize in the Initialize function of every gem
    {
        quality = q;
    }

    public virtual void onUpdate()
    {
        //Be sure to check if active in the update frames
    }

    public virtual void activate()
    {
        //Perform checks on the active field before running
        //any activate code to prevent it from running the
        //effects twice

    }

    public virtual void deactivate()
    {
       //Perform checks on the active field before running
       //any deactivate code to prevent it from reverting the
       //effects twice
    }

    public void setQuality(int q)
    {
        quality = q;
    }

    public void setOwner(Player p)
    {
        player = p;
    }

    public void setGemType(int type)
    {
        gemType = type;
    }

    public void setGemIcon(Sprite icon)
    {
        gemIcon = icon;
    }

    public string getName() { return gemName; }
    public string getDescription() { return description; }
    public int getQuality() { return quality; }

    public int getGemType() { return gemType; }

    public Sprite getGemIcon() { return gemIcon; }

    public string GetDescription() { return description; }
}




public class GemManager : MonoBehaviour {
    const int MAX_STORED_GEMS = 10;
    const int MAX_EQUIPPED_GEMS = 3;
    
    int numGems = 0;

    List<Gem> storedGems = new List<Gem>();
    List<Gem> equippedGems = new List<Gem>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("u"))
        {
            Debug.Log("U PRESSED");
            equip(0);
            
        }
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("I PRESSED");
            unequip(0);
        }

        for (int i = 0; i < MAX_EQUIPPED_GEMS && i < equippedGems.Count; i++)
        {
            if(equippedGems[i] != null)
            {
                equippedGems[i].onUpdate();
            }
        }
	}


    //Will return 1 upon success, 0 if there is no room
    public int equip(int index)
    {
        Debug.Log("Made it into equip");
        //Try to find an open slot

        if(equippedGems.Count < MAX_EQUIPPED_GEMS)
        {
            storedGems[index].activate();
            equippedGems.Add(storedGems[index]);
            removeGem(index);
            return 1;
        }
        else
        {
            return 0;  /// no room.
        }

        //for(int i = 0; i < MAX_EQUIPPED_GEMS; i++)
        //{
        //    //if (equippedGems[i] == null)
        //    {
        //        equippedGems.Add(storedGems[index]);
        //        removeGem(index);
        //        equippedGems[i].activate();
        //        return 1;
        //    }
        //}

        return 0;
    }

    public bool unequip(int index)
    {
        if(equippedGems[index]==null)
        {
            return false;
        }
        equippedGems[index].deactivate();
        addGem(equippedGems[index]);
        equippedGems.RemoveAt(index);
        
        return true;
    }


    //Will sort the array after removing a gem from the array
    public void removeGem(int index)
    {
        //storedGems[index] = storedGems[numGems];   //Move last gem into old gem position before sorting. might be able to find a better solution.
        //if (index == numGems)
        //{
        storedGems.RemoveAt(index); //Handle special case that the gem being removed is the last gem
                                      //}

        numGems--;
        sortGems();
        //SORT GEMS
    }

    
    //Attempts to add a gem to the inventory
    //This function assumes the array is sorted
    //Returns 0 if inventory is full
    public int addGem(Gem gem)
    {
        if(numGems <= MAX_STORED_GEMS)
        {
            storedGems.Add(gem);
            numGems++;
            Debug.Log("Stored " + gem.getName() + " in index " + numGems);
            sortGems();
        }
        else
        {
            return 0; //Give error message
        }

        return 1;
    }

    private void sortGems()
    {
        for(int i = 0; i < numGems; i++)
        {
            if(storedGems[i] == null)
            {
                for(int j = i; j < numGems - i; j++)
                {
                    storedGems[j] = storedGems[j + 1];
                }
                storedGems[numGems] = null;
            }
        }
    }
    
    public Gem GetEquippedGem(int index)
    {
        return equippedGems[index];
    }

    public List<Gem> GetEquippedGems()
    {
        return equippedGems;
    }

    public Gem GetStoredGem(int index)
    {
        return storedGems[index];
    }

    public List<Gem> GetStoredGems()
    {
        return storedGems;
    }
}
