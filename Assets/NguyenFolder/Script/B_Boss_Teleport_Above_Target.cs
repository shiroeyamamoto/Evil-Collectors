using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Teleport_Above_Target : StateMachineBehaviour
{
    [SerializeField] float heightOffset;
    [SerializeField] bool isTeleport = false;
    Rigidbody2D rb2d;


    [Range(0,1)]
    [SerializeField] float alphaValue = 0.75f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // set alpha before teleport
        SetColor(animator, animator.transform.GetComponent<BossController>().strongAttackColor, 1);

        //
        rb2d = animator.GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator.transform.position = (Vector2)player.transform.position + Vector2.up * heightOffset;
        isTeleport = true;
        // set alpha
        Color color = animator.GetComponent<SpriteRenderer>().color;
        color.a = alphaValue;
        animator.GetComponent<SpriteRenderer>().color = color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isTeleport)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 1;
        //

    }

    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
