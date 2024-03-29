using DG.Tweening;
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
        
        animator.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOFade(0, 0.25f)
        .OnStart(() =>
        {
            animator.transform.GetComponent<Collider2D>().isTrigger = true;
            animator.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        })
        .OnComplete(() =>
        {
            rb2d = animator.GetComponent<Rigidbody2D>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            isTeleport = true;
            animator.transform.position = (Vector2)player.transform.position + Vector2.up * heightOffset;
            animator.SetTrigger("NextStep");
            animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOFade(1, 0.25f).OnComplete(() =>
            {
                animator.DOKill();
                animator.transform.GetComponent<Collider2D>().isTrigger = false;
                animator.transform.GetComponent<Rigidbody2D>().gravityScale = 0;

            });
            
        });
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isTeleport)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;
            //SetColor(animator, Color.white, 1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = 1;
        animator.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.ResetTrigger("NextStep");
        //

    }

    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.transform.Find("Body").Find("Body").GetComponent<SpriteRenderer>().color = color;
    }

}
