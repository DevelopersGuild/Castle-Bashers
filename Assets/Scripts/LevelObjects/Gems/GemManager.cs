using UnityEngine;
using System.Collections;


public class Gem
{
    protected bool active = false;
    protected Player player;
    protected string gemName = "";
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

    public string getName() { return gemName; }
    public string getDescription() { return description; }
    public int getQuality() { return quality; }
}






public class GemManager : MonoBehaviour {
    const int MAX_STORED_GEMS = 10;
    const int MAX_EQUIPPED_GEMS = 3;
    int count = 5;

    Gem[] storedGems;
    Gem[] equippedGems;
    int numGems = 0;

	// Use this for initialization
	void Start () {
        storedGems = new Gem[MAX_STORED_GEMS];
        equippedGems = new Gem[MAX_EQUIPPED_GEMS];
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("u"))
        {
            Debug.Log("U PRESSED");
            storedGems[0].activate();
            count--;
        }
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("I PRESSED");
            storedGems[0].deactivate();
        }

        for (int i = 0; i < MAX_EQUIPPED_GEMS; i++)
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
        //Try to find an open slot
        for(int i = 0; i < MAX_STORED_GEMS; i++)
        {
            if(equippedGems[i] == null)
            {
                equippedGems[i] = storedGems[index];
                removeGem(index);
                equippedGems[i].activate();
                return 1;
            }
        }

        return 0;
    }


    //Will sort the array after removing a gem from the array
    public void removeGem(int index)
    {
        storedGems[index] = storedGems[numGems];   //Move last gem into old gem position before sorting. might be able to find a better solution.
        if (index == numGems)
        {
            storedGems[index] = null; //Handle special case that the gem being removed is the last gem
        }

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
            storedGems[numGems] = gem;
            Debug.Log("Stored " + gem.getName() + " in index " + numGems);
            numGems++;
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
            for(int j = i; j < numGems - i - 1; j++)
            {
                
                if(storedGems[i].getQuality() > storedGems[j].getQuality())
                {
                    Gem temp = storedGems[i];
                    storedGems[i] = storedGems[j];
                    storedGems[j] = temp;

                }
            }
        }
    }
    

}
