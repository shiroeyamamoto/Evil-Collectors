using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_On_Wall : StateMachineBehaviour
{
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; rb.bodyType = RigidbodyType2D.Kinematic;
        SetColor(animator, animator.transform.GetComponent<BossController>().normalColor, 1);

        animator.ResetTrigger("NextStep");
    }

    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.GetComponent<SpriteRenderer>().color = color;
    }
}
