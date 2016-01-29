using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{

    private Animator animator;
    private MoveController moveController;
    public bool isAttacking;


    // Use this for initialization
    void Start()
    {
        moveController = GetComponent<MoveController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("IsAttacking", isAttacking);
            animator.SetBool("IsMoving", moveController.isMoving);
            animator.SetBool("IsFlinched", moveController.GetFlinched());
            animator.SetBool("IsKnockedBack", moveController.GetKnockedBack());

        }
    }
}
