using UnityEngine;
using System.Collections;
using System;

public class AreaGen : MonoBehaviour
{

    public int Min_Area; //minmum areas for level
    public int Max_Area; //maximum areas for level
    public int Min_Enemy;//minimum enemies for area
    public int Max_Enemy; //max enemies for area
    public int Total_Objects;
    private int t_length = 0; //Stage length, used for Traps
    public Enemy Boss; /// <summary>
                       /// Need to create a Boss script which inherits from our enemy script. Boss will be of Class 'Boss' 
                       /// </summary>
    public Biome.BiomeName ActiveBiomeName;

    private System.Random rnd;


    // Use this for initialization
    void Start()
    {
        FindObjectOfType<Main_Process>().GetComponent<Main_Process>().Main_UI_Init(false);

        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        int AreaNumber = rnd.Next(Min_Area, Max_Area);
        int AreaXCoord = 0;
        int AreaYCoord = 1;
        int AreaZCoord = 1;

        GameObject temp;

        GameObject background = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath(Biome.Backgrounds[(int)ActiveBiomeName, 0], typeof(GameObject));



        

        Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/Left Limit.prefab", typeof(GameObject)), new Vector3(-19,0,0), transform.rotation);

        for (int i = 0; i < AreaNumber; i++)
        {


            Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/3DFloorB.prefab", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, AreaZCoord), transform.rotation);
            Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/Front Limit.prefab", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, 13), transform.rotation); //set front limits
            Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/Back Limit.prefab", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, -10), transform.rotation); //set back limits
            t_length += 40;

            if(background!=null)
            Instantiate(background, new Vector3((AreaXCoord + i) * 40, 5, 13), transform.rotation);

            // Debug.Log("Recurrssion: " + i);
            int EnemySize = rnd.Next(Min_Enemy, Max_Enemy);

           // Debug.Log("EnemySize: " + EnemySize);
            int[] EnemyTypeArray = new int[EnemySize];
            int[] arrayX = new int[EnemySize];
            int[] arrayZ = new int[EnemySize];


                for (int m = 0; m < EnemySize; m++) //creates enemy types
                {
                    int testType = rnd.Next(0, 2);

                    EnemyTypeArray[m] = testType;
                   // Debug.Log("EnemyTypeArray at value" + m + "::" + EnemyTypeArray[m]);
                }


                //////////////////////////
                int testtemp;
                for (int m = 0; m < EnemySize; m++) //This loop gets us our X coordinates
                {
                    testtemp = rnd.Next(10, 20); //our X value
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
                    testtemp = rnd.Next(-10, 9); //our Z value
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
                    temp = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath((string)Biome.EnemyList[(int)ActiveBiomeName, EnemyTypeArray[m]], typeof(GameObject)));
                    if (temp!=null)
                    Instantiate(temp, new Vector3((arrayX[m] + (40 * i)), 5, arrayZ[m]), transform.rotation);
                }

                if (i == AreaNumber-1)
                {
                if(Boss!=null)
                Instantiate(Boss, new Vector3((15 + (40 * i)), 5, AreaZCoord), transform.rotation);
                }


            } //END OF PART GENERATION

        temp = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/Barrel.prefab", typeof(GameObject)));

        for (int i=0; i< Total_Objects; i++)
            {
            int[] arrayX = new int[Total_Objects];
            int[] arrayZ = new int[Total_Objects];
            //////////////////////////
            int testtemp;
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
                testtemp = rnd.Next(-10, 9); //our Z value
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
            Instantiate(temp, new Vector3((arrayX[i]), 2.5f, arrayZ[i]), transform.rotation);
        }

        Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/Right Limit.prefab", typeof(GameObject)), new Vector3((t_length)-13, AreaYCoord, AreaZCoord), transform.rotation);
        Debug.Log(t_length);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
