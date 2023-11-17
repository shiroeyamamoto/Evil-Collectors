using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump_Second : StateMachineBehaviour
{
    public float jumpDistanceX;
    public float jumpHeightY;
    public int numerator;

    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Transform player = Player.Instance.transform;
        if (player)
        {
            int directionInt = animator.GetBehaviour<Boss_Lv1_Fake_Jump>().directionInt;//(player.transform.position.x <= animator.transform.position.x) ? -1 : 1 ;
            float distanceToPlayer = Mathf.Abs(player.transform.position.x - animator.transform.position.x);
            
            RaycastHit2D hitToWall = Physics2D.Raycast(animator.transform.position, Vector2.right * directionInt,Mathf.Infinity,wallLayer);
            if (hitToWall)
            {
                float jumpPointX = hitToWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;
                float distanceToWall = Mathf.Abs(jumpPointX - animator.transform.position.x);
                float distanceJumpSecond = animator.GetBehaviour<Boss_Lv1_Fake_Jump>().distanceX * ((float)numerator / GetDenominator(animator));
                Debug.Log("distanceX " + animator.GetBehaviour<Boss_Lv1_Fake_Jump>().distanceX);
                Debug.Log("(float)numerator / GetDenominator(animator) "+(float)numerator / GetDenominator(animator));
                Debug.Log("distanceJumpSecond"+distanceJumpSecond);
                Vector3 endPoint = hitToWall.point;
                RaycastHit2D hitToGround = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
                if (hitToGround)
                {
                    if (distanceToWall <= jumpDistanceX)
                    {
                        endPoint.x = jumpPointX;
                    }
                    else
                    {
                        endPoint.x = animator.transform.position.x + distanceJumpSecond * directionInt;
                    }

                    endPoint.y = hitToGround.point.y + Mathf.Abs(animator.transform.lossyScale.y)/2;
                }

                animator.transform.DOJump(endPoint, jumpHeightY, 1, 0.75f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    animator.SetTrigger("NextStep");
                    
                    animator.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                });
            }
        }
    }
    public int GetDenominator(Animator animator)
    {
        int denominator = animator.GetBehaviour<Boss_Lv1_Fake_Jump>().denominator;
        return denominator;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
