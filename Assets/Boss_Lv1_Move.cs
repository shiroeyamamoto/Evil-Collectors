using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Move : StateMachineBehaviour
{
    public LayerMask wallLayer;
    [Min(1)]
    public float velocity = 1f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Choose position to move forward
        Transform playerTransform = Player.Instance.transform;
        int isFaceRight;
        if (playerTransform)
        {
            Debug.Log("player found");

            isFaceRight = (playerTransform.position.x <= animator.transform.position.x) ? -1 : 1;

            RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, Vector2.right * isFaceRight, Mathf.Infinity, wallLayer);
            if (hit)
            {
                float xHitNew = hit.point.x - isFaceRight * animator.transform.lossyScale.x;
                float xMin = Mathf.Min(xHitNew,animator.transform.position.x);
                float xMax = Mathf.Max(xHitNew, animator.transform.position.x);

                
                float xRandom = Random.Range(xMin,xMax);

                float distance = Mathf.Abs(xRandom - animator.transform.position.x);
                float duration = distance / velocity;
                animator.transform.DOMoveX(xRandom, duration).OnComplete(() =>
                {
                    animator.SetTrigger("NextStep");
                });
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
