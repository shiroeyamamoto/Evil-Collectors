using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Jump_To_Player : StateMachineBehaviour
{
    public float jumpDuration, jumpHeight;
    public LayerMask groundLayer;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = Player.Instance.transform;
        if (player)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, Mathf.Infinity, groundLayer);
            if (hit)
            {
                Vector3 endPoint = hit.point;
                endPoint.y = hit.point.y + animator.transform.lossyScale.y / 2;
                //endPoint.y = player.position.y - player.lossyScale.y / 2 + animator.transform.lossyScale.y / 2;
                animator.transform.DOJump(endPoint, jumpHeight, 1, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                    animator.SetTrigger("NextStep");
                }) ;
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
