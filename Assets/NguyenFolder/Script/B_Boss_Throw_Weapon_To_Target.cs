using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class B_Boss_Throw_Weapon_To_Target : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform bossWeapon = animator.transform.parent.Find("Boss Weapon 1");
        if (bossWeapon)
        {
            Debug.Log("Found Boss Weapon 1");
            bossWeapon.GetComponent<FinalBossWeapon>().WeaponId = 2;
            bossWeapon.gameObject.SetActive(true);
        }
    }


}
