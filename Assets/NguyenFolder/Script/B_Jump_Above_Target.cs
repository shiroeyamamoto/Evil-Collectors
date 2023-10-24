using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class B_Jump_Above_Target : StateMachineBehaviour
{
    public Vector3 startJumpPos;
    [SerializeField] Vector2 endJumpPos;
    public AnimationCurve jumpCurve;
    public float time;
    float jumpHeight;
    int jumpStep;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //startJumpPos = animator.transform.position;
        endJumpPos = animator.transform.parent.Find("JumpPosition").position;
        endJumpPos.y += GameObject.FindGameObjectWithTag("Player").transform.position.y + 5;
        animator.transform.DOJump(endJumpPos, jumpHeight, jumpStep, time);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*time += Time.deltaTime;
        Vector3 curvePos = Vector3.Lerp(startJumpPos, endJumpPos, time);
        curvePos.y += jumpCurve.Evaluate(time);
        animator.transform.position = curvePos;*/
    }
}
