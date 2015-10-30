
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    //public variables
    public GameObject gobjCameraTarget;
    public Vector3 v3DefaultCameraRotationVector;
    public Vector3 v3DefaultCameraPositionVector;
    public float flVerticalOffset;
    public float flDepthOffset;
    public float flXAxisTolerance;
    public float flYAxisTolerance;
    public float flYChangeSpeed;

    public bool camerShakeIsOn = false;


    //local variables
    Vector3 v3PreviousFrameCameraPosition;
    float flCameraYBaseLine;


    void Start()
    {
        gobjCameraTarget = GameObject.Find("Player");
        flCameraYBaseLine = gobjCameraTarget.transform.position.y;

    }

    void LateUpdate()
    {
        Vector3 v3CameraTargetPosition = gobjCameraTarget.transform.position;



        Vector3 v3FinalCameraPosition;

        v3FinalCameraPosition.x = GetXCameraPosition(v3CameraTargetPosition.x);
        v3FinalCameraPosition.z = flDepthOffset;
        v3FinalCameraPosition.y = GetYCameraPosition(v3CameraTargetPosition.y);




        v3PreviousFrameCameraPosition = v3FinalCameraPosition;
        if (camerShakeIsOn == true)
        {
            Vector2 v2ScreenShakeVector = ScreenShake.ScreenShakeTest();
            v3FinalCameraPosition.x += v2ScreenShakeVector.x;
            v3FinalCameraPosition.y += v2ScreenShakeVector.y;

        }


        transform.position = v3FinalCameraPosition;
    }

    float GetXCameraPosition(float flCameraTargetXPosition)
    {

        float flFrameXDifference = v3PreviousFrameCameraPosition.x - flCameraTargetXPosition;

        if ((Mathf.Abs(flFrameXDifference)) > flXAxisTolerance)
        {
            return flCameraTargetXPosition + Mathf.Sign(flFrameXDifference) * flXAxisTolerance;
        }
        return v3PreviousFrameCameraPosition.x;
    }

    float GetYCameraPosition(float flCameraTargetYPosition)
    {

        float flDifference = flCameraTargetYPosition - flCameraYBaseLine;

        if (flDifference < 0)
        {
            flCameraYBaseLine = flCameraTargetYPosition;
        }
        else if (flDifference > flYAxisTolerance)
        {
            flCameraYBaseLine = flCameraYBaseLine + Mathf.Min(flYChangeSpeed, flDifference);
        }

        return flCameraYBaseLine + flVerticalOffset;
    }
}

public class ScreenShake : MonoBehaviour
{


    public static Vector2 ScreenShakeTest()
    {
        float Magnitude = 0.1f;
        Vector2 random_direction = Random.insideUnitCircle;

        return random_direction * Magnitude;
    }
}

//Junk
/*



    public MoveController target;
    
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public float focusAreaSize;


     //FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    //focusArea = new FocusArea(target.GetComponent<BoxCollider>().bounds, focusAreaSize, GetComponent<Transform>().position.y);







      void LateUpdate()
    {
        focusArea.Update(target.GetComponent<BoxCollider>().bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
            {
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                }
            }
        }


        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, new Vector2(focusAreaSize, 1));
    }

    class FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float y;
 
        public FocusArea(Bounds targetBounds, float size, float y)
        {
            left = targetBounds.center.x - size / 2;
            right = targetBounds.center.x + size / 2;
            y = y;
            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, y);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            centre = new Vector2((left + right) / 2, y);
            velocity = new Vector2(shiftX, y);
        }
    }










*/
