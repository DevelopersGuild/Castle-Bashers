using UnityEngine;
using System.Collections;

public class GrabAttack : MonoBehaviour
{

    private ExtendBehaviour eb;
    public GrabDamage grabDamage;
    private GrabDamage grabDamageObj;
    private bool hit = false;
    private bool throwAttack = false;
    private float dir;

    // Use this for initialization
    void Start()
    {
        eb = GetComponent<ExtendBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
    //    GameObject oth = other.gameObject;
    //    Player p = oth.GetComponent<Player>();

    //    if (!hit)
    //    {
    //        if (p)
    //        {
    //            hit = true;
    //            //shouldn't use eb, just move based on animation
    //            //eb.SetPause(true);

    //            //subject to change
    //            p.GetComponent<CrowdControllable>().addStun(2.5f);

    //            //grabDamage.transform.rotation?
    //            grabDamageObj = Instantiate(grabDamage, p.transform.position, p.transform.rotation) as GrabDamage;
    //            grabDamageObj.setTarget(p);
    //            //subject to change
    //            Destroy(grabDamageObj, 2.5f);

    //            //shouldn't use eb, just move based on animation (always at end of hair)
    //            //eb.Resume(2.5f);

    //            if (throwAttack)
    //            {
    //                grabDamageObj.setThrow();
    //                //when resume over, play throw animation (run method at end of animation, only works if throw)
    //                //how does this work with the animation? extend sets the animation frame instead of a running animation?
    //                //based on length I guess

    //                //new cc, thrown. after 0.3s, will stop if collided with ground and will apply a 0.3s stun. 
    //                //A velocity is applied when thrown, slowly loses y velocity 
    //                p.throwPlayer(new Vector3(UnityEngine.Random.Range(0, 300) / 100.0f * dir, 14, UnityEngine.Random.Range(-350, 350) / 100.0f));
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //after 2.5s, reverse animation
    //        //nothing else
    //    }
    //}

    //void OnCollisionEnter(Collision other)
    //{
    //    GameObject oth = other.gameObject;
    //    Player p = oth.GetComponent<Player>();

    //    if (!hit)
    //    {
    //        if (p)
    //        {
    //            hit = true;
    //            //shouldn't use eb, just move based on animation
    //            //eb.SetPause(true);

    //            //subject to change
    //            p.GetComponent<CrowdControllable>().addStun(2.5f);

    //            //grabDamage.transform.rotation?
    //            grabDamageObj = Instantiate(grabDamage, p.transform.position, p.transform.rotation) as GrabDamage;
    //            grabDamageObj.setTarget(p);
    //            //subject to change
    //            Destroy(grabDamageObj, 2.5f);

    //            //shouldn't use eb, just move based on animation (always at end of hair)
    //            //eb.Resume(2.5f);

    //            if (throwAttack)
    //            {
    //                grabDamageObj.setThrow();
    //                //when resume over, play throw animation (run method at end of animation, only works if throw)
    //                //how does this work with the animation? extend sets the animation frame instead of a running animation?
    //                //based on length I guess

    //                //new cc, thrown. after 0.3s, will stop if collided with ground and will apply a 0.3s stun. 
    //                //A velocity is applied when thrown, slowly loses y velocity 
    //                p.throwPlayer(new Vector3(UnityEngine.Random.Range(0, 300) / 100.0f * dir, 14, UnityEngine.Random.Range(-350, 350) / 100.0f));
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //after 2.5s, reverse animation
    //        //nothing else
    //    }
    }

    public void setThrow()
    {
        throwAttack = true;
    }

    public void setDir(float d)
    {
        dir = d;
    }
}
