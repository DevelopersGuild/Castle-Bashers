using UnityEngine;
using System.Collections;

public class ChosenThree : MonoBehaviour {

    public GameObject warriorObj;
    public GameObject mageObj;
    public GameObject archerObj;

    private GameObject warrior;
    private GameObject mage;
    private GameObject archer;

    private bool tankBuffActive;
    private bool speedBuffActive;
    private bool castBuffActive;

	// Use this for initialization
	void Start () {
        warrior = Instantiate(warriorObj, gameObject.transform.position, Quaternion.identity) as GameObject;
        tankBuffActive = true;
        mage = Instantiate(mageObj, gameObject.transform.position, Quaternion.identity) as GameObject;
        castBuffActive = true;
        archer = Instantiate(archerObj, gameObject.transform.position, Quaternion.identity) as GameObject;
        speedBuffActive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!warrior)
        {
            tankBuffActive = false;
        }
        if (!mage)
        {
            Debug.Log("Mage has died");
            castBuffActive = false;
        }
        if (!archer)
        {
            speedBuffActive = false;
        }
	}

    public bool isTankBuffActive()
    {
        return tankBuffActive;
    }
    public bool isCastBuffActive()
    {
        return castBuffActive;
    }
    public bool isSpeedBuffActive()
    {
        return speedBuffActive;
    }
}
