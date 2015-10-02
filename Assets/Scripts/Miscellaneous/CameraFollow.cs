
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public MoveController target;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public float focusAreaSize;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    void Start()
    {
        target = FindObjectOfType<Player>().GetComponent<MoveController>();
        focusArea = new FocusArea(target.GetComponent<BoxCollider>().bounds, focusAreaSize, GetComponent<Transform>().position.y);
    }

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
            Debug.Log(left);
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

}