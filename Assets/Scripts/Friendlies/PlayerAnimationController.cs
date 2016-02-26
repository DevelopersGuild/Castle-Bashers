using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

    private Animator animator;
    private Player player;
    private MoveController moveController;
    private AttackController attackController;
    private CrowdControllable crowdControllable;
    private Health health;
    public bool isAttacking;


    // Use this for initialization
    void Start()
    {
        moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
        attackController = GetComponent<AttackController>();
        crowdControllable = GetComponent<CrowdControllable>();
        health = GetComponent<Health>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", moveController.isMoving);
            animator.SetBool("IsFlinched", moveController.GetFlinched());
            animator.SetBool("IsStunned", crowdControllable.getStun());
            animator.SetBool("IsKnockedBack", moveController.GetKnockedBack());
            animator.SetBool("IsJumping", player.getIsJumping());
            animator.SetBool("IsDead", player.getDown());
            animator.SetBool("IsGrounded", moveController.GetIsGrounded());
        }
    }
}
