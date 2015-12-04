using UnityEngine;
using System.Collections;

public class destroyBarrier : MonoBehaviour {

    private Biome.BiomeName ActiveBiomeName;
    private Main_Process mainprocess;
    private bool E_Dead = false;
    private float left = 40f;
    private int InstanceID;
    private CameraFollow cameraFollow;
    GameObject last;

    // Use this for initialization
    void Start()
    {
        Camera camera = Camera.main;
        cameraFollow = camera.GetComponent<CameraFollow>();
        mainprocess = FindObjectOfType<Main_Process>();
        for (int i=0; i<AreaGen.AreaNumber; i++) //possibly unstable
        {
            if (gameObject.GetInstanceID() == AreaGen.AreaID[i])
               InstanceID = i;
        }

        if (AreaGen.EnemyNumber[InstanceID] == 0)
            E_Dead = true;
    }

    // Update is called once per frame
    void Update ()
    {
        int count=0;

        //if (!E_Dead)
        //{
        //    cameraFollow.setLock(true);
        //}


        //for (int i = 0; i < AreaGen.EnemyNumber[InstanceID]; i++)
        //{
        //    if (AreaGen.AreaLog[InstanceID, i] != null)
        //        count++;
        //    if (count == 0)
        //        E_Dead = true;
        //}   
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && E_Dead==true)
        {
            Destroy(gameObject);
            cameraFollow.setLock(false);
        }
    }
}

