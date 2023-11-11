using DG.Tweening;
using UnityEngine;

public class Boss_Lv1_Intro : StateMachineBehaviour
{
    [Header("Jump Force")]
    [Min(0.1f)]
    public float velocity;
    public float jumpPowerOffset;
    public int jumpNumbs;

    [Space]
    [Header("Camera")]
    public float cameraShakeForce;
    public float cameraShakeDuration;

    [Space]
    [Header("Particle")]
    public Transform groundSlamPrefab;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform startPosTransform = GameObject.Find("StartPosition").transform;
        Debug.Log(startPosTransform);
        if (!startPosTransform) return;
        Vector3 startPosition = startPosTransform.position;
        // Check point to ground
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, Mathf.Infinity);
        if (!hit) return;

        Vector3 endPoint ;
        endPoint.x = hit.point.x;
        endPoint.y = hit.point.y+ animator.transform.lossyScale.y/2;
        endPoint.z = 0;
        startPosition = endPoint;


        // Do Jump
        float distance = Vector3.Distance(startPosition, animator.transform.position);
        float duration = distance / velocity;
        animator.transform.DOJump(startPosition, velocity+ jumpPowerOffset, jumpNumbs, duration).SetEase(Ease.Linear).OnStart(() =>
        {
            Debug.Log("Jump Down Start");
        }).OnComplete(() =>
        {
            Debug.Log("Jump Down Complete");
            groundSlamPrefab = animator.transform.Find("GroundSlam");
            if(groundSlamPrefab)
            {
                groundSlamPrefab.position = new Vector3(animator.transform.position.x, animator.transform.position.y - animator.transform.lossyScale.y / 2, 0);
                groundSlamPrefab.GetComponent<ParticleSystem>().Play();
            }
            // Rung chấn khi nhảy xuống
            Camera.main.GetComponent<CameraController>().ShakeCamera(cameraShakeDuration, cameraShakeForce);
            // Rung chấn khi đe dọa 
            Camera.main.GetComponent<CameraController>().ShakeCamera(2, 0.1f, cameraShakeDuration + 1f, Ease.Linear, () => animator.SetTrigger("NextStep")) ;
        });
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
