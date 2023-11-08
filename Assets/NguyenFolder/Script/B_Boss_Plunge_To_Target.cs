using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Plunge_To_Target : StateMachineBehaviour
{
    [SerializeField] float plungeDuration;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] RaycastHit2D rayTargetToGround;
    [SerializeField] Transform test;
    [Range(0, 1)]
    public float alphaValue = 0.75f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetColor(animator, animator.transform.GetComponent<BossController>().strongAttackColor, 1);

        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target.name);
        if (target)
        {
            float velocity = animator.GetComponent<BossController>().velocity;
            rayTargetToGround = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
            
            if(rayTargetToGround)
            {
                Debug.Log(rayTargetToGround.point);
                Vector2 newTarget = 
                new Vector2(target.transform.position.x,
                            rayTargetToGround.point.y + animator.transform.lossyScale.y/2
                            );

                plungeDuration = Vector2.Distance(newTarget, animator.transform.position) / velocity;
                animator.transform.DOMove(newTarget, plungeDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                    animator.SetTrigger("NextStep");
                });
            }
        } 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
}
