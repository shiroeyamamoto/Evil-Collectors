using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump : StateMachineBehaviour
{
    public int numerator;
    public int denominator;

    public float heightOffset;
    float fraction;
    public float jumpPower;
    public float jumpDuration;
    public int directionInt;
    public float distanceX;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (denominator == 0) return;

        fraction = (float)((float)numerator / (float)denominator);
        Debug.Log("fraction " + fraction);
        Transform player = Player.Instance.transform;
        if (player)
        {
            directionInt = (player.position.x <= animator.transform.position.x) ? -1 : 1;
            Debug.Log("directionInt " + directionInt);
            distanceX = Vector2.Distance(player.position, animator.transform.position);
            Debug.Log("distanceX " + distanceX);
            float moveDistanceX = distanceX * fraction;
            Debug.Log("moveDistanceX " + moveDistanceX);
            Vector3 endPoint = animator.transform.position;
            endPoint.x = animator.transform.position.x + directionInt * moveDistanceX;
            endPoint.y = animator.transform.position.y + heightOffset;
            endPoint.z = 0f;

            Debug.Log(endPoint); 
            
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
