using UnityEngine;
using System.Collections;

public class CreateStart : MonoBehaviour {

    const double DOORCHANCE = 0.01;

    public int Min_Room; //cannot be less than 1
    public int Max_Room; //cannot be less than 1
    public int Min_enemy; //cannot be negative 
    public int Max_enemy; //cannot be negative
    public int Min_Objects; //nonnegative
    public int Max_Objects; //nonnegative
    public int Min_Paths; //nonnegative
    public int Max_Paths; //nonnegative
    static public int position; //between 0 and 3
    public int difficulty;

    public static int numRoom;
    int numEnemy;
    int numObj;
    int numPath;

    static public int roomCount = 0;
    static public int squadSize;
    static public int[] AreaID;
    static public int[] EnemyNumber; //unique ID for each enemy. Used for E_dead check
    public GameObject[] AreaLog;

    public enum types { front, back, top, surprise }

    public Biome.BiomeName ActiveBiomeName;

    private System.Random rnd;

    public void EnemyScale(GameObject Enemy)
    {
        DealDamage.setDamage(DealDamage.getDamage()*100f);
    }

    public void MakeRoom(int roomC, GameObject bg)
    //makes a room

    {
        int AreaXCoord = -20;
        int AreaYCoord = 1;
        int AreaZCoord = 1;
        Instantiate(Resources.Load("LevelObjects/3DFloorB", typeof(GameObject)), new Vector3((AreaXCoord) + (40* roomC), AreaYCoord, AreaZCoord), transform.rotation);
        Instantiate(Resources.Load("LevelObjects/Front Limit", typeof(GameObject)), new Vector3((AreaXCoord) + (40 * roomC), AreaYCoord, 9), transform.rotation); //set front limits
        Instantiate(Resources.Load("LevelObjects/Back Limit", typeof(GameObject)), new Vector3((AreaXCoord) + (40 * roomC), AreaYCoord, -8), transform.rotation); //set back limits

        double doorRoll = rnd.NextDouble();

        if (doorRoll >= DOORCHANCE)
        {
            Instantiate(Resources.Load("LevelObjects/Door", typeof(GameObject)), new Vector3( AreaXCoord+ (40 * roomC), AreaYCoord, 8), Quaternion.Euler(90,0,0)); 
        }


        if (bg!=null)
        Instantiate(bg, new Vector3((AreaXCoord + roomC) * 40, 5, 13), transform.rotation);

        AreaID[roomC]= Instantiate(Resources.Load("LevelObjects/Right Limit", typeof(GameObject)), new Vector3((AreaXCoord) + (40 * roomC)+20, 0, 0), transform.rotation).GetInstanceID();
    }

