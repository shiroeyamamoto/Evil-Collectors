using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Stun_Back : StateMachineBehaviour
{
    public float stunDistance;
    public float stunYPower;
    public float duration;

    public Transform groundSlamPrefab;

    public AudioClip clip;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int directionInt = animator.GetInteger("FaceRight");
        Vector3 endJumpPos = animator.transform.position;
        endJumpPos.x -= directionInt * stunDistance;
        animator.transform.DOJump(endJumpPos, stunYPower, 1, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            SoundManager.PlaySound(clip);
            animator.SetTrigger("NextStep");
            Transform groundSlamPrefab = animator.transform.Find("GroundSlam");
            if (groundSlamPrefab)
            {
                groundSlamPrefab.position = new Vector3(animator.transform.position.x, animator.transform.position.y - animator.transform.lossyScale.y / 2, 0);
                groundSlamPrefab.GetComponent<ParticleSystem>().Play();
            }
        });
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("NextStep");
    }
}
