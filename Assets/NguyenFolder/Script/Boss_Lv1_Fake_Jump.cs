using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump : StateMachineBehaviour
{
    public float numerator;
    public float denominator;

    public float heightOffset;
    float fraction;
    public float jumpPower;
    public float jumpDuration;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (denominator == 0) return;

        fraction = numerator / denominator;
        Transform player = Player.Instance.transform;
        if (player)
        {
            int directionInt = (player.position.x <= animator.transform.position.x) ? -1 : 1;
            float distanceX = Vector2.Distance(player.position, animator.transform.position);
            float moveDistanceX = distanceX * fraction;

            Vector3 endPoint = animator.transform.position;
            endPoint.x = animator.transform.position.x + directionInt * moveDistanceX;
            endPoint.y = animator.transform.position.y + heightOffset;

            
            animator.transform.DOJump(endPoint, jumpPower, 1, jumpDuration).SetEase(Ease.Linear)
                /*.OnStart(() =>
            {
                jumpDurationCaculate = jumpDuration;
                value = 0f;
                delay = (jumpDuration * 0.2f) / 10;
            })*/
                /*.OnUpdate(() =>
            {
                jumpDurationCaculate -=  Time.deltaTime;
                timer += Time.deltaTime;
                if (jumpDurationCaculate < jumpDuration * 0.2f && timer >= delay)
                {
                    timer = 0; 
                    value += 0.1f;
                    animator.SetFloat("Blend", value);
                }
            })*/
            /*.OnComplete(() =>
            {
                animator.SetTrigger("NextStep");
                Debug.Log("First Jump Completed");
            })*/
            ;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
