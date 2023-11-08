using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Attack : StateMachineBehaviour
{
    [Range(0, 10)]
    [SerializeField] int attackType;
    public int maxAttackType;
    [SerializeField] bool randomAttackType;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (randomAttackType)
        {
            attackType = Random.Range(0, maxAttackType + 1);
            
        }
        animator.SetInteger("AttackType", attackType);
        /*chooseAttackType:
            {
                if (randomAttackType)
                {
                    attackType = Random.Range(0, maxAttackType + 1);
                    animator.GetComponent<Boss_Level_1_Controller>().currentAttackType = attackType;
                }
            }

            if (animator.GetComponent<Boss_Level_1_Controller>().currentAttackType != animator.GetComponent<Boss_Level_1_Controller>().previousAttackType)
            {
                animator.GetComponent<Boss_Level_1_Controller>().previousAttackType = animator.GetComponent<Boss_Level_1_Controller>().currentAttackType;
                animator.SetInteger("AttackType", animator.GetComponent<Boss_Level_1_Controller>().currentAttackType);

            }
            else
            {
                goto chooseAttackType;
            }*/
    }
}
