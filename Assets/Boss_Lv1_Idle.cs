using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Idle : StateMachineBehaviour
{
    public List<string> actions;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string action = DecideAction();
        animator.SetTrigger(action);
    }

    string DecideAction()
    {
        int index = Random.Range(0, actions.Count);
        string action = actions[index];
        return action;
    }
}
