using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Move : StateMachineBehaviour
{
    [Range(-1, 1)]
    [SerializeField] int directionMove;
    public float moveTime;
    public float moveSpeed;


    [SerializeField] Rigidbody2D rb2d;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d = animator.GetComponent<Rigidbody2D>();
        directionMove = DecideDirectionToMove();

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2d.velocity = new Vector2(moveSpeed * directionMove, rb2d.velocity.y);
    }
    int DecideDirectionToMove()
    {
        int directionMove = 0;
        directionMoveLable: { directionMove = Random.Range(-1, 2); }
        
        if(directionMove == 0)
        {
            goto directionMoveLable;
        }
        return directionMove;
    }
}
