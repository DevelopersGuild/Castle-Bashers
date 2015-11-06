using UnityEngine;
using System.Collections;
using System;

public class AreaGen : MonoBehaviour
{

    public int Min_Area; //minmum areas for level
    public int Max_Area; //maximum areas for level
    public int Min_Enemy;//minimum enemies for area
    public int Max_Enemy; //max enemies for area
    public Enemy Boss; /// <summary>
                       /// Need to create a Boss script which inherits from our enemy script. Boss will be of Class 'Boss' 
                       /// </summary>
    public Biome.BiomeName ActiveBiomeName;

    private System.Random rnd;


    // Use this for initialization
    void Start()
    {
        rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
        int AreaNumber = rnd.Next(Min_Area, Max_Area);
        int AreaXCoord = 0;
        int AreaYCoord = 1;
        int AreaZCoord = 1;

        GameObject temp;

        //        GameObject test = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/BasicEnemy.prefab", typeof(GameObject))) as GameObject;

        for (int i = 0; i < AreaNumber; i++)
        {
            Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelObjects/3DFloorB.prefab", typeof(GameObject)), new Vector3((AreaXCoord + i) * 40, AreaYCoord, AreaZCoord), transform.rotation);
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
                    Instantiate(temp, new Vector3((arrayX[m] + (40 * i)), 5, arrayZ[m]), transform.rotation);
                }

                if (i == AreaNumber-1)
                {
                Instantiate(Boss, new Vector3((15 + (40 * i)), 5, AreaZCoord), transform.rotation);
                }


            }

        }

    // Update is called once per frame
    void Update()
    {

    }

}
