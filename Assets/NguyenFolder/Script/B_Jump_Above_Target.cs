using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class B_Jump_Above_Target : StateMachineBehaviour
{
    public Vector3 startJumpPos;
    [SerializeField] Vector2 endJumpPos;
    public float jumpDuration;
    public float jumpHeight;
    public int jumpStep = 1;
    public Transform particles;

    [Space]
    [Range(0,1)]
    public float alphaSprite = 0.75f;
    [Space]
    public float scaleValue;
    public float defaultScaleValue = 1;
    public float scaleSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetBehaviour<B_Boss_Idle>().Flip(animator);
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.1f, 0.1f);
        // set alpha before jump

        //startJumpPos = animator.transform.position;
        endJumpPos = animator.transform.parent.Find("JumpPosition").position;

        endJumpPos.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        endJumpPos.y = GameObject.FindGameObjectWithTag("Player").transform.position.y + 5;

        float velocity = animator.GetComponent<BossController>().velocity;
        jumpDuration = Vector2.Distance(endJumpPos, animator.transform.position) / velocity;
        PrepareJump(animator);
        /**/
    }
    void PlayParticle(Animator animator)
    {
        
        Vector3 particleSpawnPoint;
        particleSpawnPoint.x = animator.transform.position.x;
        particleSpawnPoint.y = animator.transform.position.y - Mathf.Abs(animator.transform.lossyScale.y) / 2;
        particleSpawnPoint.z = 0;
        Transform o = Instantiate(particles, particleSpawnPoint, Quaternion.identity, null);
        o.GetComponent<ParticleSystem>().Play();
        Destroy(o.gameObject, 10f);
    }

    void SetColor(Animator animator, Color color , float alpha)
    {
        color.a = alpha;
        animator.transform.Find("Body").GetComponent<SpriteRenderer>().color = color;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        animator.ResetTrigger("NextStep");
    }

    public void PrepareJump(Animator animator)
    {
        animator.transform.Find("Body").DOScaleY(scaleValue, scaleSpeed).SetEase(Ease.Linear);
        animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y - (1 - scaleValue) / 2, scaleSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y + (1 - scaleValue) / 2, scaleSpeed).SetEase(Ease.Linear);
            animator.transform.Find("Body").DOScaleY(1, scaleSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Jump(animator);
            });
        });
    }

    public void Jump(Animator animator)
    {
        animator.transform.DOJump(endJumpPos, jumpHeight, jumpStep, jumpDuration).SetEase(Ease.Linear).OnStart(() =>
                {
                    PlayParticle(animator);
                    animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                }).OnComplete(() =>
                {
                    SetColor(animator, animator.transform.GetComponent<BossController>().strongAttackColor, 1);
                    animator.SetTrigger("NextStep");
                });
    }
}
