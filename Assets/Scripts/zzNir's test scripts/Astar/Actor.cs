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
    bool toP = false;
    bool canMove = false;

    private GameObject target;

    void Awake()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        control = (NodeControl)cam.GetComponent(typeof(NodeControl));
        target = null;
    }

    void Update()
    {
        //m_speed = Time.deltaTime * m_speed_multi;
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
                        MoveOrder(target.transform.position, toP);
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
                Debug.DrawLine((Vector3)path[i], (Vector3)path[i + 1], Color.magenta, 0.01f);
            }
        }
        if (canMove)
        {
            Vector3 newPos = transform.position;

            float Xdistance = newPos.x - currNode.x;
            if (Xdistance < 0) Xdistance -= Xdistance * 2;
            float Ydistance = newPos.z - currNode.z;
            if (Ydistance < 0) Ydistance -= Ydistance * 2;

            if ((Xdistance < 0.1 && Ydistance < 0.1) && m_target == currNode) //Reached target
            {
                //ChangeState(State.IDLE);
                MoveOrder(target.transform.position, toP);
            }
            else if (Xdistance < 0.1 && Ydistance < 0.1)
            {
                nodeIndex++;
                onNode = true;
            }

            /***Move toward waypoint***/
            Vector3 motion = currNode - newPos;

            //newPos += motion;
            Debug.DrawLine(transform.position, currNode, Color.red, 0.01f);
            GetComponent<Enemy>().Move(motion, 10);
            //newPos += motion * m_speed;

            //transform.position = newPos;
        }
    }

    public void setMove(bool c)
    {
        canMove = c;
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

    public void MoveOrder(Vector3 pos, bool toPlayer = false)
    {
        toP = toPlayer;
        if (toPlayer)
        {
            float f = Mathf.Sign(pos.x - transform.position.x);
            m_target = pos + new Vector3(-1.5f, 0, 0) * f;
        }
        else
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
