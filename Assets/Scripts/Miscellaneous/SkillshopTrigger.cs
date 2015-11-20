using UnityEngine;
using System.Collections;

public class SkillshopTrigger : MonoBehaviour {

    private Main_Process mainprocess;
    public int skillClassID; // 1 is weapon, 2 is armor, 3 is accessories
    public int[] skillArray;

    void Start()
    {
        mainprocess = FindObjectOfType<Main_Process>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainprocess.UI_SkillShop_Open(skillClassID, skillArray);
        }
    }
}
