using UnityEngine;
using System.Collections;

public class ExtendBehaviour : MonoBehaviour
{

    public float direction = 1;
    public bool dextend = true;

    private Transform scale;
    private BoxCollider boxCol;
    private Vector3 maxSize, currentSize, currentPos, startPos;
    private float currentX, currentY, currentZ, animDelay, degAngle, radAngle, radAngleZ;
    private bool run, pause;
    private bool comeBack;
    public bool x, isResuming;
    private float t;

    // Use this for initialization
    void Start()
    {
        run = true;
        comeBack = false;
        animDelay = 0.2f;
        boxCol = GetComponent<BoxCollider>();
        scale = gameObject.transform;
        maxSize = transform.localScale;
        currentX = 0;
        currentY = 0;
        if (x)
            currentSize = new Vector3(0, maxSize.y, maxSize.z);
        else
            currentSize = new Vector3(maxSize.x, 0, maxSize.z);
        //boxCol.size = currentSize;
        transform.localScale = currentSize;
        startPos = transform.localPosition;
        currentPos = new Vector3(0, 0, 0);
        degAngle = transform.rotation.z;
        radAngle = Mathf.Deg2Rad * degAngle;
        radAngleZ = transform.rotation.y * Mathf.Deg2Rad;

        t = 0;
        isResuming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pause)
        {
            if(isResuming)
            {
                if(t <= 0) 
                    Return();
            }
        }
        else if (run)
        {
            if (currentX <= maxSize.x && currentY <= maxSize.y)
            {
                if (x)
                    currentX += 0.07f;
                else
                    currentY += 0.07f;
            }
            if (currentX > maxSize.x || currentY > maxSize.y)
            {
                run = false;
                animDelay = 0.4f;
                if (x)
                    currentX = maxSize.x;
                else
                    currentY = maxSize.y;

            }
            //add vertical offset, make direction called horizontal offset, create attack
            if (x)
            {
                currentSize.x = currentX;
                currentPos.x = -currentX * 3.6f;
            }
            else
            {
                currentSize.y = currentY;
                currentPos.y = -currentY * 3.6f;
            }
            transform.localPosition = startPos + currentPos;
            //Debug.Log(currentPos);
            transform.localScale = currentSize;

        }
        else if (!comeBack && animDelay > 0)
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
                currentX -= 0.07f;
            }
            if (currentX <= 0)
            {
                currentX = 0;
                comeBack = false;
                Destroy(gameObject);
            }
            currentSize.x = currentX;
            currentPos.x = -currentX * 3.6f;
            transform.localPosition = startPos + currentPos;
            transform.localScale = currentSize;

        }

        t -= Time.unscaledDeltaTime;


    }

    public void Deactivate()
    {

        if (!dextend)
            Destroy(gameObject);
        else
            comeBack = true;
    }

    public void SetPause(bool bl)
    {
        pause = bl;
    }

    public void Return()
    {
        pause = false;
        run = false;
        comeBack = true;
    }

    public void Resume(float f)
    {
        t = f;
        isResuming = true;
    }
}
