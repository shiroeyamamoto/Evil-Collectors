using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Stun_Back : StateMachineBehaviour
{
    public float stunDistance;
    public float stunYPower;
    public float duration;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int directionInt = animator.GetInteger("FaceRight");
        Vector3 endJumpPos = animator.transform.position;
        endJumpPos.x -= directionInt * stunDistance;
        animator.transform.DOJump(endJumpPos, stunYPower, 1, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.SetTrigger("NextStep");
        });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("NextStep");
    }
}
