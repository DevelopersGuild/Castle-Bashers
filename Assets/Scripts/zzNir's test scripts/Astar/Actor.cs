using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{

    enum State
    {
        IDLE,
        MOVING,
    }

    float m_speed;
    float m_speed_multi = 5;
    public bool DebugMode;

    bool onNode = true;
    Vector3 m_target = new Vector3(0, 0, 0);
    Vector3 currNode;
    int nodeIndex;
    List<Vector3> path = new List<Vector3>();
    NodeControl control;
    State state = State.IDLE;
    float OldTime = 0;
    float checkTime = 0;
    float elapsedTime = 0;
    float zDiff = -1;

    private GameObject target;

    void Awake()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        control = (NodeControl)cam.GetComponent(typeof(NodeControl));
        target = null;
    }

    void Update()
    {
        m_speed = Time.deltaTime * m_speed_multi;
        elapsedTime += Time.deltaTime;

        if (elapsedTime > OldTime)
        {
            switch (state)
            {
                case State.IDLE:
                    break;

                case State.MOVING:
                    OldTime = elapsedTime + 0.01f;

                    if (elapsedTime > checkTime)
                    {
                        checkTime = elapsedTime + 1;
                        //SetTarget();
                        MoveOrder(target.transform.position);
                    }

                    if (path != null)
                    {
                        if (onNode)
                        {
                            onNode = false;
                            if (nodeIndex < path.Count)
                                currNode = path[nodeIndex];
                        }
                        else
                            MoveToward();
                    }
                    break;
            }
        }
    }

    void MoveToward()
    {
        if (DebugMode)
        {
            for (int i = 0; i < path.Count - 1; ++i)
            {
                Debug.DrawLine((Vector3)path[i], (Vector3)path[i + 1], Color.red, 0.01f);
            }
        }

        Vector3 newPos = transform.position;

        float Xdistance = newPos.x - currNode.x;
        if (Xdistance < 0) Xdistance -= Xdistance * 2;
        float Ydistance = newPos.z - currNode.z;
        if (Ydistance < 0) Ydistance -= Ydistance * 2;

        if ((Xdistance < 0.1 && Ydistance < 0.1) && m_target == currNode) //Reached target
        {
            //ChangeState(State.IDLE);
            MoveOrder(target.transform.position);
        }
        else if (Xdistance < 0.1 && Ydistance < 0.1)
        {
            nodeIndex++;
            onNode = true;
        }

        /***Move toward waypoint***/
        Vector3 motion = currNode - newPos;
        motion.Normalize();
        newPos += motion * m_speed;

        transform.position = newPos;
    }

    private void SetTarget()
    {
        path = control.Path(transform.position, m_target, zDiff);
        nodeIndex = 0;
        onNode = true;

    }

    public void setZ(float z)
    {
        zDiff = z;
    }

    public void MoveOrder(Vector3 pos)
    {
        m_target = pos;
        SetTarget();
        ChangeState(State.MOVING);
    }

    private void ChangeState(State newState)
    {
        state = newState;
    }

    public void setTarg(GameObject g)
    {
        target = g;
    }
}
