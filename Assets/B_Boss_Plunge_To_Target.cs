using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Plunge_To_Target : StateMachineBehaviour
{
    [SerializeField] float plungeDuration;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {
            Vector3 newTarget = 
                new Vector3(target.transform.position.x, 
                            target.transform.position.y 
                                - target.transform.lossyScale.y / 2
                                + animator.transform.lossyScale.y / 2, 
                            target.transform.position.z);
            animator.transform.DOMove(newTarget, plungeDuration);
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
