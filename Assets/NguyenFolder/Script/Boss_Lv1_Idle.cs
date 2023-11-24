using DG.Tweening;
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

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Flip(animator);
    }

    private void Flip(Animator animator)
    {
        int directionInt = (Player.Instance.transform.position.x <= animator.transform.position.x) ? 1 : -1;
        
        animator.transform.DOScaleX(Mathf.Abs(animator.transform.lossyScale.x) * directionInt,0);
    }

    string DecideAction()
    {
        int index = Random.Range(0, actions.Count);
        string action = actions[index];
        return action;
    }
}
