using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Jump_To_Player : StateMachineBehaviour
{
    public float jumpDuration, jumpHeight;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = Player.Instance.transform;
        if (player)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, Mathf.Infinity, groundLayer);
            if (hit)
            {
                int directionInt = (player.position.x <= animator.transform.position.x) ? -1 : 1;

                RaycastHit2D hitWall = Physics2D.Raycast(animator.transform.position, Vector2.right* directionInt, Mathf.Infinity, wallLayer);
                if (hitWall)
                {
                    float distanceToPlayer = Mathf.Abs(player.position.x - animator.transform.position.x);
                    Vector3 endPoint = hitWall.point;
                    //endPoint.x = hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;

                    float distanceToWall = Mathf.Abs(hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt - animator.transform.position.x);
                    if(distanceToWall <= distanceToPlayer)
                    {
                        endPoint.x = hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;

                    } else
                    {
                        endPoint.x = player.position.x;
                    }
                    endPoint.y = hit.point.y + animator.transform.lossyScale.y / 2;
                    //endPoint.y = player.position.y - player.lossyScale.y / 2 + animator.transform.lossyScale.y / 2;
                    animator.transform.DOJump(endPoint, jumpHeight, 1, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                        animator.SetTrigger("NextStep");
                    });
                }
                
            }
            
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
