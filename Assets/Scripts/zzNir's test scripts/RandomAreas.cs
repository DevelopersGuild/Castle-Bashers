using UnityEngine;
using System.Collections;
using System;

/*------How To Program with the Random Area Generator -----/
/Restrictions:
/- Must set Area 0 with value
/- Must set Areas and enemies in order (i.e, dont skip Area 2 if you have two Areas, i.e. Area1 then Area3)
/- Max limit of Area types is TBD
/- Max limit of Enemy types is TBD
/- Max limit of Trap types is TBD
/---------------------------------------------------------*/


public class RandomAreas : MonoBehaviour
{
    public int MIN_AREAS;
    public int MAX_AREAS;
    public int BIOME;
    public int enemyMinimum;
    public Area area1, area2, area3; //our area types

    //area variables
    private float chunkSpacing; //used fo distance
    private Area[] ar = new Area[3]; //array of areas
    private int rndArea, rndTimes, tempArea;
    private float distMult;
    private Vector3 aL;
    private int areaNullStop = 0;

    //enemy variables
    private UnityEngine.GameObject[]Enemies = new UnityEngine.GameObject[20];
    private Vector3 vShift;
    private const int enemyMax = 5; //change here for max
    private UnityEngine.GameObject[] en = new UnityEngine.GameObject[3]; // enemy array
    private int rndGroup; //range for enemies at given area

    private System.Random rnd;

    // Use this for initialization
    void Start()
    {
        //Enemies[1] = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/BasicEnemy"), typeof(GameObject);
        if (Enemies[1] ==null)
        { Application.Quit(); }
        Enemies[2] = GameObject.Find("Enemy2");
        Enemies[3] = GameObject.Find("Enemy3");

        chunkSpacing = 90f;
        //create black screen with loading

        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        //min rand areas/2, max rand areas/2
        rndTimes = rnd.Next(MIN_AREAS, MAX_AREAS);

        //distance between areas

        aL = new Vector3(chunkSpacing, 0, 0);
        vShift = new Vector3(0, 1, 0);

        distMult = 0;


        ar[0] = area1;
        ar[1] = area2;
        ar[2] = area3;

        if (ar[0] == null)
            Application.Quit();

        //check for nulls, if all null, wont run
        for (int i = 0; i < 3; i++)
        {
            if (ar[i] == null)
                areaNullStop += 1;
        }

        if (areaNullStop == 2)
        {
            Application.Quit();
        }

        en[0] = Enemies[1];
        en[1] = Enemies[2];
        en[2] = Enemies[3];

        rndArea = rnd.Next(0, 2);
        tempArea = rndArea;

        if (ar[rndArea] != null)
        {

            Instantiate(ar[rndArea], transform.position + aL * distMult, area1.transform.rotation);
            for (int i = 0; i < rndTimes; i++)
            {
                distMult += 0.4f;
                rndArea = rnd.Next(0, 3);

                //// Transitions
                //Instantiate(ar[tempArea].getTransition(ar[rndArea]), transform.position + aL * distMult, area1.transform.rotation);
                //distMult += 0.6f;

                Instantiate(ar[rndArea], transform.position + aL * distMult, area1.transform.rotation);
                tempArea = rndArea;

                //after area created, create enemies
                rndGroup = rnd.Next(enemyMinimum, enemyMax);

                int[] arrayX = new int[rndGroup];
                int[] arrayZ = new int[rndGroup];
                int testtemp;
                for (int j = 0; j < rndGroup; j++) //This loop gets us our X coordinates
                {
                    testtemp = rnd.Next(10, 20); //our X value
                    for (int k = 0; k < rndGroup; k++)//dummy test
                    {
                        if (arrayX[k] == testtemp)
                        {
                            --j;
                            testtemp = 0;
                            k = rndGroup;
                        }
                        if (k == rndGroup - 1)
                            arrayX[j] = testtemp;
                    }
                }

                for (int j = 0; j < rndGroup; j++) //This loop gets us our Z coordinates
                {
                    testtemp = rnd.Next(-10, 9); //our Z value
                    for (int k = 0; k < rndGroup; k++) //dummy test
                    {
                        if (arrayZ[k] == testtemp)
                        {
                            --j;
                            testtemp = 0;
                            k = rndGroup;
                        }
                        if (k == rndGroup - 1)
                            arrayZ[j] = testtemp;
                    }
                }

                for (int j = 0; j < rndGroup; j++)
                {
                    vShift.x = arrayX[j];
                    vShift.z = arrayZ[j];
                    vShift.y = 5.00f;

                    if (en[rndArea] != null)
                    { Instantiate(en[i], (transform.position) + (aL * distMult) + vShift, transform.rotation); }//UNSTABLE
                  

                }
            }

        Instantiate(en[1], new Vector3(0, 0, 0), transform.rotation);

        }

        Instantiate(en[1], new Vector3(0,0,0), transform.rotation);
        //create player
        //destroy black screen
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
