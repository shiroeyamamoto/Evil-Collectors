using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Jump_To_Player : StateMachineBehaviour
{
    public float jumpDuration, jumpHeight;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    public float scaleChange;
    public float scaleSpeed;

    public AudioClip clipStart;
    public AudioClip clipEnd;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = Player.Instance.transform;
        if (player)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, Mathf.Infinity, groundLayer);
            if (hit)
            {
                int directionInt = (player.position.x <= animator.transform.position.x) ? -1 : 1;

                RaycastHit2D hitWall = Physics2D.Raycast(animator.transform.position, Vector2.right* directionInt, Mathf.Infinity, wallLayer);
                if (hitWall)
                {
                    float distanceToPlayer = Mathf.Abs(player.position.x - animator.transform.position.x);
                    Vector3 endPoint = hitWall.point;
                    //endPoint.x = hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;

                    float distanceToWall = Mathf.Abs(hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt - animator.transform.position.x);
                    if(distanceToWall <= distanceToPlayer)
                    {
                        endPoint.x = hitWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;

                    } else
                    {
                        endPoint.x = player.position.x;
                    }
                    endPoint.y = hit.point.y + animator.transform.lossyScale.y / 2;
                    animator.transform.Find("Body").DOScaleY(scaleChange, scaleSpeed).SetEase(Ease.Linear);
                    animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y - (1- scaleChange )/2, scaleSpeed).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y + (1 - scaleChange)/2, scaleSpeed).SetEase(Ease.Linear);
                        animator.transform.Find("Body").DOScaleY(1, scaleSpeed).SetEase(Ease.Linear).OnComplete(()=>{
                            animator.transform.DOJump(endPoint, jumpHeight, 1, jumpDuration).SetEase(Ease.Linear).OnStart(() =>
                            {
                                SoundManager.PlaySound(clipStart);
                                Transform groundSlamPrefab = animator.transform.Find("GroundSlam");
                                if (groundSlamPrefab)
                                {
                                    groundSlamPrefab.position = new Vector3(animator.transform.position.x, animator.transform.position.y - animator.transform.lossyScale.y / 2, 0);
                                    groundSlamPrefab.GetComponent<ParticleSystem>().Play();
                                }
                            }).OnComplete(() =>
                            {
                                SoundManager.PlaySound(clipEnd);

                                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                                Transform groundSlamPrefab = animator.transform.Find("GroundSlam");
                                if (groundSlamPrefab)
                                {
                                    groundSlamPrefab.position = new Vector3(animator.transform.position.x, animator.transform.position.y - animator.transform.lossyScale.y / 2, 0);
                                    groundSlamPrefab.GetComponent<ParticleSystem>().Play();
                                }

                                animator.SetTrigger("NextStep");

                            });
                        });
                    });
                }

            }
            
            
        }
    }

}
