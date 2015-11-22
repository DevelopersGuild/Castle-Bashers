using UnityEngine;
using System.Collections;

public class destroyBarrier : MonoBehaviour {

    private Biome.BiomeName ActiveBiomeName;
    private Main_Process mainprocess;
    private bool E_Dead = false;
    private float left = 40f;
    private int InstanceID;
    GameObject last;

    // Use this for initialization
    void Start()
    {
        mainprocess = FindObjectOfType<Main_Process>();
        for (int i=0; i<AreaGen.AreaNumber; i++) //possibly unstable
        {
            if (gameObject.GetInstanceID() == AreaGen.AreaID[i])
               InstanceID = i;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        int count=0;
        for (int i = 0; i < AreaGen.EnemyNumber[InstanceID]; i++)
        { if (AreaGen.AreaLog[InstanceID, i] != null)
                count++;
            if (count == 0)
                E_Dead = true;
        }   
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && E_Dead==true)
        {
            Destroy(gameObject);
        }
    }
}

