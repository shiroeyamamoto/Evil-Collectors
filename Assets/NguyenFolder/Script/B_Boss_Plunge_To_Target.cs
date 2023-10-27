using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Plunge_To_Target : StateMachineBehaviour
{
    [SerializeField] float plungeDuration;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] RaycastHit2D rayTargetToGround;

    [Range(0, 1)]
    public float alphaValue = 0.75f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetColor(animator, Color.red, alphaValue);
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target)
        {
            float velocity = animator.GetComponent<BossController>().velocity;
            rayTargetToGround = Physics2D.Raycast(target.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
            
            if(rayTargetToGround)
            {
                Vector3 newTarget = 
                new Vector3(target.transform.position.x,
                            rayTargetToGround.point.y + animator.transform.lossyScale.y/2,
                            target.transform.position.z);

                plungeDuration = Vector2.Distance(newTarget, animator.transform.position) / velocity;
                animator.transform.DOMove(newTarget, plungeDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                    SetColor(animator, Color.red, 1);
                });
            }
        } 
    }


    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
}
