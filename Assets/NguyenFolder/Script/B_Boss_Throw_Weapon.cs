using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Throw_Weapon : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Transform weapon  = animator.transform.parent.Find("Boss Weapon 1");
        if (weapon)
        {
            Debug.Log(weapon.name);
            weapon.GetComponent<FinalBossWeapon>().WeaponId = 1;
            weapon.gameObject.SetActive(true);
        }
    }
}
