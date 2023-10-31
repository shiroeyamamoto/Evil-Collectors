using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class B_Boss_Plunge : StateMachineBehaviour
{
    Rigidbody2D rb2d;
    public float plungeSpeed;
    [SerializeField] LayerMask groundLayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetColor(animator, animator.transform.GetComponent<BossController>().strongAttackColor, 1);

        plungeSpeed = animator.GetComponent<BossController>().velocity;
        rb2d = animator.GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit)
        {
            float endPointMoveY = hit.point.y + animator.transform.lossyScale.y/2;

            float plungeDuration = (animator.transform.position.y - endPointMoveY)/plungeSpeed;
            animator.transform.DOMoveY(endPointMoveY, plungeDuration).OnComplete(() =>
            {
                CameraController cameraController = Camera.main.GetComponent<CameraController>();
                cameraController.ShakeCamera(0.5f, 0.5f);
                animator.SetTrigger("NextStep");
            });
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb2d.velocity = Vector2.down * plungeSpeed;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
        {
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
}
