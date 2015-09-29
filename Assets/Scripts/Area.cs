using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Area : MonoBehaviour
{

     public string numID;
     public List<Area> transitions = new List<Area>();

     // Use this for initialization
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }

     public Area getTransition(Area ar)
     {
          string s = numID + ar.getID();
          foreach (Area trans in transitions)
          {
               if (trans.getID().Equals(s))
               {
                    return trans;
               }
          }
          return null;
     }

     public string getID()
     {
          return numID;
     }
}
