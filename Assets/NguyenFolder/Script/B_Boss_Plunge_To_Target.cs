using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class B_Boss_Plunge_To_Target : StateMachineBehaviour
{
    public Vector2 newTarget;
    [SerializeField] float plungeDuration;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] RaycastHit2D rayTargetToGround;
    [SerializeField] Transform test;
    GameObject target;
    [Range(0, 1)]
    public float alphaValue = 0.75f;
    //
    public float velocity;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
        SetColor(animator, Color.red, alphaValue);
        /*target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target.name);
        if (target)
        {
            float velocity = animator.GetComponent<BossController>().velocity;
            rayTargetToGround = Physics2D.Raycast(animator.transform.position, target.transform.position - animator.transform.position, Mathf.Infinity, groundLayer);
            
            if(rayTargetToGround)
            {
                Debug.Log(rayTargetToGround.point);
                newTarget = 
                new Vector2(target.transform.position.x,
                            rayTargetToGround.point.y + 2
                            );
                Debug.Log("new target = " + newTarget);
                plungeDuration = Vector2.Distance(newTarget, animator.transform.position) / velocity;
                animator.transform.DOMove(newTarget, plungeDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    animator.transform.DOKill();
                    animator.SetTrigger("NextStep");
                    animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                    SetColor(animator, Color.red, 1);
                    
                });
            }
        } */
        target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target.name);
        if (target)
        {
            float velocity = animator.GetComponent<BossController>().velocity;
            rayTargetToGround = Physics2D.Raycast(animator.transform.position, target.transform.position - animator.transform.position, Mathf.Infinity, groundLayer);

            if (rayTargetToGround)
            {
                Debug.Log(rayTargetToGround.point);
                newTarget =
                new Vector2(target.transform.position.x,
                            rayTargetToGround.point.y + 2
                            );
                Debug.Log("new target = " + newTarget);
                plungeDuration = Vector2.Distance(newTarget, animator.transform.position) / velocity;
                
            }
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*animator.transform.DOMove(newTarget, plungeDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.transform.DOKill();
            animator.SetTrigger("NextStep");
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
            SetColor(animator, Color.red, 1);

        });*/
        if (rayTargetToGround)
        {
            if (Vector2.Distance(animator.transform.position, newTarget) > 0.001f)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, newTarget, velocity);
            } else
            {
                animator.SetTrigger("NextStep");
            }

        }
        

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
        // Debug.Log(newTarget);
    }
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
}
