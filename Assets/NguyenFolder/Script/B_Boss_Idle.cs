using DG.Tweening;
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
        Flip(animator);
        SetColor(animator, animator.transform.GetComponent<BossController>().normalColor, 1);
        this.animator = animator;
        ResetDelayTime();
        TransactionDelay(this.delayTime);
    }

    void Flip(Animator animator)
    {
        int directionInt = (Player.Instance.transform.position.x <= animator.transform.position.x) ? -1 : 1;
        animator.transform.DOScaleX(Mathf.Abs(animator.transform.lossyScale.x) * directionInt,0);
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
        animator.transform.Find("Body").GetComponent<SpriteRenderer>().color = color;
    }
    public List<string> actions;
    public void DecideAction()
    {
        int randomAction = Random.Range(0, actions.Count);
        this.animator.SetTrigger(actions[randomAction]);
    }

    public void ResetDelayTime()
    {
        this.delayTime = this.delayTimeStart;
    }
}
