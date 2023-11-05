using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_Lv1_Melee_Attack : StateMachineBehaviour
{
    public int directionInt;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = Player.Instance.transform;
        if (player)
        {
            directionInt = (player.position.x < animator.transform.position.x) ? 1 : -1;

            Vector3 scale = animator.transform.lossyScale;

            animator.transform.DOScaleX(directionInt * Mathf.Abs(scale.x),0);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 scale = animator.transform.lossyScale;
        animator.transform.DOScaleX(Mathf.Abs(scale.x), 0);
    }
}
