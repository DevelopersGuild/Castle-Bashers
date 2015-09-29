using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
     public float startingHealth = 10;
     public float currentHealth;
     public int CoinValue;
     public bool isDead;
     public GameObject deathPilePrefab;
     private Vector2 knockbackDirection;

     private bool isQuitting;

     private Animator anim;
     private AudioSource enemyAudio;
     public ParticleSystem particle;
     public ParticleSystem deathParticle;
     private CameraFollow camera;

     public bool canKnock;

     // Use this for initialization
     void Start()
     {
          isDead = false;
          currentHealth = startingHealth;
          canKnock = true;
          camera = FindObjectOfType<CameraFollow>();
     }

     public void cancelKnockback()
     {
          canKnock = false;
     }

     public void replenish(float amt)
     {
          currentHealth += amt;
          if (currentHealth > startingHealth)
               currentHealth = startingHealth;
     }

     public void setHealth(float amt)
     {
          currentHealth = amt;
     }
     public float currentHp()
     {
          return currentHealth;
     }

     public void TakeDamage(float amount)
     {
          GameManager.Notifications.PostNotification(this, "OnHit");
          currentHealth -= amount;

          // Instantiate a particle effect if it has one
          if (particle != null && currentHealth > 0)
          {
               Instantiate(particle, transform.position, transform.rotation);
          }
          else if (deathParticle != null && currentHealth <= 0)
          {
               Instantiate(deathParticle, transform.position, transform.rotation);
          }

          // Shake the camera if its not a barrel
          if(!gameObject.CompareTag("Barrel"))
          {
               camera.CameraShake();
          }   

          if (currentHealth <= 0)
          {
               Death();
          }

         
     }

     //For triggers
     public void CalculateKnockback(Collider2D other, Vector2 currentPosition, float multiplier = 0.25f)
     {
          //Calculate point of collision and knockback accordingly
          Vector3 contactPoint = other.transform.position;
          Vector3 center = currentPosition;
          EnemyMoveController enemyMoveController = other.gameObject.GetComponent<EnemyMoveController>();
          PlayerMoveController playerMoveController = other.gameObject.GetComponent<PlayerMoveController>();

          if (canKnock)
          {
               if (enemyMoveController != null)
               {
                    Vector2 pushDirection = new Vector2(contactPoint.x - center.x, contactPoint.y - center.y);
                    knockbackDirection = pushDirection;
                    enemyMoveController.Knockback(pushDirection.normalized * multiplier);
               }
               else if (playerMoveController != null)
               {
                    Vector2 pushDirection = new Vector2(contactPoint.x - center.x, contactPoint.y - center.y);
                    playerMoveController.Knockback(pushDirection.normalized * multiplier);
               }

          }

     }

     public Vector2 getKnockback()
     {
          return knockbackDirection;
     }

     //For colliders
     public void CalculateKnockback(Collision2D other, Vector2 currentPosition, float multiplier = 1)
     {
          //Calculate point of collision and knockback accordingly
          Vector3 contactPoint = other.transform.position;
          Vector3 center = currentPosition;
          EnemyMoveController enemyMoveController = other.gameObject.GetComponent<EnemyMoveController>();
          PlayerMoveController playerMoveController = other.gameObject.GetComponent<PlayerMoveController>();

          if (enemyMoveController != null)
          {
               Vector2 pushDirection = new Vector2(contactPoint.x - center.x, contactPoint.y - center.y);
               enemyMoveController.Knockback(pushDirection.normalized * multiplier);
          }
          else if (playerMoveController != null)
          {
               Vector2 pushDirection = new Vector2(contactPoint.x - center.x, contactPoint.y - center.y);
               playerMoveController.Knockback(pushDirection.normalized * multiplier);
          }

     }

     public void Death()
     {
          isDead = true;
          if (gameObject.tag == "Player")
          {
               GameManager.Notifications.PostNotification(this, "OnPlayerDeath");
               this.setHealth(startingHealth);
          }
          else if (gameObject.GetComponent<Enemy>())
          {
               Enemy enem = gameObject.GetComponent<Enemy>();
               enem.onDeath();
          }
          else if (gameObject.GetComponent<Boss>())
          {
               Boss enem = gameObject.GetComponent<Boss>();
               enem.Shake();
               if (deathPilePrefab)
               {
                    Instantiate(deathPilePrefab, this.transform.position, Quaternion.identity);
               }
          }
          isDead = true;
          

          DropLoot dropLoot;
          if (dropLoot = GetComponent<DropLoot>())
          {
               //Dont drop loot if its a enemy spawning barrel
               if (!GetComponent<BarrelSpawn>())
               {
                    dropLoot.DropItem();
               }
          }
          if (!gameObject.GetComponent<Boss>())
          {
               Destroy(gameObject);
          }
     }

     void OnApplicationQuit()
     {
          isQuitting = true;
     }

     //Drop loot on death
     public void OnDestroy()
     {
          if (!isQuitting)
          {

          }
     }
}

