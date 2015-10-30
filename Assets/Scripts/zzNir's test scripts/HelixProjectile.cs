using UnityEngine;
using System.Collections;

public class HelixProjectile : Projectile {

    private float theta;
    private Rigidbody rb;
    private Vector3 rotationVector, posVector;
    private SpriteRenderer sprRend;

	// Use this for initialization
	void Start () {
        base.Start();
        rb = GetComponent<Rigidbody>();
        sprRend = GetComponent<SpriteRenderer>();
        rotationVector = new Vector3(0, 0, 0);
        posVector = new Vector3(0, 0, 0);
        theta = 0;
        rb.velocity = new Vector3(-1,0,0) * projectileSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        
        //y moves in a sine wave, z moves in a circle
        posVector = transform.position;
        posVector.y += Mathf.Sin(theta) /100;
        posVector.z += (Mathf.Cos(theta)) / 20;
        transform.position = posVector;
       // rotationVector.z = ;
        float scale = Mathf.Abs(Mathf.Cos((theta + Mathf.PI/4) / 2f)) / 5 + 0.5f;
        transform.localScale = new Vector3(scale,scale,scale);
        //rb.velocity += rotationVector;
        theta += Time.deltaTime;
	}

    //add rotation too
    public void Shoot(Vector3 dir)
    {
        
    }
}
