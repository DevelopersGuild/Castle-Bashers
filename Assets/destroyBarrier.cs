using UnityEngine;
using System.Collections;

public class destroyBarrier : MonoBehaviour {

    private Biome.BiomeName ActiveBiomeName;
    private Main_Process mainprocess;
    private bool E_Dead = false; //change back!
    private float left = 40f;
    private int InstanceID;
    private CameraFollow cameraFollow;
    public CreateStart room;
    public Main_Process end;

    // Use this for initialization
    void Start()
    {
        
        Camera camera = Camera.main;
        cameraFollow = camera.GetComponent<CameraFollow>();
        mainprocess = FindObjectOfType<Main_Process>();

        
        InstanceID = CreateStart.AreaID[CreateStart.roomCount-1];
        Debug.Log("InstanceID: " + InstanceID + ", " + (CreateStart.roomCount-1));
        

       if (CreateStart.squadSize == 0)
            E_Dead = true;

        room = FindObjectOfType<CreateStart>().GetComponent<CreateStart>();
        end = GameObject.Find("Main Process").GetComponent<Main_Process>();
    }

    // Update is called once per frame
    void Update ()
    {
        int count=CreateStart.squadSize;

        //if (!E_Dead)
        //{
        //    cameraFollow.setLock(true);
        //}

        //Debug.Log(CreateStart.EnemyNumber[CreateStart.squadSize]);
        if (E_Dead == false)
        {
            //Debug.Log(CreateStart.squadSize);
            //Debug.Log((CreateStart.AreaLog[CreateStart.squadSize, 1]));
            for (int i = 0; i < CreateStart.squadSize; i++)
            {
                if (room.AreaLog[i] == 0)
                {
                    count--;
                }
                else
                {
                    Debug.Log("I: " + i + " SquadSize: " + room.AreaLog[i]);
                    count = CreateStart.squadSize; }
            }
                if (count == 0)
                    E_Dead = true;
            
        }
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
                room.MakeMob(CreateStart.roomCount);
            }
            else
                end.UI_Mission_Success_Open();
            Destroy(gameObject);
            cameraFollow.setLock(false);

            
        }
        CreateStart.roomCount++;
    }
}

