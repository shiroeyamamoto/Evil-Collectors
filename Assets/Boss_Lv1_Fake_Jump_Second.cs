using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump_Second : StateMachineBehaviour
{
    public float jumpDistanceX;
    public float jumpHeightY;

    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = Player.Instance.transform;
        if (player)
        {
            int directionInt = (player.transform.position.x <= animator.transform.position.x) ? -1 : 1 ;
            float distanceToPlayer = Mathf.Abs(player.transform.position.x - animator.transform.position.x);

            RaycastHit2D hitToWall = Physics2D.Raycast(animator.transform.position, Vector2.right * directionInt,Mathf.Infinity,wallLayer);
            if (hitToWall)
            {
                float jumpPointX = hitToWall.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;
                float distanceToWall = Mathf.Abs(jumpPointX - animator.transform.position.x);

                Vector3 endPoint = hitToWall.point;
                RaycastHit2D hitToGround = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
                if (hitToGround)
                {
                    if (distanceToWall <= distanceToPlayer+ jumpDistanceX)
                    {
                        endPoint.x = jumpPointX;
                    }
                    else
                    {
                        endPoint.x = player.transform.position.x;
                    }

                    endPoint.y = hitToGround.point.y + animator.transform.lossyScale.y;
                }


            }
        }
    }
}
