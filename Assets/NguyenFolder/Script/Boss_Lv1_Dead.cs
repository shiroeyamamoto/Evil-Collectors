using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boss_Lv1_Dead : StateMachineBehaviour
{
    public LayerMask groundLayer;
    [Space]
    public float jumpPower;
    public float jumpDuration;
    [Space]
    public float fadeDuration;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //  Jump To Ground
        RaycastHit2D hitToGround = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        Vector3 endPoint = hitToGround.point;
        endPoint.x = hitToGround.point.x;
        endPoint.y = hitToGround.point.y + animator.transform.lossyScale.y / 2;
        endPoint.z = 0f;

        animator.transform.DOJump(endPoint, jumpPower, 1, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOFade(0, fadeDuration).SetEase(Ease.Linear).OnStart(() =>
            {
                animator.transform.Find("Body")
                                    .Find("Eyes")
                                    .GetComponent<SpriteRenderer>()
                                    .DOFade(0, fadeDuration)
                                    .SetEase(Ease.Linear);
                animator.transform.Find("Body")
                .Find("Eyebrow")
                .GetComponent<SpriteRenderer>()
                .DOFade(0, fadeDuration)
                .SetEase(Ease.Linear);
            }).OnComplete(() =>
            {
                animator.GetComponent<Boss_Level_1_Controller>().OnDead();
            });
        });
    }
}
