using UnityEngine;
using System.Collections;

public class MaladyAnimationController : MonoBehaviour
{

    private Animator animator;
    private MoveController moveController;
    private Malady malady;


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
            //animator.SetBool("IsAttacking", isAttacking);
            animator.SetBool("IsMoving", moveController.isMoving);
        }
    }
}
