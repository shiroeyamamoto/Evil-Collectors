using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class B_Boss_Wall_Plunge : StateMachineBehaviour
{
    [SerializeField] float jumpDuration;
    [SerializeField] float jumpForce;
    Animator animator;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        Vector3 endJumpPoint = ChooseSideToJump();
        animator.transform.DOJump(endJumpPoint, jumpForce, 1, jumpDuration);
    }

    Vector2 ChooseSideToJump()
    {
        leftSide = this.animator.transform.parent.Find("L_WallJumpRangeMin");
        rightSide = this.animator.transform.parent.Find("R_WallJumpRangeMax");
        Transform side;
        int randomSide = Random.Range(0, 2);
        if(randomSide == 0)
        {
            side = leftSide;   
        } else
        {
            side = rightSide;
        }

        Vector3 randomChosenPosition;
        float x = side.position.x;
        float y = Random.Range(Mathf.Min(leftSide.position.y, rightSide.position.y), Mathf.Max(leftSide.position.y, rightSide.position.y));
        float z = side.position.z;

        randomChosenPosition = new Vector3(x, y, z);

        return randomChosenPosition;
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
