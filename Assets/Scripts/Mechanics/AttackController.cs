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
    private float cooldown;

    void Start()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        attack = false;
        tap = 0;
        cooldown = 0;
    }


    void Update()
    {
        //Debug.Log(player.GetMoveController().isStunned);
        cooldown -= Time.deltaTime;
        timer = Time.timeSinceLevelLoad;

        if (player.playerRewired!=null && player.playerRewired.GetButtonDown("Fire1") && !player.getInputDisabled() && !player.GetMoveController().isStunned && cooldown < 0)
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

        if (timer - lastPress > 0.2f)
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
        cooldown = .1f;
    }

    public void resetAttack()
    {
        
    }

    public void finishedAnimation()
    {

        player.setIsMoving(true);
        player.GetMoveController().enableMovement();
        anim.SetBool("Finished", true);
    }

    public void startedAnimation()
    {
        AudioSource.PlayClipAtPoint(player.attackAudio, player.transform.position);
        attack = true;
        anim.SetBool("Finished", false);
        if(player.GetMoveController().GetIsGrounded())
        {
            player.setIsMoving(false);
            player.GetMoveController().disableMovement();
        }
        // player.GetAttackCollider().GetComponent<DealDamage>().setDamage(gameObject.GetComponent<Player>().getPhysicalDamage());
        player.GetAttackCollider().GetComponent<DealDamage>().setDamage(20);
        player.GetAttackCollider().SetActive(true);
        StartCoroutine(attackCoroutine(0.1f));
    }

    IEnumerator attackCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.GetAttackCollider().SetActive(false);
    }

    public bool GetIsAttack()
    {
        return attack;
    }
}
