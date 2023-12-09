using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class B_Boss_Dead : StateMachineBehaviour
{

    public LayerMask groundLayer;
    [Space]
    public float jumpPower;
    public float jumpDuration;
    [Space]
    public float fadeDuration;

    public BossStatus_SO bsso_current;
    public BossStatus_SO bsso_next;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //  Jump To Ground
        RaycastHit2D hitToGround = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        Vector3 endPoint = hitToGround.point;
        endPoint.x = hitToGround.point.x;
        endPoint.y = hitToGround.point.y + Mathf.Abs(animator.transform.lossyScale.y) / 2;
        endPoint.z = 0f;

        animator.transform.DOJump(endPoint, jumpPower, 1, jumpDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.transform.Find("Body").GetComponent<SpriteRenderer>().DOFade(0, fadeDuration).SetEase(Ease.Linear).OnStart(() =>
            {
                animator.transform.Find("Body").Find("Eyes").GetComponent<SpriteRenderer>().DOFade(0, fadeDuration).SetEase(Ease.Linear);
                animator.transform.Find("Body").Find("Eyes").Find("Eyebrow").GetComponent<SpriteRenderer>().DOFade(0, fadeDuration).SetEase(Ease.Linear);

            }).OnComplete(() =>
            {
                Debug.Log("Final Diad");
                //animator.DOKill();
                animator.transform.parent.gameObject.SetActive(false);//UI OnDead
                Player.Instance.OnDead?.Invoke(true);
                Player.Instance.OnDead(true);
                bsso_current.defeated = true;
                bsso_next.unlocked = true;
            });
        });
    }
}
