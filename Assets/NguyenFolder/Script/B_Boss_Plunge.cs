using UnityEngine;
using DG.Tweening;

public class B_Boss_Plunge : StateMachineBehaviour
{
    Rigidbody2D rb2d;
    public float plungeSpeed;
    [SerializeField] LayerMask groundLayer;
    public Transform particles;
    public AudioClip clip;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        plungeSpeed = animator.GetComponent<BossController>().velocity;
        rb2d = animator.GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit)
        {
            float endPointMoveY = hit.point.y + animator.transform.lossyScale.y/2;

            float plungeDuration = (animator.transform.position.y - endPointMoveY)/plungeSpeed;
            animator.transform.DOMoveY(animator.transform.position.y + 3, 0.2f).OnComplete(() =>
            {
                animator.transform.DOMoveY(endPointMoveY, plungeDuration).OnComplete(() =>
                {
                    SoundManager.PlaySound(clip);
                    //animator.transform.DOKill();
                    CameraController cameraController = Camera.main.GetComponent<CameraController>();
                    cameraController.ShakeCamera(0.5f, 0.5f);
                    animator.SetTrigger("NextStep");
                    PlayParticle(animator);
                });
            });
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb2d.velocity = Vector2.down * plungeSpeed;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
        SetColor(animator, animator.transform.GetComponent<BossController>().normalColor, 1);
        if (animator.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
        {
            animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
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
    void SetColor(Animator animator, Color color, float alpha)
    {
        color.a = alpha;
        animator.transform.Find("Body").GetComponent<SpriteRenderer>().color = color;
    }
}
