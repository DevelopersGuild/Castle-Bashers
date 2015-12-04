using UnityEngine;
using System.Collections;
using System;

public class AreaGen : MonoBehaviour
{

    public int Min_Area; //minmum areas for level
    public int Max_Area; //maximum areas for level
    public static int AreaNumber;
    public static int[] EnemyNumber;
    public int Min_Enemy;//minimum enemies for area
    public int Max_Enemy; //max enemies for area
    public int Total_Objects;
    public Enemy Event;
    public GameObject Weather;
    private GameObject weatherObject;
    private int t_length = 0; //Stage length, used for Traps
    public Enemy Boss; /// <summary>
                       /// Need to create a Boss script which inherits from our enemy script. Boss will be of Class 'Boss' 
                       /// </summary>
    static public int BossID;
    public Biome.BiomeName ActiveBiomeName;

    static public GameObject[,] AreaLog;
    static public int[] AreaID;
    public Texture[] objects = new Texture[5];

    private System.Random rnd;

    // Use this for initialization
    void Start()
    {

        //FindObjectOfType<Main_Process>().GetComponent<Main_Process>().Main_UI_Init(false);

        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        AreaNumber = rnd.Next(Min_Area, Max_Area);
        int AreaXCoord = 0;
        int AreaYCoord = 1;
        int AreaZCoord = 1;
        Quaternion weather = Quaternion.AngleAxis(90, Vector3.right);

        GameObject temp;

        GameObject background = (GameObject)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName,0], typeof(GameObject));

        AreaLog = new GameObject[AreaNumber, Max_Enemy];
        AreaID = new int[AreaNumber];
        EnemyNumber = new int[AreaNumber];
        
        
        Instantiate(Resources.Load("LevelObjects/Left Limit", typeof(GameObject)), new Vector3(-5,0,0), transform.rotation);

        for (int i = 0; i < AreaNumber; i++)
        {


            Instantiate(Resources.Load("LevelObjects/3DFloorB", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, AreaZCoord), transform.rotation);
            Instantiate(Resources.Load("LevelObjects/Front Limit", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, 11), transform.rotation); //set front limits
            Instantiate(Resources.Load("LevelObjects/Back Limit", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, -8), transform.rotation); //set back limits
            AreaID[i] = Instantiate(Resources.Load("LevelObjects/Right Limit", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40 + 20, 0, 0), transform.rotation).GetInstanceID();
            
            t_length += 40;

            if (Weather != null)
            {
                weatherObject = Instantiate(Weather, new Vector3((AreaXCoord + i) * 40, 50, -8), Quaternion.identity) as GameObject;
                weatherObject.transform.eulerAngles = new Vector3(77, 180, 180);
            }

            if (background!=null)
            Instantiate(background, new Vector3((AreaXCoord + i) * 40, 5, 13), transform.rotation);
               if((int)ActiveBiomeName== 0)
                    {
                    objects[0] = (Texture)Resources.Load(Biome.Backgrounds[(int)ActiveBiomeName, 1], typeof(Texture));
                    if(objects[0]!=null)
                    Instantiate(objects[0], new Vector3((AreaXCoord + i) * 40, AreaYCoord, 2), transform.rotation);
                    //Debug.Log(objects[0].name);
            }

            // Debug.Log("Recurrssion: " + i);
            int EnemySize = rnd.Next(Min_Enemy, Max_Enemy);
            EnemyNumber[i] = EnemySize;

           // Debug.Log("EnemySize: " + EnemySize);
            int[] EnemyTypeArray = new int[EnemySize];
            double[] arrayX = new double[EnemySize];
            double[] arrayZ = new double[EnemySize];


                for (int m = 0; m < EnemySize; m++) //creates enemy types
                {
                    int testType = rnd.Next(0, 2);

                    EnemyTypeArray[m] = testType;
                   // Debug.Log("EnemyTypeArray at value" + m + "::" + EnemyTypeArray[m]);
                }


                //////////////////////////
                double testtemp;
                for (int m = 0; m < EnemySize; m++) //This loop gets us our X coordinates
                {
                    testtemp = rnd.NextDouble(); //our X value
                    for (int n = 0; n < EnemySize; n++)//dummy test
                    {
                        if (arrayX[n] == testtemp)
                        {
                            --m;
                            testtemp = 0;
                            n = EnemySize;
                        }
                        if (n == EnemySize - 1)
                            arrayX[m] = testtemp;
                    }
                }

                ////////////////////
                for (int m = 0; m < EnemySize; m++) //This loop gets us our Z coordinates
                {
                    testtemp = rnd.NextDouble(); //our Z value
                    for (int n = 0; n < EnemySize; n++) //dummy test
                    {
                        if (arrayZ[n] == testtemp)
                        {
                            --m;
                            testtemp = 0;
                            n = EnemySize;
                        }
                        if (n == EnemySize - 1)
                            arrayZ[m] = testtemp;
                    }
                }
                ///////////////////////////



                for (int m = 0; m < EnemySize; m++)
                {
                Debug.Log("Created enemy number: " + EnemySize + " succesffuly!");
                    temp = (GameObject)(Resources.Load((string)Biome.EnemyList[(int)ActiveBiomeName, EnemyTypeArray[m]], typeof(GameObject)));
                    if (temp!=null)
                    AreaLog[i, m] = (GameObject)Instantiate(temp, new Vector3((float)(arrayX[m]*rnd.Next(1,5)+15+ (40 * i)), 5, (float)arrayZ[m]*rnd.Next(0,4)), transform.rotation);

            }

                if (i == AreaNumber-1)
                {
                
                if (Boss != null)
                {
                    
                    BossID = Instantiate(Boss, new Vector3((15 + (40 * i)), 5, AreaZCoord), transform.rotation).GetInstanceID();
                    
                }
                }




            } //END OF PART GENERATION
        
        temp = (GameObject)(Resources.Load("LevelObjects/Barrel", typeof(GameObject)));
        
        for (int i=0; i< Total_Objects; i++)
            {
            double[] arrayX = new double[Total_Objects];
            double[] arrayZ = new double[Total_Objects];
            //////////////////////////
            double testtemp;
            for (int m = 0; m < Total_Objects; m++) //This loop gets us our X coordinates
            {
                testtemp = rnd.Next(10, t_length-20); //our X value
                for (int n = 0; n < Total_Objects; n++)//dummy test
                {
                    if (arrayX[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = Total_Objects;
                    }
                    if (n == Total_Objects - 1)
                        arrayX[m] = testtemp;
                }
            }

            ////////////////////
            for (int m = 0; m < Total_Objects; m++) //This loop gets us our Z coordinates
            {
                testtemp = rnd.Next(-4, 4); //our Z value
                for (int n = 0; n < Total_Objects; n++) //dummy test
                {
                    if (arrayZ[n] == testtemp)
                    {
                        --m;
                        testtemp = 0;
                        n = Total_Objects;
                    }
                    if (n == Total_Objects - 1)
                        arrayZ[m] = testtemp;
                }
            }
            
            if (temp!=null)
            Instantiate(temp, new Vector3(((float)arrayX[i])*rnd.Next(10,20)+5, 2.5f, (float)arrayZ[i]*rnd.Next(-7,7)), transform.rotation);
            
        }
        

        Instantiate(Resources.Load("LevelObjects/End Limit", typeof(GameObject)), new Vector3((t_length)-20, AreaYCoord, AreaZCoord), transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

}
