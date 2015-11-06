using UnityEngine;
using System.Collections;

public class ClawBehaviour : MonoBehaviour {

    public bool Part1;
    public GameObject part2;
    public float direction = 1;

    private Transform scale;
    private BoxCollider boxCol;
    private Vector3 maxSize, currentSize, currentPos, startPos, maxPos;
    private float currentX, currentY, currentZ, animDelay, degAngle, radAngle, radAngleZ;
    private float addX, addY, addZ;
    private bool run;
    private bool comeBack;

    // Use this for initialization
    void Start()
    {
        run = true;
        comeBack = false;
        animDelay = 0;
        boxCol = GetComponent<BoxCollider>();
        scale = gameObject.transform;
        maxSize = transform.localScale;
        maxPos = new Vector3(maxSize.x * boxCol.size.x, maxSize.y * boxCol.size.y, maxSize.z * boxCol.size.z);
        currentX = 0;
        currentSize = new Vector3(0, maxSize.y, maxSize.z);
        //boxCol.size = currentSize;

        addX = maxSize.x/100.0f;
        addY = maxSize.y/100.0f;
        addZ = maxSize.z/100.0f;

        transform.localScale = currentSize;
        startPos = transform.localPosition;
        currentPos = new Vector3(0, 0, 0);
        degAngle = transform.eulerAngles.z * Mathf.Deg2Rad;
        radAngle = Mathf.Deg2Rad * degAngle;
        radAngleZ = transform.rotation.y * Mathf.Deg2Rad;
    }

	// Update is called once per frame
	void Update () {
        if(run)
        {
            if(currentX < maxSize.x)
            {
                currentX += addX * Mathf.Cos(degAngle);
                currentY += addY * Mathf.Cos(degAngle);
                currentZ += addZ * Mathf.Cos(degAngle);
            }
            if(currentX >= maxSize.x)
            {
                currentX = maxSize.x;
                currentY = maxSize.y;
                currentZ = maxSize.z;
                run = false;
                if (Part1)
                {
                    float degAngle2 = degAngle - part2.transform.eulerAngles.z;
                    BoxCollider other = part2.GetComponent<BoxCollider>();
                    float thisL = maxPos.x;
                    float l = other.size.x * part2.transform.localScale.x;
                    Vector3 thisVec = new Vector3(maxPos.x * Mathf.Sin(degAngle), maxPos.x * Mathf.Sin(degAngle),0) * maxSize.x;
                    Vector3 otherVec = new Vector3(l * Mathf.Sin(degAngle2), 0, 0) * part2.transform.localScale.x;
                    Debug.Log(thisVec + " vs " + otherVec);
                    GameObject p2 = Instantiate(part2, transform.position + direction * (thisVec + otherVec), part2.transform.rotation) as GameObject;
                    //new Vector3(boxCol.size.x * Mathf.Sin(degAngle2) * 1/2, boxCol.size.y * Mathf.Cos(degAngle2) * 1/2, boxCol.size.z * Mathf.Sin(degAngle2) * 1/2)
                    //p2.transform.rotation *= Quaternion.Euler(0 - transform.rotation.x, 0 - transform.rotation.y,transform.rotation.z- 10);
                    //Debug.Log(transform.rotation.z * Mathf.Rad2Deg);
                    p2.GetComponent<ClawBehaviour>().part2 = gameObject;
                   // p2.transform.rotation = new Quaternion(0, 0, 0.02f, 0);
                }
                else
                {
                    animDelay = 0.4f;
                }
            }
            //add vertical offset, make direction called horizontal offset, create attack
            currentSize.x = currentX;
            currentPos.x = -currentX * 1f * Mathf.Cos(degAngle) * maxPos.x * maxSize.x;
            currentPos.y = -currentY * 1f * Mathf.Sin(degAngle) * maxPos.x * (maxSize.x + maxPos.y);
            currentPos.z = -currentZ * 1f * Mathf.Cos(degAngle) * maxPos.x * maxSize.x;
            transform.position = startPos + currentPos;
            //Debug.Log(currentPos);
            transform.localScale = currentSize;
        }

        if (animDelay > 0)
        {
            animDelay -= Time.deltaTime;
            if (animDelay <= 0)
            {
                Deactivate();
            }
        }
        else if (comeBack)
        {
            if (currentX > 0)
            {
                currentX -= addX * Mathf.Cos(degAngle);
               // currentY -= addY * Mathf.Sin(degAngle);
                //currentZ -= addZ * Mathf.Sin(degAngle);
            }
            if (currentX <= 0)
            {
                currentX = 0;
                comeBack = false;
                if (!Part1)
                {
                    part2.GetComponent<ClawBehaviour>().Deactivate();
                }
                Destroy(gameObject);
            }
            currentSize.x = currentX;
            currentPos.x = -currentX * 3.6f;
            //transform.position = startPos + currentPos;
            transform.localScale = currentSize;

        }
    }

    public void Deactivate()
    {
        comeBack = true;
    }
}
