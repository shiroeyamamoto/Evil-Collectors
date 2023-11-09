using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class B_Jump_Above_Target : StateMachineBehaviour
{
    public Vector3 startJumpPos;
    [SerializeField] Vector2 endJumpPos;
    public float jumpDuration;
    public float jumpHeight;
    public int jumpStep = 1;

    [Space]
    [Range(0,1)]
    public float alphaSprite = 0.75f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.1f, 0.1f);
        // set alpha before jump

        //startJumpPos = animator.transform.position;
        endJumpPos = animator.transform.parent.Find("JumpPosition").position;

        endJumpPos.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        endJumpPos.y = GameObject.FindGameObjectWithTag("Player").transform.position.y + 5;

        float velocity = animator.GetComponent<BossController>().velocity;
        jumpDuration = Vector2.Distance(endJumpPos, animator.transform.position) / velocity;
        
        animator.transform.DOJump(endJumpPos, jumpHeight, jumpStep, jumpDuration).SetEase(Ease.Linear).OnStart(() =>
        {
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }).OnComplete(() =>
        {
            animator.SetTrigger("NextStep");
        });
    }


    void SetColor(Animator animator, Color color , float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.ResetTrigger("NextStep");
    }
}
