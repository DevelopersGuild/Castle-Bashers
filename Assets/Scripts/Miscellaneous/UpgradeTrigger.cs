using UnityEngine;
using System.Collections;

public class UpgradeTrigger : MonoBehaviour {

    private Main_Process mainprocess;
    public int upgradeID; // 1 is weapon, 2 is armor, 3 is accessories

    void Start()
    {
        mainprocess = FindObjectOfType<Main_Process>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainprocess.UI_Upgrade_Window_Open(upgradeID);
        }
    }
}
