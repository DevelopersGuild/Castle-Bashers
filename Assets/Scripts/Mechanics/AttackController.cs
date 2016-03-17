using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour
{
    public DealDamage attackCollider;
    private float lastPress;
    private float timer;
    private bool isAttacking;
    private Player player;
    private Animator anim;
    private Queue attackQueue;
    public float coolDown;

    void Start()
    {
        player = GetComponentInParent<Player>();
        anim = GetComponent<Animator>();
        attackQueue = new Queue();
        isAttacking = false;
        coolDown = 0;
    }


    void Update()
    {
        coolDown -= Time.deltaTime;

        timer = Time.timeSinceLevelLoad;

        if (player.playerRewired != null && player.playerRewired.GetButtonDown("Fire1"))
        {
            lastPress = Time.timeSinceLevelLoad;
        }

        if (player.playerRewired != null && player.playerRewired.GetButtonDown("Fire1") && !player.getInputDisabled() && !player.GetMoveController().isStunned && coolDown < 0)
        {
            if(player.getIsJumping() && attackQueue.Count <=0)
                attackQueue.Enqueue(attackQueue.Count + 1);
            else if(attackQueue.Count <= 2)
                attackQueue.Enqueue(attackQueue.Count + 1);
        }

        if (attackQueue.Count > 0 && !isAttacking && !player.animator.GetBool("JumpAttack"))
        {
            if(player.GetMoveController().GetIsGrounded())
                Combo();
            else
            {
                JumpAttack();
            }
        }

        if (timer - lastPress > 0.2f && attackQueue.Count <= 0)
        {
            ResetTap();
            coolDown = 0;
            player.animator.SetBool("JumpAttack", false);
        }
    }

    void JumpAttack()
    {
        anim.SetBool("JumpAttack", true);
        isAttacking = true;
        InstantiateAttack();
    }

    void Combo()
    {
        switch ((int)attackQueue.Peek())
        {

            case 0:
                //Reset to Idle
                break;

            case 1:
                //Combo Start
                anim.SetInteger("Tap", 1);
                AttackTime(0.4f);
                break;

            case 2:
                //Combo Prolonged
                anim.SetInteger("Tap", 2);
                AttackTime(0.4f);
                break;
            case 3:
                //Combo Finished
                anim.SetInteger("Tap", 3);
                AttackTime(0.5f);
                break;
        }
    }

    public void ResetTap()
    {
        attackQueue.Clear();
        isAttacking = false;
        anim.SetInteger("Tap", 0);
        coolDown = .2f;
    }

    public void ResetIdle()
    {
        attackQueue.Clear();
        isAttacking = false;
        anim.SetInteger("Tap", 0);
    }

    public bool GetIsAttack()
    {
        return isAttacking;
    }

    public void AttackTime(float time)
    {
        isAttacking = true;
        player.setIsMoving(false);
        InstantiateAttack();
        StartCoroutine(activateAttack(time));
    }

    public void InstantiateAttack()
    {
        AudioSource.PlayClipAtPoint(player.attackAudio, player.transform.position);
        DealDamage attackColliderInstance = (DealDamage)Instantiate(attackCollider, player.GetAttackCollider().transform.position, Quaternion.identity);
        attackColliderInstance.setDamage(gameObject.GetComponent<Player>().getPhysicalDamage());
        Destroy(attackColliderInstance.gameObject, 0.2f);
    }

    IEnumerator activateAttack(float time)
    {
        yield return new WaitForSeconds(time);
        if(attackQueue.Count > 0)
            attackQueue.Dequeue();
        isAttacking = false;
    }
}
