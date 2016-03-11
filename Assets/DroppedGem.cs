using UnityEngine;
using System.Collections;

public class DroppedGem : MonoBehaviour {

    int quality; // 1: normal
                 // 2: Rare
                 // 3: Epic

    Gem gem;
    int gemType;
    Player player;
    public Sprite blueGem;
    public Sprite greenGem;
    public Sprite redGem;
    private SpriteRenderer sr;
    private Sprite currentSprite;

	// Use this for initialization
	void Start () {
        int qualityRoll = Random.Range(1, 11); //randomizes normal, rare, epic. Will change to incorporate percent chance of each
        if (qualityRoll == 10)    //10% chance for epic
            quality = 3;
        else if (qualityRoll >= 7)   //30% chance for  rare
            quality = 2;
        else                    // 60% chance for normal
            quality = 1;

	    sr = GetComponent<SpriteRenderer>();

        if(quality >= 2)
        {
            gemType = Random.Range(1, 6); // If rare or epic, can roll skill gem
        }
        else
        {
            gemType = Random.Range(1, 5);
        }

	    if (quality == 1)
	    {
            currentSprite = greenGem;
	    }else if (quality == 2)
	    {
            currentSprite = redGem;
        }
	    else
	    {
            currentSprite = blueGem;
        }

	    sr.sprite = currentSprite;

        //Roll stats for gem
        Debug.Log("Rolled quality " + quality);
	}

    void OnTriggerEnter(Collider col)
    {

        if (player = col.gameObject.GetComponent<Player>())
        {
            Debug.Log("Detected the collision!");
            PickedUp(player);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //include setters for manually setting stats

    void PickedUp(Player player)
    {

        switch (gemType)
        {
            case 1:
                gem = new StrengthGem();
                break;
            case 2:
                gem = new AgilityGem();
                break;
            case 3:
                gem = new IntelligenceGem();
                break;
            case 4:
                gem = new HealthGem();
                break;
            case 5:
                gem = new ManaGem();
                break;
            case 6:
                Debug.Log("Would be skill Gem");
                //insert skill gem here when done
                break;
        }
        gem.setOwner(player);

        gem.Initialize(quality);  //MUST CALL. think of this as running the Start() function, but Gem does not inherit from monobehaviour. Its its own man, taking orders from no base class.
        Debug.Log(gem.getDescription());
        //Set stats for gem before adding to play

        player.GetComponent<GemManager>().addGem(gem);
        Destroy(gameObject);
    }
}
