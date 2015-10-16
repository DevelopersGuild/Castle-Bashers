using UnityEngine;
using System.Collections;
using System;

public class RandomAreas : MonoBehaviour
{


    public int MIN_AREAS;
    public int MAX_AREAS;
    public int enemyMinimum;
    public Area area1, area2, area3; //our basic areas
    public Enemy enemy1, enemy2, enemy3; //basic enemies

    //area variables
    private float chunkSpacing; //used fo distance
    private Area[] ar = new Area[3]; //array of areas
    private int rndArea, rndTimes, tempArea;
    private float distMult;
    private Vector3 aL;

    //enemy variables
    private Vector3 vShift;
    private const int enemyMax = 5; //change here for max
    private Enemy[] en = new Enemy[3]; // enemy array
    private int rndGroup; //range for enemies at given area

    private System.Random rnd;

    // Use this for initialization
    void Start()
    {
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

        //CHANGE
        en[0] = enemy1;
        en[1] = enemy2;
        en[2] = enemy3;

        rndArea = rnd.Next(0, 2);
        tempArea = rndArea;
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
            for (int j= 0; j< rndGroup; j++) //This loop gets us our X coordinates
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
                    if (k == rndGroup-1)
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
                
                Instantiate(en[rndArea], (transform.position) + (aL * distMult) + vShift, transform.rotation); //UNSTABLE

                
            }
        }

        //create player
        //destroy black screen
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
