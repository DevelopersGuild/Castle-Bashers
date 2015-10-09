using UnityEngine;
using System.Collections;

public class PheonixShot : Projectile
{
     private Vector3 dir = new Vector3(1, 0, 0);
     private Vector3 flamesPos;

     public GameObject flamesObj;
     private GameObject flames;

     private float flames_CD;
     private float flameDeath;
     //private PlayerHealth health;

     // Use this for initialization
     void Start()
     {
          base.Start();
          flameDeath = TimeToLive;
          //health = GetComponent<PlayerHealth>();
          flames_CD = 0.2f;
     }

     // Update is called once per frame
     void Update()
     {
          if(flames_CD >= 0.2f)
          {
               flames_CD = 0;
               flamesPos = new Vector3(transform.position.x, 1, transform.position.z);
               flames = Instantiate(flamesObj, flamesPos, transform.rotation) as GameObject;
               Destroy(flames, flameDeath);
               flameDeath -= 0.1f;
          }
          
          flames_CD += Time.deltaTime;
     }

     public override Vector3 getOffset()
     {
          return dir;
         // return offset;
     }

     public override void Init(float facing)
     {
          Shoot(dir * -facing);
     }



}
