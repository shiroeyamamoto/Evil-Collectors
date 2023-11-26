using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class B_Boss_Wall_Plunge : StateMachineBehaviour
{
    public float jumpDuration;
    [SerializeField] float jumpForce;
    public LayerMask wallLayer;
    public float maxY;
    public float minY;
    Animator animator;
    public Transform wallSlamPrefab;
    [Range(0, 1)]
    public float alphaValue = 0.25f;

    [Space]
    public float scaleValue;
    public float scaleSpeed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        scaleAfterJump = animator.transform.lossyScale.x;
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.1f, 0.1f);
        int randomSide;
        Label:
        {
            randomSide = Random.Range(-1, 2);
        }
        RaycastHit2D hit;
        hit = Physics2D.Raycast(animator.transform.position, Vector2.right * randomSide, Mathf.Infinity, wallLayer);
        Vector3 endPointJump;
        endPointJump.x = hit.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * randomSide;
        endPointJump.y = Random.Range(minY, maxY);
        endPointJump.z = 0f;
        
        if (randomSide != 0)
        {
            //scaleAfterJump = randomSide * animator.transform.lossyScale.x;
            //animator.transform.DOScaleX(-scaleAfterJump, 0);
            PrepareJump(animator, endPointJump);
        }
        else
        {
            goto Label;
        }
    }

    void PlayParticle(Animator animator,Vector3 hitPoint)
    {
        Transform wallSlam = Instantiate(wallSlamPrefab, hitPoint, Quaternion.identity, null);
        wallSlam.GetComponent<ParticleSystem>().Play();
        Destroy(wallSlam.gameObject, 5);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");

        //animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.transform.Find("Body").GetComponent<SpriteRenderer>().color = color;
    }

    public void PrepareJump(Animator animator,Vector3 endPointJump)
    {
        animator.transform.Find("Body").DOScaleY(scaleValue, scaleSpeed).SetEase(Ease.Linear);
        animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y - (1 - scaleValue) / 2, scaleSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            animator.transform.Find("Body").DOLocalMoveY(animator.transform.Find("Body").localPosition.y + (1 - scaleValue) / 2, scaleSpeed).SetEase(Ease.Linear);
            animator.transform.Find("Body").DOScaleY(1, scaleSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Jump(animator,endPointJump);
            });
        });
    }

    public void Jump(Animator animator,Vector3 endPointJump)
    {
        animator.transform.DOJump(endPointJump, jumpForce, 1, jumpDuration).SetEase(Ease.Linear).OnStart(() =>
        {
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }).OnComplete(() =>
        {
            PlayParticle(animator, endPointJump);
            FlipAfterJump(animator,scaleAfterJump);
            animator.SetTrigger("NextStep");
        });
    }
    float scaleAfterJump;
    public void FlipAfterJump(Animator animator,float scale)
    {
        animator.transform.DOScaleX(scaleAfterJump, 0);
    }
}
