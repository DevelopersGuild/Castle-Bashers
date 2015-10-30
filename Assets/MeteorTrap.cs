using UnityEngine;
using System.Collections;

public class MeteorTrap : MonoBehaviour {
    private ISkill meteor;
	// Use this for initialization
	void Start () {
        meteor = new sMeteor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            meteor.UseSkill(gameObject, col.gameObject);
        }
    }
}
