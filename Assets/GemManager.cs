using UnityEngine;
using System.Collections;


public class Gem
{
    protected bool active = false;
    protected Player player;
    protected string gemName = "";
    protected Sprite gemIcon;
    protected int gemType; // 1: ATK 2. DEF 3.SUP 4.ADS
    protected string description;
    protected int quality; // 1: Normal 2: Rare  3: Epic

    public virtual void Start()
    {
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
    public int getQuality() { return quality; }

    public int getGemType() { return gemType; }

    public Sprite getGemIcon() { return gemIcon; }

    public string GetDescription() { return description; }
}



class StrengthGem : Gem
{

    public override void Start()
    {
        base.Start();
        gemName = "Strength Gem";
        description = "Increases Strength by 10";
        gemType = 2;
    }

    public override void activate()
    {
        if (active == false)
        {
            Debug.Log(player.GetStrength());
            player.AddStrength(10);
            Debug.Log("Player gained 10 str!");
            Debug.Log(player.GetStrength());
            active = true;
        }
    }
    public override void deactivate()
    {
        if(active == true)
        {
            player.AddStrength(-10);
            active = false;
        }
    }

}

class AgilityGem : Gem
{

    public override void Start()
    {
        base.Start();
        gemName = "Strength Gem";
        description = "Increases Strength by 10";
        gemType = 3;
    }

    public override void activate()
    {
        if (active == false)
        {
            player.AddAgility(10);
            active = true;
        }
    }
    public override void deactivate()
    {
        if (active == true)
        {
            player.AddStrength(-10);
            active = false;
        }
    }
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

    public bool unequip(int index)
    {
        if(equippedGems[index]==null)
        {
            return false;
        }
        equippedGems[index].deactivate();
        addGem(equippedGems[index]);
        equippedGems[index] = null;
        return true;
    }


    //Will sort the array after removing a gem from the array
    public void removeGem(int index)
    {
        storedGems[index] = null;
        numGems--;
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
            numGems++;
        }
        else
        {
            return 0;
        }

        return 1;
    }
    
    public Gem GetEquippedGem(int index)
    {
        return equippedGems[index];
    }

    public Gem[] GetEquippedGems()
    {
        return equippedGems;
    }

    public Gem GetStoredGem(int index)
    {
        return storedGems[index];
    }

    public Gem[] GetStoredGems()
    {
        return storedGems;
    }
}
