using DG.Tweening;
using UnityEngine;

public class B_Boss_Jump_To_Weapon : StateMachineBehaviour
{
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [SerializeField] float JumpForce;
    public Transform particlesStart;
    public Transform particlesEnd;
    public AudioClip clip;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // get boss
        Transform boss = animator.transform;
        Transform weapon = animator.transform.parent.Find("Boss Weapon 1");
        int directionInt = (Player.Instance.transform.position.x <= animator.transform.position.x) ? -1 : 1;
        
        Vector3 endPoint;
        if (Physics2D.Raycast(weapon.position, Vector2.down, animator.transform.lossyScale.y / 2, groundLayer))
        {
            endPoint.y = weapon.position.y + animator.transform.lossyScale.y / 2;
        }
        else
        {
            endPoint.y = weapon.position.y;
        }
        endPoint.x = weapon.position.x;
        if (Physics2D.Raycast(weapon.position, Vector2.right * directionInt, Mathf.Infinity, wallLayer))
        {
            endPoint.x = weapon.position.x - Mathf.Abs(animator.transform.lossyScale.x) / 2 * directionInt;
            endPoint.z = weapon.position.z;
            boss.DOJump(endPoint, JumpForce, 1, 0.5f).SetEase(Ease.Linear).OnStart(() =>
            {
                PlayParticle(animator);
            })
                
                .OnComplete(() =>
            {
                //boss.DOKill();
                SoundManager.PlaySound(clip);
                PlayParticle(animator);
                weapon.gameObject.SetActive(false);
                animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 1f);
                animator.SetTrigger("NextStep");
            });

        }
    }
    void PlayParticle(Animator animator)
    {
        Vector3 particleSpawnPoint;
        particleSpawnPoint.x = animator.transform.position.x;
        particleSpawnPoint.y = animator.transform.position.y - Mathf.Abs(animator.transform.lossyScale.y) / 2;
        particleSpawnPoint.z = 0;
        Transform o = Instantiate(particlesEnd, particleSpawnPoint, Quaternion.identity, null);
        o.GetComponent<ParticleSystem>().Play();
        Destroy(o.gameObject, 10f);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
