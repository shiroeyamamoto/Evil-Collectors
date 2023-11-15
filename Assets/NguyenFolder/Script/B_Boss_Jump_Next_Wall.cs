using DG.Tweening;
//using System;
using UnityEngine;

public class B_Boss_Jump_Next_Wall : StateMachineBehaviour
{
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public float swordMoveDuration;
    public float offsetHeight;
    public float jumpPower;
    public float jumpDuration;
    public int airAttackTimes;
    public float[] attackMomentTime;
    public bool[] isAttacked;
    public Transform swordPrefab;
    public float[] groundPosXs;
    public bool[] isSwordGrounded;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // setup
        attackMomentTime = new float[airAttackTimes];
        isAttacked = new bool[airAttackTimes];
        groundPosXs = new float[airAttackTimes];
        isSwordGrounded = new bool[airAttackTimes];

        // Find Next Side to Jump
        RaycastHit2D hitLeft = Physics2D.Raycast(animator.transform.position, Vector2.left, Mathf.Infinity, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(animator.transform.position, Vector2.right, Mathf.Infinity, wallLayer);

        float distanceL = Vector3.Distance(animator.transform.position, hitLeft.point);
        float distanceR = Vector3.Distance(animator.transform.position, hitRight.point);
        float mapSize = Mathf.Abs(hitLeft.point.x - hitRight.point.x);

        

        Vector3 endPoint;
        if (distanceL > distanceR)
        {
            endPoint.x = hitLeft.point.x + Mathf.Abs(animator.transform.lossyScale.x) / 2;
            endPoint.y = hitLeft.point.y + Random.Range(-offsetHeight, +offsetHeight);

        }
        else
        {
            endPoint.x = hitRight.point.x - Mathf.Abs(animator.transform.lossyScale.x) / 2;
            endPoint.y = hitRight.point.y + Random.Range(-offsetHeight, +offsetHeight);
        }
        endPoint.z = 0;
        endPoint.y = Mathf.Clamp(endPoint.y, 0.5f, 12f);

        float timer = 0;
        
        animator.transform.DOJump(endPoint, jumpPower, 1, jumpDuration).SetEase(Ease.Linear).OnStart(() => 
        {
            PlayParticle(animator, endPoint);
            timer = 0;
            float attackStep = jumpDuration / (airAttackTimes + 1);
            float attackPosStep = mapSize / (airAttackTimes + 1);
            for (int i = 0; i < airAttackTimes; i++)
            {
                // attackStep + i * attackStep
                attackMomentTime[i] = attackStep + i * attackStep;
                groundPosXs[i] = attackPosStep + i * attackPosStep;
            }

        })
            // spawn sword 
        .OnUpdate(() =>
        {
            timer += Time.deltaTime;
            for(int i = 0; i < airAttackTimes; i++)
            {
                if(timer >= attackMomentTime[i] && !isAttacked[i])
                {
                    isAttacked[i] = true;
                    Transform o = Instantiate(swordPrefab, animator.transform.position, Quaternion.identity, null);
                    animator.GetComponent<BossController>().listSwords.Add(o);
                    int index;
                    chosenRandomIndexToMove:
                    {
                        index = Random.Range(0, airAttackTimes);
                    }
                    if (isSwordGrounded[index])
                    {
                        goto chosenRandomIndexToMove;
                    } else
                    {
                        isSwordGrounded[index] = true;
                        Vector3 endPoint;
                        endPoint.x = hitLeft.point.x + groundPosXs[index];
                        endPoint.y = Physics2D.Raycast(o.position, Vector2.down, Mathf.Infinity, groundLayer).point.y;
                        endPoint.z = 0;

                        Vector3 vectorLook = o.position - endPoint;
                        float angle = Mathf.Atan2(vectorLook.y, vectorLook.x) * Mathf.Rad2Deg;
                        o.DORotate(new Vector3(0, 0, angle), 0);
                        o.DOMove(endPoint, swordMoveDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                        });
                    }
                }
            }
        })
        
        .OnComplete(() =>
        {
            PlayParticle(animator, endPoint);
            Debug.Log("Jump to Other size completed");
        });
    }

    public Transform wallSlamPrefab;
    void PlayParticle(Animator animator, Vector3 hitPoint)
    {
        Transform wallSlam = Instantiate(wallSlamPrefab, hitPoint, Quaternion.identity, null);
        wallSlam.GetComponent<ParticleSystem>().Play();
        Destroy(wallSlam.gameObject, 5);
    }
    public void CreateSpawnSword()
    {

    }
}
