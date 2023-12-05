using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class FinalBossWeapon : MonoBehaviour
{
    Animator animator;
    Animator ownerAnimator;
    [SerializeField] LayerMask wallLayer;
    [Range(0, 3)]
    public int WeaponId;
    [SerializeField] int areaNumber = 7;
    [SerializeField] int areaIndex = 0;
    [SerializeField] float areaSize;
    [SerializeField] Transform minBoundPos;
    [SerializeField] Transform maxBoundPos;
    [Min(1)]
    public int projectTileNumbs;
    public int projectTileVelocity;
    public Transform weaponChild;
    [SerializeField]bool deleteChildAfterDisable;
    Transform bossPos;
    Transform targetPos;
    float moveDuration;
    [SerializeField] float moveDistance;

    [Min(0.0001f)]
    [SerializeField] float velocity;

    [Header("Particles")]
    [Space]
    public Transform groundSlamPrefab;
    public Transform wallSlamPrefab;
    private void OnEnable()
    {
        animator = transform.parent.Find("BossTest").GetComponent<Animator>();
        switch (WeaponId)
        {
            case 0: Weapon_0_Controller(); break;
            case 1: Weapon_1_Controller(); break;
            case 2: Weapon_2_Controller(); break;
            case 3: Weapon_3_Controller(); break;
            default: Debug.Log("Weapon Id Not Found"); break;
        }
    }

    [SerializeField] LayerMask groundNWallLayer;
    
    void Spawn_Projectile(int index)
    {
        float angleStep = 360f / projectTileNumbs;
        float startAngle = 0f;
        float endAngle = (startAngle + angleStep * index)*Mathf.Deg2Rad;
        float x = Mathf.Sin(endAngle);
        float y = Mathf.Cos(endAngle);
        GameObject o = Instantiate(weaponChild, transform).gameObject;
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * projectTileVelocity;
        StartCoroutine(DisableAfterTimeRoutine());

    }
    private void OnDisable()
    {
        if (deleteChildAfterDisable)
        {
            foreach (Transform o in transform)
            {
                Destroy(o.gameObject);
            }
        }
    }
    public float delayTime;
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delayTime);
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
        for (int i = 0; i < projectTileNumbs; i++)
        {
            Spawn_Projectile(i);
        }
    }
    public float disableTime = 5.0f;
    IEnumerator DisableAfterTimeRoutine()
    {
        yield return new WaitForSeconds(disableTime);
        transform.GetComponent<SpriteRenderer>().color = Color.cyan;

        gameObject.SetActive(false);
    }

    void Weapon_0_Controller()
    {
        Debug.Log("weapon 0 active");
        deleteChildAfterDisable = true;
        transform.GetComponent<SpriteRenderer>().color = Color.cyan;
        bossPos = transform.parent.Find("BossTest");
        transform.position = bossPos.position;

        areaSize = Mathf.Abs((transform.parent.Find("L_WallJumpRangeMin").position.x - transform.parent.Find("R_WallJumpRangeMax").position.x)) / areaNumber;

        float randomX = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.x + areaSize * areaIndex, transform.parent.Find("L_WallJumpRangeMin").position.x + areaSize * (areaIndex + 1));

        float randomY = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.y, transform.parent.Find("R_WallJumpRangeMax").position.y);

        Vector3 endPos = new Vector3(randomX, randomY, 0);
        moveDistance = Vector3.Distance(bossPos.position, endPos);
        moveDuration = (moveDistance / velocity);
        transform.DOMove(endPos, moveDuration).SetEase(Ease.Linear).OnComplete(() => {
            //transform.DOKill();
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(DelayAttack());
        });
    }

    [SerializeField] bool moveCompleted;
    [SerializeField]
    int directionInt;
    void Weapon_1_Controller()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        deleteChildAfterDisable = false;
        moveCompleted = false;
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
        transform.position = transform.parent.Find("BossTest").position;
        // Check direction left or right from player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            float distance = player.transform.position.x - transform.position.x;

            directionInt = (int)(distance / Mathf.Abs(distance));// -1 :1;
            Flip(directionInt);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * directionInt, Mathf.Infinity, wallLayer);
            if (hit)
            {
                float distanceToWall = Vector3.Distance(transform.position, hit.point);
                transform.DORotate(Vector3.zero, 0).OnComplete(() =>
                {
                    transform.DOMoveX(hit.point.x, (float)(Mathf.Abs(distanceToWall) / velocity)).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        //transform.DOKill();
                        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                        moveCompleted = true;
                        Transform wallSlam = Instantiate(wallSlamPrefab, transform.position, Quaternion.identity, null);
                        wallSlam.GetComponent<ParticleSystem>().Play();
                        Destroy(wallSlam.gameObject,5);
                        AnimatorParamSet();
                    });

                });
            }
        }
    }
    private void Weapon_2_Controller()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        bossPos = transform.parent.Find("BossTest");
        transform.position = bossPos.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            float angleRotate;
            float distanceX = -(player.transform.position.x - transform.position.x);
            float distance = Vector3.Distance(transform.position, player.transform.position);
            angleRotate = distanceX / distance;
            angleRotate = Mathf.Acos(angleRotate) * Mathf.Rad2Deg;
            float duration = distance / velocity;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, 
                                                player.transform.position - transform.position, 
                                                Mathf.Infinity, 
                                                groundNWallLayer);
            //Debug.Log(hit.point);
            Vector2 endPoint = hit.point;

            transform.DORotate(new Vector3(0, 0, angleRotate), 0).OnComplete(() => {
                transform.DOMove(endPoint, duration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    //transform.DOKill();
                    moveCompleted = true;
                    AnimatorParamSet();
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.25f, 0.25f);
                    animator.SetTrigger("NextStep");
                });
            });

        }
    }
    
    private void Weapon_3_Controller()
    {

    }
    void Flip(float faceRight)
    {
        transform.DOScaleX(-faceRight * Mathf.Abs(transform.lossyScale.x), 0f).OnComplete(() =>
        {
            //transform.DOKill();
        });
    }

    void AnimatorParamSet()
    {
        ownerAnimator = transform.parent.Find("BossTest").GetComponent<Animator>();
        if (ownerAnimator)
        {
            if(moveCompleted) ownerAnimator.SetTrigger("NextStep");
        }
    }
}
