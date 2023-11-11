using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Dead : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Boss lv1 dead");
        //Destroy(animator.transform.parent.gameObject);
    }
}
