using UnityEngine;
using System.Collections;

public class DealDamageToPlayer : MonoBehaviour
{
    public float dmgAmount = 1;
    public float knockback = 4;
    public float flinch = 5;
    public bool IsPhysicalDamage = true;

    // Use this for initialization
    void Start()
    {

        //GetComponent Enemy or Hazard
        //get damage amount from Enemy or Hazard
        //Enemy can change his damage amount
    }

    // Update is called once per frame
    void Update()
    {

    }

    //If we make colliders appear on attacks, create OnCollisionEnter and OnTriggerEnter collisions
    //destroy collider after they hit something

    public void OnCollisionStay(Collision other)
    {
        //Check for player collision
        if (other.gameObject.GetComponent<Player>())
        {
            //Find components necessary to take damage and knockback
            GameObject playerObject = other.gameObject;
            Player player = playerObject.GetComponent<Player>();
            Defense defense = playerObject.GetComponent<Defense>();
            //Take damage if the player isnt already currently invincible
            if (!player.GetInvincible())
            {
                //Deal damage, knockback
                //get amt (1), dmgAmount(1) from Enemy/Hazard
                if(IsPhysicalDamage == true)
                {
                    defense.CheckPhysicalDefense(dmgAmount, knockback, flinch);
                }
                else
                {
                    defense.CheckMagicalDefense(dmgAmount, knockback, flinch);
                }
                
                player.SetInvTime(1.5f);
            }

            if (tag == "DProj")
                Destroy(gameObject);

        }
    }

    //Same code just make sure it happens
    public void OnTriggerStay(Collider other)
    {

        //Check for player collision
        if (other.gameObject.GetComponent<Player>())
        {
            //Find components necessary to take damage and knockback
            GameObject playerObject = other.gameObject;
            Player player = playerObject.GetComponent<Player>();
            Defense defense = playerObject.GetComponent<Defense>();
            //Take damage if the player isnt already currently invincible
            if (!player.GetInvincible())
            {
                //Deal damage, knockback
                //get amt (1), dmgAmount(1) from Enemy/Hazard
                if (IsPhysicalDamage == true)
                {
                    defense.CheckPhysicalDefense(dmgAmount, knockback, flinch);
                }
                else
                {
                    defense.CheckMagicalDefense(dmgAmount, knockback, flinch);
                }
                player.SetInvTime(1.5f);
                if (CompareTag("One Time"))
                {
                    Destroy(gameObject);
                }
            }


            if (tag == "DProj")
                Destroy(gameObject);

        }
    }
}
