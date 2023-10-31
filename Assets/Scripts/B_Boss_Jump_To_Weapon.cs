using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class B_Boss_Jump_To_Weapon : StateMachineBehaviour
{
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [SerializeField] float JumpForce;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // get boss
        Transform boss = animator.transform;
        Transform weapon = animator.transform.parent.Find("Boss Weapon 1");
        float endPointX = 0;
        float endPointY = 0;
        if (Physics2D.Raycast(weapon.position, Vector2.down, animator.transform.lossyScale.y / 2, groundLayer))
        {
            endPointY = weapon.position.y + animator.transform.lossyScale.y / 2;
        }
        else
        {
            endPointY = weapon.position.y;
        }
        endPointX = weapon.position.x;
        if (Physics2D.Raycast(weapon.position, Vector2.left, animator.transform.lossyScale.x / 2, wallLayer))
        {
            endPointX = weapon.position.x + animator.transform.lossyScale.x / 2;
            goto label;
        }
        else
        {
            endPointX = weapon.position.x;
        }
        if (Physics2D.Raycast(weapon.position, Vector2.right, animator.transform.lossyScale.x / 2, wallLayer))
        {
            endPointX = weapon.position.x - animator.transform.lossyScale.x / 2;
        }

    label:
        {
            Vector2 endPoint = new Vector2(endPointX, endPointY);
            if (boss && weapon)
            {
                boss.DOJump(endPoint, JumpForce, 1, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    weapon.gameObject.SetActive(false);
                    animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 1f);
                    animator.SetTrigger("NextStep");
                });
            }
        }
    }
    

}
