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

    GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
        
        Camera camera = Camera.main;
        cameraFollow = camera.GetComponent<CameraFollow>();
        mainprocess = FindObjectOfType<Main_Process>();

        
        InstanceID = CreateStart.AreaID[CreateStart.roomCount-1];
        //Debug.Log("InstanceID: " + InstanceID + ", " + (CreateStart.roomCount-1));

        room = FindObjectOfType<CreateStart>().GetComponent<CreateStart>();

        if(CreateStart.roomCount!=1)
        room.MakeMob(CreateStart.roomCount);

        if (CreateStart.squadSize == 0)
            E_Dead = true;

        
        enemies = new GameObject[room.Max_enemy];
        for (int j = 0; j < CreateStart.squadSize; j++)
        {
            Debug.Log(room.AreaLog[j]);
        }
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
                if (room.AreaLog[i] == null)
                {
                    count--;
                }
                else
                {
                    Debug.Log("Still here");
                    i = count = CreateStart.squadSize;
                    // for (int j = 0; j < CreateStart.squadSize; j++) ;
                    //enemies[j]=GameObject.Find(room.AreaLog[j]).GetInstanceID().ToString();
                }
            }
            if (count == 0)
                E_Dead = true;

        }

        }   
        // Debug.Log("A Gen number=" + AreaGen.EnemyNumber[InstanceID]);

   

    void OnTriggerEnter(Collider other)
    {
        if (E_Dead==true)
        {
            if (CreateStart.roomCount != CreateStart.numRoom)
            {
                GameObject background = (GameObject)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName, 0], typeof(GameObject));
                room.MakeRoom(CreateStart.roomCount, background);

            }
            else
                end.UI_Mission_Success_Open();
            Destroy(gameObject);
            cameraFollow.setLock(false);

            
        }
        CreateStart.roomCount++;
    }
}

