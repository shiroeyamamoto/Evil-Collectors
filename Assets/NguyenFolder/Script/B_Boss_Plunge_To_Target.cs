using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Plunge_To_Target : StateMachineBehaviour
{
    [SerializeField] float plungeDuration;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] RaycastHit2D rayTargetToGround;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target)
        {
            Debug.Log("found player");
            float velocity = animator.GetComponent<BossController>().velocity;
            Debug.Log(velocity);
            rayTargetToGround = Physics2D.Raycast(target.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
            
            if(rayTargetToGround)
            {
                Debug.Log("ray exist");
                Vector3 newTarget = 
                new Vector3(target.transform.position.x,
                            rayTargetToGround.point.y + animator.transform.lossyScale.y/2,
                            target.transform.position.z);

                plungeDuration = Vector2.Distance(newTarget, animator.transform.position) / velocity;
                animator.transform.DOMove(newTarget, plungeDuration).SetEase(Ease.Linear);
            }
        } 
    }

}
