using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

    private Transform target;
    public float xOffset, zOffset;
    public LayerMask collisionMask;

    // Use this for initialization
    void Start()
    {
        target = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        // Shadow will be on the ground relative to its parent by shooting raycasts
        Vector3 rayOrigin = target.position;
        RaycastHit hitInfo;
        float rayLength;
        bool hit = Physics.Raycast(rayOrigin, Vector2.up * -1, out hitInfo, 10, collisionMask);

        if (hit)
        {
            rayLength = hitInfo.distance;
            transform.position = new Vector3(target.position.x + (xOffset * target.localScale.x * -1), hitInfo.point.y, target.position.z + zOffset);
        }
    }
}
