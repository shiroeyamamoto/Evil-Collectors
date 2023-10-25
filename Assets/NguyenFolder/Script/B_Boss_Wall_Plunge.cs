using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class B_Boss_Wall_Plunge : StateMachineBehaviour
{
    public float jumpDuration;
    [SerializeField] float jumpForce;
    Animator animator;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        Vector3 endJumpPoint = ChooseSideToJump();

        float velocity = animator.GetComponent<BossController>().velocity;
        float distance = (float)Vector3.Distance(endJumpPoint, animator.transform.position);
        
        jumpDuration = (float)  (distance/velocity);

        Debug.Log(distance);

        

        animator.transform.DOJump(endJumpPoint, jumpForce, 1, jumpDuration).SetEase(Ease.Linear);

    }

    Vector3 ChooseSideToJump()
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
        

        randomChosenPosition = new Vector3(x, y);

        return randomChosenPosition;
    }

}
