using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class B_Jump_Above_Target : StateMachineBehaviour
{
    public Vector3 startJumpPos;
    [SerializeField] Vector2 endJumpPos;
    public AnimationCurve jumpCurve;
    public float jumpDuration;
    public float jumpHeight;
    int jumpStep;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //startJumpPos = animator.transform.position;
        endJumpPos = animator.transform.parent.Find("JumpPosition").position;

        endJumpPos.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        endJumpPos.y = GameObject.FindGameObjectWithTag("Player").transform.position.y + 5;

        float velocity = animator.GetComponent<BossController>().velocity;
        jumpDuration = Vector2.Distance(endJumpPos, animator.transform.position) / velocity;

        animator.transform.DOJump(endJumpPos, jumpHeight, jumpStep, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
