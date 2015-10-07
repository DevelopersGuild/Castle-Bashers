using UnityEngine;
using System.Collections;
using System;

public class RandomAreas : MonoBehaviour
{

    public Area area1, area2, area3;
    private float chunkSpacing;
    private Area[] ar = new Area[3];
    private System.Random rnd;
    private int rndArea, rndTimes, tempArea;
    private float distMult;
    private Vector3 aL;

    // Use this for initialization
    void Start()
    {
        chunkSpacing = 90f;
        //create black screen with loading

        rnd = new System.Random(Guid.NewGuid().GetHashCode());
        //min rand areas/2, max rand areas/2
        rndTimes = rnd.Next(10, 26) / 2;

        //distance between areas
        aL = new Vector3(chunkSpacing, 0, 0);
        distMult = 0;

        ar[0] = area1;
        ar[1] = area2;
        ar[2] = area3;

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
