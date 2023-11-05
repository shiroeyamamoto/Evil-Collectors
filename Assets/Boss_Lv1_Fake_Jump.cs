using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Fake_Jump : StateMachineBehaviour
{
    public float numerator;
    public float denominator;

    public float heightOffset;
    float fraction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (denominator == 0) return;

        fraction = numerator / denominator;
        Transform player = Player.Instance.transform;
        if (player)
        {

        }
    }
}
