using UnityEngine;
using System.Collections;

public class TurretRange : MonoBehaviour
{

     public Turret turret;

     // Use this for initialization
     void Start()
     {

          //turret = GetComponent<Turret>();
     }

     // Update is called once per frame
     void Update()
     {

     }

     //If we make colliders appear on attacks, create OnCollisionEnter and OnTriggerEnter collisions
     //destroy collider after they hit something

     public void OnCollisionStay(Collision other)
     {
          if (other.gameObject.GetComponent<Enemy>())
          {
               Enemy enem = other.gameObject.GetComponent<Enemy>();
               turret.addEnemy(enem.gameObject);
               enem.SetTarget(turret.gameObject);
          }

     }

     //Same code just make sure it happens
     public void OnTriggerStay(Collider other)
     {
          if (other.gameObject.GetComponent<Enemy>())
          {
               Enemy enem = other.gameObject.GetComponent<Enemy>();
               turret.addEnemy(enem.gameObject);
               enem.SetTarget(turret.gameObject);
          }
     }

     public void OnCollisionExit(Collision other)
     {
          if (other.gameObject.GetComponent<Enemy>())
          {
               Enemy enem = other.gameObject.GetComponent<Enemy>();
               turret.removeEnemy(enem.gameObject);
               enem.SetTarget(null);
          }

     }

     //Same code just make sure it happens
     public void OnTriggerExit(Collider other)
     {
          if (other.gameObject.GetComponent<Enemy>())
          {
               Enemy enem = other.gameObject.GetComponent<Enemy>();
               turret.removeEnemy(enem.gameObject);
               enem.SetTarget(null);
          }
     }
}
