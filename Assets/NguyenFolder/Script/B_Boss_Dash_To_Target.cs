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

    [SerializeField] List<Transform> bossWeapons;
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
            animator.transform.DOMove(endDashPoint, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                animator.transform.DOKill();
            });
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(Transform bossweapon in animator.transform.parent)
        if (bossweapon.name == "Boss Weapon")
        {
            bossweapon.gameObject.SetActive(true);
        }
    }

}
