using UnityEngine;
using System.Collections;

public class MeteorMover : MonoBehaviour {
    private Vector3 moveIncrement;
    public float speed = 1;
    public float speedIncrement = 0.1f; //speeds meteor up by this amount each frame
    public GameObject destroyedParticle;
    public AudioClip destroyedSound;
    private DealDamage dealDamage;
	// Use this for initialization
	void Start () {
        moveIncrement = new Vector3(1, -2, 0);
	    dealDamage = GetComponent<DealDamage>();
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        speed += speedIncrement;
        gameObject.transform.position += moveIncrement * speed;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            Destroy(gameObject);
        }

        if (((other.GetComponent<Player>() && dealDamage.damagesPlayers) || (other.GetComponent<Enemy>() && dealDamage.damagesEnemies)) || other.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            if (destroyedParticle)
            {
                GameObject particle = (GameObject)Instantiate(destroyedParticle, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            }

            if (destroyedSound)
            {
                AudioSource.PlayClipAtPoint(destroyedSound, transform.position, 1);
            }
            Destroy(gameObject);
        }
    }

    public void faceRight()
    {
        moveIncrement = new Vector3(-1, -2, 0);
    }

    public void faceLeft()
    {
        moveIncrement = new Vector3(-1, -2, 0);
        gameObject.transform.Rotate(0, 180, 0);
    }
}
