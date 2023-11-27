using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_MoveFowardTarget : StateMachineBehaviour
{
    int faceToTarget = 0;
    Rigidbody2D rb2d;
    [SerializeField] float movespeed;
    [SerializeField] float alphaValue;
    [SerializeField] bool isColorCompleted = false;
    [SerializeField] float colorDuration;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetBehaviour<B_Boss_Idle>().Flip(animator);

        animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOColor(Color.white, 0).SetDelay(colorDuration).OnComplete(() =>
        {
            animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOColor(Color.red, 0).SetDelay(colorDuration).OnComplete(() =>
            {
                animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOColor(Color.white, 0).SetDelay(colorDuration).OnComplete(() =>
                {
                    animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOColor(Color.red, 0).SetDelay(colorDuration).OnComplete(() =>
                    {
                            isColorCompleted = true;
                    });
                });
            });
        });
        rb2d = animator.GetComponent<Rigidbody2D>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(faceToTarget != 0 && isColorCompleted)
        {
            rb2d.velocity = new Vector2(faceToTarget * movespeed, rb2d.velocity.y);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isColorCompleted = false;
        rb2d.velocity = Vector2.zero;
        // reset alpha
        Color color = animator.transform.Find("Body").GetComponent<SpriteRenderer>().color;
        color.a = 1;
        animator.transform.Find("Body").GetComponent<SpriteRenderer>().color = color;
        animator.SetTrigger("NextStep");
        animator.ResetTrigger("NextStep");
    }

}
