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
    private System.Random rnd;
    int Wavechance;

    GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        Camera camera = Camera.main;
        cameraFollow = camera.GetComponent<CameraFollow>();
        mainprocess = FindObjectOfType<Main_Process>();

        
        InstanceID = CreateStart.AreaID[CreateStart.roomCount-1];
        //Debug.Log("InstanceID: " + InstanceID + ", " + (CreateStart.roomCount-1));

        room = FindObjectOfType<CreateStart>().GetComponent<CreateStart>();

        if (CreateStart.roomCount != 1) //never create a mob in the first room
        {
            room.MakeMob(CreateStart.roomCount);
            room.MakeObjects(CreateStart.roomCount);
        }

        if (CreateStart.squadSize == 0)
            E_Dead = true;

        end = GameObject.Find("Main Process").GetComponent<Main_Process>();
    }

    // Update is called once per frame
    void Update ()
    {
        int count=CreateStart.squadSize;

        if (E_Dead == false)
        {
            for (int i = 0; i < CreateStart.squadSize; i++)
            {
                if (room.AreaLog[i] == null)
                    count--;
                else
                    i = count = CreateStart.squadSize;
            }
            if (count == 0)
            {
                E_Dead = true;
                if (CreateStart.SecondWave == true)
                {
                    room.MakeMob(CreateStart.roomCount);
                    E_Dead = false;
                    Wavechance = rnd.Next(1, 100);
                    CreateStart.SecondWave = false;
                    Debug.Log("WAVECHANCE:" + Wavechance);

                    if (CreateStart.SecondWaveChance>Wavechance)
                      {
                          CreateStart.SecondWave = true; }
                    else
                    CreateStart.SecondWave = false;

                }

            }

        }
    }   
        // Debug.Log("A Gen number=" + AreaGen.EnemyNumber[InstanceID]);

   

    void OnTriggerStay(Collider other)
    {
        if (E_Dead==true)
        {

                if (CreateStart.roomCount != CreateStart.numRoom)
                {
                    GameObject background = (GameObject)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName, 0], typeof(GameObject));
                    room.MakeRoom(CreateStart.roomCount, background);
                    CreateStart.roomCount++;
                }
                else
                    end.UI_Mission_Success_Open();
 
            Destroy(gameObject);
            cameraFollow.setLock(false);

            
        }
      
    }
}

