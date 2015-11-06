using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour
{
     public GameObject skill1;

     // Use this for initialization
     void Start()
     {
        
     }

     // Update is called once per frame
     void Update()
     {
          
     }

     public virtual void SetDir(float dir)
     {
          Instantiate(skill1, transform.position + dir * new Vector3(.5f,0,0), transform.rotation);
          Destroy(gameObject);
     }

     public virtual Vector3 getOffset()
     {
          return new Vector3(1,0,0);
     }

     public virtual void Init(float facing)
     {

     }


}