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

    [Range(0, 1)]
    public float alphaValue = 0.25f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.1f, 0.1f);
        this.animator = animator;
        Vector3 endJumpPoint = ChooseSideToJump();

        float velocity = animator.GetComponent<BossController>().velocity;
        float distance = (float)Vector3.Distance(endJumpPoint, animator.transform.position);
        
        jumpDuration = (float)  (distance/velocity);

        

        animator.transform.DOJump(endJumpPoint, jumpForce, 1, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("complete");
            animator.SetTrigger("NextStep");
        });

    }
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
    Vector3 ChooseSideToJump()
    {
        leftSide = this.animator.transform.parent.Find("L_WallJumpRangeMin");
        rightSide = this.animator.transform.parent.Find("R_WallJumpRangeMax");
        Transform side;
        int randomSide = Random.Range(0, 2);
        //side = leftSide;
        float offsetX;
        if (randomSide == 0)
        {
            side = leftSide; offsetX = animator.transform.lossyScale.x / 2;
        } else
        {
            side = rightSide; offsetX = -animator.transform.lossyScale.x / 2;
        }
        
        
        Vector3 randomChosenPosition;
        float x = side.position.x + offsetX;
        float y = Random.Range(Mathf.Min(leftSide.position.y, rightSide.position.y), Mathf.Max(leftSide.position.y, rightSide.position.y));
        

        randomChosenPosition = new Vector3(x, y);

        return randomChosenPosition;
    }

}
