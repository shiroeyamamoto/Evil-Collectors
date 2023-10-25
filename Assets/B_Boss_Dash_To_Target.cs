using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Dash_To_Target : StateMachineBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float dashDistance;
    RaycastHit2D hit;
    public LayerMask wallLayer;

    [SerializeField] Vector2 endDashPoint;

    [SerializeField] Transform bossWeapon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            Vector2 dashPosition = new Vector2();

            int targetLeft = (animator.transform.position.x - target.transform.position.x) > 0 ? 1 : -1;

            hit = Physics2D.Raycast(animator.transform.position, Vector2.left * targetLeft,Mathf.Infinity,wallLayer);

            dashPosition = hit.point;
            if(dashPosition.x < animator.transform.position.x)
            {
                if (animator.transform.position.x - dashDistance > dashPosition.x) endDashPoint = new Vector2(animator.transform.position.x - dashDistance, dashPosition.y);
                else endDashPoint = dashPosition;
            } else
            {
                if (animator.transform.position.x + dashDistance < dashPosition.x) endDashPoint = new Vector2(animator.transform.position.x + dashDistance, dashPosition.y);
                else endDashPoint = dashPosition;
            }
            float velocity = animator.GetComponent<BossController>().velocity;
            duration = Vector2.Distance(endDashPoint, animator.transform.position) / velocity;
            animator.transform.DOMove(endDashPoint, duration).SetEase(Ease.Linear);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!bossWeapon.gameObject.activeSelf)
        {
            bossWeapon.gameObject.SetActive(true);
        }
    }

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
