using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Evade : StateMachineBehaviour
{
    public float heightJumpEvade;
    public float jumpXMaxDistance;
    public LayerMask wallLayer;
    public float duration;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        // get direction from transform to player 
        Transform player= Player.Instance.transform;
        if(player)
        {
            int directionInt = (player.position.x - animator.transform.position.x) <= 0 ? -1:1 ;

            RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, Vector2.left * directionInt, Mathf.Infinity, wallLayer);
            float newHitX = hit.point.x + directionInt * animator.transform.lossyScale.x / 2;
            
            if (hit)
            {
                Vector3 endPoint;
                if(Mathf.Abs(animator.transform.position.x - newHitX) >= jumpXMaxDistance)
                {
                    endPoint.x = animator.transform.position.x -  jumpXMaxDistance * directionInt;
                } else
                {
                    endPoint.x = newHitX;
                }
                endPoint.y = hit.point.y;
                endPoint.z = 0;
                animator.transform.DOJump(endPoint, heightJumpEvade, 1, duration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    animator.SetTrigger("NextStep");
                });
            }
        }
    }
}
