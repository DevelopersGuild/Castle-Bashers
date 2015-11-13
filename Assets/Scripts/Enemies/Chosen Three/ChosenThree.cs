using UnityEngine;
using System.Collections;

public class ChosenThree : MonoBehaviour {

    public GameObject warriorObj;
    public GameObject mageObj;
    public GameObject archerObj;

    private GameObject warrior;
    private GameObject mage;
    private GameObject archer;

    private bool beenNotifiedWarrior = false;
    private bool beenNotifiedArcher = false;
    private bool beenNotifiedMage = false;

	// Use this for initialization
	void Start () {
        warrior = Instantiate(warriorObj, gameObject.transform.position, Quaternion.identity) as GameObject;
        mage = Instantiate(mageObj, gameObject.transform.position, Quaternion.identity) as GameObject;
        archer = Instantiate(archerObj, gameObject.transform.position, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (!warrior && !beenNotifiedWarrior)
        {
            if(mage)
                mage.GetComponent<ChosenMage>().NotifyWarriorDeath();
            if(archer)
                archer.GetComponent<ChosenArcher>().NotifyWarriorDeath();
            beenNotifiedWarrior = true;
            
        }
        if (!mage && !beenNotifiedMage)
        {
            if(warrior)
                warrior.GetComponent<ChosenWarrior>().NotifyMageDeath();
            if(archer)
                archer.GetComponent<ChosenArcher>().NotifyMageDeath();
            beenNotifiedMage = true;
        }
        if (!archer && !beenNotifiedArcher)
        {
            if(warrior)
                warrior.GetComponent<ChosenWarrior>().NotifyArcherDeath();
            if(mage)
                mage.GetComponent<ChosenMage>().NotifyArcherDeath();
            beenNotifiedArcher = true;
        }
        if(!archer && !mage && !warrior)
        {
            Destroy(gameObject);
        }
	}

}
