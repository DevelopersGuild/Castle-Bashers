using UnityEngine;
using System.Collections;

public class EndOfLevelTrigger : MonoBehaviour {

    public int levelToLoad;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TriggerEndOfLevel(levelToLoad);
        }
    }

    void TriggerEndOfLevel(int level)
    {
        Application.LoadLevel(level);
    }
}