    public void MakeObjects(int room)
    {
        if (Min_Objects == 0 || Max_Objects == 0)
            return;

        else
        {
            numObj = rnd.Next(Min_Objects, Max_Objects);
            int[] ObjectTypeArray = new int[numObj];
            double[] X_coord = new double[numObj];
            double[] Z_coord = new double[numObj];

            for (int m = 0; m < numObj; m++) //creates object types
            {
                int testType = rnd.Next(0, 3);
                ObjectTypeArray[m] = testType;
                // Debug.Log("ObjectTypeArray at value" + m + "::" + ObjectTypeArray[m]);
            }

            double testtemp;
            for (int m = 0; m < numObj; m++) //This loop gets us our X coordinates
            {
                testtemp = rnd.NextDouble(); //our X value
                for (int n = 0; n < numObj; n++)//dummy test
                {
                    if (X_coord[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = numObj;
                    }
                    if (n == numObj - 1)
                        X_coord[m] = testtemp;
                }
            }

            for (int m = 0; m < numObj; m++) //This loop gets us our Z coordinates
            {
                testtemp = rnd.NextDouble(); //our Z value
                for (int n = 0; n < numObj; n++) //dummy test
                {
                    if (Z_coord[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = numObj;
                    }
                    if (n == numObj - 1)
                        Z_coord[m] = testtemp;
                }
            }

            GameObject temp;

            for (int m = 0; m < numObj; m++)
            {
                //Debug.Log("Created enemy number: " + squadSize + " succesffuly!");
                temp = (GameObject)Resources.Load((string)Biome.Objects[(int)ActiveBiomeName, ObjectTypeArray[m]], typeof(GameObject));

                if (temp != null)
                    spawn(-1, temp, X_coord[m], Z_coord[m]);
            }
        }
    }

    public void MakeMob(int room)
    {
        if (Min_enemy == 0 || Max_enemy == 0)
        {
            return; }
        else
        {
            squadSize = rnd.Next(Min_enemy, Max_enemy);
            int[] EnemyTypeArray = new int[squadSize];
            double[] X_coord = new double[squadSize];
            double[] Z_coord = new double[squadSize];

            for (int m = 0; m < squadSize; m++) //creates enemy types
            {
                int testType = rnd.Next(0, 2);
                EnemyTypeArray[m] = testType;
                // Debug.Log("EnemyTypeArray at value" + m + "::" + EnemyTypeArray[m]);
            }

            double testtemp;
            for (int m = 0; m < squadSize; m++) //This loop gets us our X coordinates
            {
                testtemp = rnd.NextDouble(); //our X value
                for (int n = 0; n < squadSize; n++)//dummy test
                {
                    if (X_coord[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = squadSize;
                    }
                    if (n == squadSize - 1)
                        X_coord[m] = testtemp;
                }
            }

            for (int m = 0; m < squadSize; m++) //This loop gets us our Z coordinates
            {
                testtemp = rnd.NextDouble(); //our Z value
                for (int n = 0; n < squadSize; n++) //dummy test
                {
                    if (Z_coord[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = squadSize;
                    }
                    if (n == squadSize - 1)
                        Z_coord[m] = testtemp;
                }
            }

            GameObject temp;
            position = rnd.Next(3, 4);

            for (int m = 0; m < squadSize; m++)
            {
                //Debug.Log("Created enemy number: " + squadSize + " succesffuly!");
                temp = (GameObject)(Resources.Load((string)Biome.EnemyList[(int)ActiveBiomeName, EnemyTypeArray[m]], typeof(GameObject)));

                if (temp != null)
                    AreaLog[m] = spawn(position, temp, X_coord[m], Z_coord[m]);
                EnemyScale(AreaLog[m]);
            }

        }

    }

    public GameObject spawn(int position, GameObject enemy, double x, double z)
    {
        if (position == -1)
            return (GameObject)Instantiate(enemy, new Vector3((float)((40 * (roomCount)* x)+10), 2, (float)z* 10), transform.rotation);

        else if ((int)types.front == position)
            return(GameObject)Instantiate(enemy, new Vector3((float)((40 * (roomCount - 1)) - (x * 5)), 5, (float)z * rnd.Next(-7, 10)), transform.rotation);
        
        else if ((int)types.back == position)
            return (GameObject)Instantiate(enemy, new Vector3((float)((40 * (roomCount-2)) - (x * 5)-5), 5, (float)z * rnd.Next(-7, 10)), transform.rotation);

        else if ((int)types.top == position)
            return (GameObject)Instantiate(enemy, new Vector3((float)((40 * (roomCount - 2)) + (x*5)+5), 15, (float)z * rnd.Next(-7, 10)), transform.rotation);

        else if ((int)types.surprise == position)
             return (GameObject)Instantiate(enemy, new Vector3((float)((40 * (roomCount-2)) - (x*5)+ rnd.Next(-5,10)), 5, (float)z * rnd.Next(-7, 10)), transform.rotation);

        return enemy;
    }

	// Use this for initialization
	void Start () {
        // Debug.Log("Running");
        ///Preconditions///
        /// 
        //Debug.Log(Min_enemy + ", " + Max_enemy);
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

        AreaID = new int[numRoom];

        //create first room
        Instantiate(Resources.Load("LevelObjects/Left Limit", typeof(GameObject)), new Vector3(-39, 0, 0), transform.rotation);
        MakeRoom(roomCount, background);
        AreaLog = new GameObject[Max_enemy];
        roomCount++;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
