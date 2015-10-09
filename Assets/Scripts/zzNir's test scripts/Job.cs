using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour
{

     public Skill skill1, skill2, skill3, skill4, skill5;
     private Skill s1, s2, s3, s4, s5;
     private float s1_CD, s2_CD, s3_CD, s4_CD, s5_CD;
     private float facing;
     private MoveController moveController;
     private Vector3 pos;

     // Use this for initialization
     void Start()
     {
          moveController = GetComponent<MoveController>();
          s1_CD = s2_CD = 3;
          s3_CD = 5;
          s4_CD = 8;
          s5_CD = 12;
          pos = new Vector3(transform.position.x, -2.196465f, transform.position.z);
     }

     // Update is called once per frame
     void Update()
     {
          if (Input.GetKey(KeyCode.Alpha1))
          {
               if (s1_CD >= 3)
               {
                    facing = moveController.GetFacing();
                    s1 = Instantiate(skill1, transform.position, transform.rotation) as Skill;
                    s1.Init(facing);
                    s1_CD = 0;
               }
               //else
               //play error sound
          }
          if (Input.GetKey(KeyCode.Alpha2))
          {
               if (s2_CD >= 3)
               {
                    facing = moveController.GetFacing();
                    s2 = Instantiate(skill2, transform.position, transform.rotation) as Skill;
                    s2.Init(facing);
                    s2_CD = 0;
               }
          }
          if (Input.GetKey(KeyCode.Alpha3))
          {
               if (s3_CD >= 5)
               {
                    facing = moveController.GetFacing();
                    s3 = Instantiate(skill3, transform.position, transform.rotation) as Skill;
                    s3.Init(facing);
                    s3_CD = 0;
               }
          }


          s1_CD += Time.deltaTime;
          s2_CD += Time.deltaTime;
          s3_CD += Time.deltaTime;
          s4_CD += Time.deltaTime;
          s5_CD += Time.deltaTime;
     }


}
