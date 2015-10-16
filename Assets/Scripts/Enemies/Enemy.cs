using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

     [HideInInspector]
     public GameObject target;
     public Transform targetPos;
     public MoveController moveController;
     public bool isInvincible;
     public float invTime;
     public int EnemyID; //use for random generation. IDs must match with Area IDs

     private float velocityXSmoothing, velocityZSmoothing;

	// Use this for initialization
	public void Start () {
          //later on make it only target living players, priority on tanks
          target = FindObjectOfType<Player>().gameObject;
          targetPos = target.transform;
          moveController = GetComponent<MoveController>();
          isInvincible = false;
          invTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

     public GameObject GetTarget()
     {
          return target;
     }

     public void SetTarget(GameObject tar)
     {
          target = tar;
     }
     
     public bool getInvincible()
     {
          return isInvincible;
     }

     public void setInvTime(float t)
     {
          invTime = t;
          isInvincible = true;
     }

     public void Move(Vector3 velocity, float force = 1)
     {
          velocity.y = 0f;
          //velocity.x = Mathf.SmoothDamp(velocity.x, 6, ref velocityXSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
          //velocity.z = Mathf.SmoothDamp(velocity.z, 10, ref velocityZSmoothing, (moveController.collisions.below) ? 0.1f : 0.2f);
          moveController.Move(velocity * Time.deltaTime * force);
     }
}

