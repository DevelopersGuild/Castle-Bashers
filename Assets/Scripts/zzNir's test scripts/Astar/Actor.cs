using UnityEngine;
using System;
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
    Enemy me;

    private GameObject target;

    void Awake()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        me = GetComponent<Enemy>();
        control = GetComponent<NodeControl>();
        //control = (NodeControl)cam.GetComponent(typeof(NodeControl));
        target = null;
    }

    void Update()
    {
        //m_speed = Time.deltaTime * m_speed_multi;
        if(state == State.MOVING)
        {
            if(elapsedTime > 0.25)
            {
                elapsedTime = 0;
                MoveOrder(target.transform.position, toP);
            }
            MoveToward();
        }
        //if (elapsedTime > OldTime)
        //{
        //    switch (state)
        //    {
        //        case State.IDLE:
        //            break;

        //        case State.MOVING:
        //            OldTime = elapsedTime + 0.01f;

        //            if (elapsedTime > checkTime)
        //            {
        //                checkTime = elapsedTime + 1;
        //                //SetTarget();
        //                MoveOrder(target.transform.position, toP);
        //            }

        //            if (path != null)
        //            {
        //                if (onNode)
        //                {
        //                    onNode = false;
        //                    if (nodeIndex < path.Count)
        //                    {
        //                        MoveOrder(target.transform.position, toP);
        //                        currNode = path[nodeIndex];
        //                    }
        //                }
        //                MoveToward();
        //            }
        //            else
        //            {
        //                me.MoveToDir(me.targetPos);
        //            }
        //            break;
        //    }
        //}
        elapsedTime += Time.deltaTime;

    }

    void MoveToward()
    {
        if (path == null)
        {
            currNode = target.transform.position;
        }
        else
        {
            currNode = path[nodeIndex];
        }
      

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

            if ((currNode - newPos).magnitude < 0.25) //Reached target
            {
                if (m_target == currNode)
                {
                    //ChangeState(State.IDLE);
                    MoveOrder(target.transform.position, toP);
                }
                else
                {
                    nodeIndex++;
                    onNode = true;
                    if (path != null && nodeIndex < path.Count)
                        currNode = path[nodeIndex];
                    else
                    {
                        MoveOrder(target.transform.position, toP);
                    }
                }
            }

            /***Move toward waypoint***/
            //Debug.Log(currNode);
            //newPos += motion;
            Debug.DrawLine(transform.position, currNode, Color.red, 0.01f);
            Vector3 motion = new Vector3(currNode.x, 0, currNode.z);
            me.MoveToDir(motion, 10);
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
        int temp = 0;
        do
        {
            path = control.Path(transform.position, m_target, zDiff);
            temp++;
        } while (path == null && temp < 5);
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
