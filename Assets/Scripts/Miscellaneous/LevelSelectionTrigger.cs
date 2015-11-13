using UnityEngine;
using System.Collections;

public class LevelSelectionTrigger : MonoBehaviour {

    private Main_Process mainprocess;
    public int levelToOpen;

    void Start()
    {
        mainprocess = FindObjectOfType<Main_Process>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainprocess.UI_Level_Selector_Open(levelToOpen);
        }
    }
}
