using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Jump_To_Weapon : StateMachineBehaviour
{

    [SerializeField] float JumpForce;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // get boss
        Transform boss = animator.transform;
        Transform weapon = animator.transform.parent.Find("Boss Weapon 1");
        if (boss && weapon)
        {
            boss.DOJump(weapon.position, JumpForce, 1, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                weapon.gameObject.SetActive(false);
            });
        }
    }
}
