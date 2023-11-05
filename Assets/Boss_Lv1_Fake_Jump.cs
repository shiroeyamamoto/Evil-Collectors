using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump : StateMachineBehaviour
{
    public float numerator;
    public float denominator;

    public float heightOffset;
    float fraction;
    public float jumpPower;
    public float jumpDuration;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (denominator == 0) return;

        fraction = numerator / denominator;
        Transform player = Player.Instance.transform;
        if (player)
        {
            int directionInt = (player.position.x <= animator.transform.position.x) ? -1 : 1;
            float distanceX = Vector2.Distance(player.position, animator.transform.position);
            float moveDistanceX = distanceX * fraction;

            Vector3 endPoint = animator.transform.position;
            endPoint.x = animator.transform.position.x + directionInt * moveDistanceX;
            endPoint.y = animator.transform.position.y + heightOffset;

            animator.transform.DOJump(endPoint, jumpPower, 1, jumpDuration).OnComplete(() =>
            {
                animator.SetTrigger("NextStep");
                Debug.Log("First Jump Completed");
            });
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
