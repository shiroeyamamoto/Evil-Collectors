using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Unity.VisualScripting;

public class FinalBossWeapon : MonoBehaviour
{
    Animator animator;
    Animator ownerAnimator;
    [SerializeField] LayerMask wallLayer;
    [Range(0, 3)]
    [SerializeField] int WeaponId;
    [SerializeField] int areaNumber = 7;
    [SerializeField] int areaIndex = 0;
    [SerializeField] float areaSize;
    [SerializeField] Transform minBoundPos;
    [SerializeField] Transform maxBoundPos;
    [Min(1)]
    public int projectTileNumbs;
    public int projectTileVelocity;
    public Transform weaponChild;
    bool deleteChildAfterDisable;
    Transform bossPos;
    Transform targetPos;
    float moveDuration;
    [SerializeField] float moveDistance;

    [Min(0.0001f)]
    [SerializeField] float velocity;
    private void OnEnable()
    {
        switch (WeaponId)
        {
            case 0: Weapon_0_Controller(); break;
            case 1: Weapon_1_Controller(); break;
            default:Debug.Log("Weapon Id Not Found"); break;
        }
    }
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
    private void Start()
    {

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
        deleteChildAfterDisable = true;
        // move by rad
        //moveDuration = moveDistance / velocity;
        transform.GetComponent<SpriteRenderer>().color = Color.cyan;
        bossPos = transform.parent.Find("BossTest");
        //targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = bossPos.position;
        //Vector2 movePos;
        //movePos.x = (bossPos.position.x - targetPos.position.x) < 0 ? 1 : -1;
        //movePos.y = 1;
        //

        //Vector2 endPos = (Vector2)bossPos.position + movePos * moveDistance;

        areaSize = Mathf.Abs((transform.parent.Find("L_WallJumpRangeMin").position.x - transform.parent.Find("R_WallJumpRangeMax").position.x)) / areaNumber;

        float randomX = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.x + areaSize * areaIndex, transform.parent.Find("L_WallJumpRangeMin").position.x + areaSize * (areaIndex + 1));

        float randomY = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.y, transform.parent.Find("R_WallJumpRangeMax").position.y);

        Vector3 endPos = new Vector3(randomX, randomY, 0);
        moveDistance = Vector3.Distance(bossPos.position, endPos);
        moveDuration = (moveDistance / velocity);
        transform.DOMove(endPos, moveDuration).SetEase(Ease.Linear).OnComplete(() => {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(DelayAttack());
        });
    }

    [SerializeField] bool moveCompleted;
    void Weapon_1_Controller()
    {
        deleteChildAfterDisable = false;
        moveCompleted = false;
        Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
        transform.position = transform.parent.Find("BossTest").position;
        // Check direction left or right from player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            float distance = player.transform.position.x - transform.position.x;
            int directionInt = (int)(distance / Mathf.Abs(distance));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * directionInt, Mathf.Infinity, wallLayer);
            if (hit)
            {
                transform.DOMoveX(hit.point.x, (float) (Mathf.Abs(distance) / velocity)).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                    moveCompleted = true;
                    AnimatorParamSet();
                });
            }
        }
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
