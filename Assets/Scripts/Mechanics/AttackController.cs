using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour
{
    private float lastPress;
    private float timer;
    private int tap;
    private bool attack;
    private Player player;
    private Animator anim;

    void Start()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        attack = false;
        tap = 0;
    }


    void Update()
    {

        timer = Time.timeSinceLevelLoad;
        if (player.playerRewired!=null)
            if (player.playerRewired.GetButtonDown("Fire1"))
            {
                lastPress = Time.timeSinceLevelLoad;
                if (tap > 2 && player.GetMoveController().GetIsGrounded())
                {
                    tap = 3;
                }
                else
                {
                    tap++;
                }
                Combo();
            }

        if (timer - lastPress > 0.25f)
        {
            tap = 0;
            anim.SetInteger("Tap", 0);
            attack = false;
        }
    }

    void Combo()
    {
        //WaitForSeconds(2);
            switch (tap)
            {

                case 0:
                    //Reset to Idle
                    break;

                case 1:
                    //Combo Start
                    anim.SetInteger("Tap", 1);

                    break;

                case 2:
                    //Combo Prolonged
                    anim.SetInteger("Tap", 2);
                    break;

                case 3:
                    //Combo Finished
                    anim.SetInteger("Tap", 3);
                    break;
            }
    }

    public void resetTap()
    {
        tap = 0;
        attack = false;
        anim.SetInteger("Tap", 0);
    }

    public void finishedAnimation()
    {
        player.GetAttackCollider().SetActive(false);
        player.setIsMoving(true);
        anim.SetBool("Finished", true);
    }

    public void startedAnimation()
    {
        AudioSource.PlayClipAtPoint(player.attackAudio, player.transform.position);
        attack = true;
        anim.SetBool("Finished", false);
        if(!player.GetMoveController().GetIsGrounded())
        {
            player.setIsMoving(false);
        }
        player.GetAttackCollider().GetComponent<DealDamage>().setDamage(gameObject.GetComponent<Player>().getPhysicalDamage());
        player.GetAttackCollider().SetActive(true);
    }

    public bool GetIsAttack()
    {
        return attack;
    }
}
