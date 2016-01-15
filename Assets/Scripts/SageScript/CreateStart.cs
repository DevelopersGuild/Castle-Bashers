using UnityEngine;
using System.Collections;

public class CreateStart : MonoBehaviour {

    public int Min_Room; //cannot be less than 1
    public int Max_Room; //cannot be less than 1
    public int Min_enemy; //cannot be negative
    public int Max_enemy; //cannot be negative
    public int Min_Objects; //nonnegative
    public int Max_Objects; //nonnegative
    public int Min_Paths; //nonnegative
    public int Max_Paths; //nonnegative

    public static int numRoom;
    int numEnemy;
    int numObj;
    int numPath;

    static public int[] AreaID;
    static public int roomCount = 0;

    public static Biome.BiomeName ActiveBiomeName;

    private System.Random rnd;

    public void MakeRoom(int roomC, GameObject bg)
    //makes a room

    {
        Debug.Log("crab");
        int AreaXCoord = 0;
        int AreaYCoord = 1;
        int AreaZCoord = 1;
        Instantiate(Resources.Load("LevelObjects/3DFloorB", typeof(GameObject)), new Vector3((AreaXCoord + roomC) * 40, AreaYCoord, AreaZCoord), transform.rotation);
        Instantiate(Resources.Load("LevelObjects/Front Limit", typeof(GameObject)), new Vector3((AreaXCoord + roomC) * 40, AreaYCoord, 11), transform.rotation); //set front limits
        Instantiate(Resources.Load("LevelObjects/Back Limit", typeof(GameObject)), new Vector3((AreaXCoord + roomC) * 40, AreaYCoord, -8), transform.rotation); //set back limits
        if (bg!=null)
            Instantiate(bg, new Vector3((AreaXCoord + roomC) * 40, 5, 13), transform.rotation);

        Instantiate(Resources.Load("LevelObjects/Right Limit", typeof(GameObject)), new Vector3((AreaXCoord + roomC) * 40 + 20, 0, 0), transform.rotation).GetInstanceID();
    }

	// Use this for initialization
	void Start () {

    ///Preconditions///
	if (Min_Room<1 || Max_Room<1)
        {
            Debug.Log("Error at Min/max area! Out of range");
            return;
        }

    if (Min_enemy<-1
    || Max_enemy<-1
    || Min_Objects<-1
    || Max_Objects<-1
    || Min_Paths<-1
    || Max_Paths<-1)
        {
            Debug.Log("Error at Enemy, Object, Path count! Out of range");
            return;
        }

        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());

        GameObject background = (GameObject)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName, 0], typeof(GameObject));

        //Gen nums
        numRoom = rnd.Next(Min_Room, Max_Room);
        numPath = rnd.Next(Min_Paths, Max_Paths);

        //create first room
        Instantiate(Resources.Load("LevelObjects/Left Limit", typeof(GameObject)), new Vector3(-5, 0, 0), transform.rotation);
        MakeRoom(roomCount, background);
        roomCount++;
        Debug.Log(roomCount);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
