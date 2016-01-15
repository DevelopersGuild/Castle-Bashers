using UnityEngine;
using System.Collections;

public class destroyBarrier : MonoBehaviour {

    private Biome.BiomeName ActiveBiomeName;
    private Main_Process mainprocess;
    private bool E_Dead = true; //change back!
    private float left = 40f;
    private int InstanceID;
    private CameraFollow cameraFollow;
    public CreateStart room;

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

       // if (AreaGen.EnemyNumber[InstanceID] == 0)
            E_Dead = true;

        room = FindObjectOfType<CreateStart>().GetComponent<CreateStart>();
    }

    // Update is called once per frame
    void Update ()
    {
        int count=0;

        //if (!E_Dead)
        //{
        //    cameraFollow.setLock(true);
        //}


       // for (int i = 0; i < AreaGen.EnemyNumber[InstanceID]; i++)
       // {
       //     if (AreaGen.AreaLog[InstanceID, i] != null)
        //        count++;
       //    if (count == 0)
       //         E_Dead = true;
      //  }   
     //   Debug.Log("A Gen number=" + AreaGen.EnemyNumber[InstanceID]);
    }

    void OnTriggerEnter(Collider other)
    {
        if (E_Dead==true)
        {
            if (CreateStart.roomCount != CreateStart.numRoom)
            {
                GameObject background = (GameObject)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName, 0], typeof(GameObject));
                room.MakeRoom(CreateStart.roomCount, background);
            }
            Destroy(gameObject);
            cameraFollow.setLock(false);
        }
        CreateStart.roomCount++;
    }
}

