using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Idle : StateMachineBehaviour
{
    public enum action { 
        Idle,Move,Attack,Evade
    }
    [SerializeField] float delayTime;
    [SerializeField] float delayTimeStart;
    Animator animator;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetColor(animator, animator.transform.GetComponent<BossController>().normalColor, 1);
        this.animator = animator;
        ResetDelayTime();
        TransactionDelay(this.delayTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    public void TransactionDelay(float delayTime)
    {
        if(delayTime > 0)
        {
            delayTime -= Time.deltaTime;
            this.delayTime = delayTime;
            TransactionDelay(this.delayTime);
        } else
        {
            DecideAction();
        }
    }
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
    public void DecideAction()
    {
        this.animator.SetTrigger("Attack");
    }

    public void ResetDelayTime()
    {
        this.delayTime = this.delayTimeStart;
    }
}
