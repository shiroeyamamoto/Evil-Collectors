using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class FinalBossWeapon : MonoBehaviour
{
    [Min(1)]
    public int projectTileNumbs;
    public int projectTileVelocity;
    public Transform weaponChild;

    Transform bossPos;
    Transform targetPos;
    float moveDuration;
    [SerializeField] float moveDistance;

    [Min(0.0001f)]
    [SerializeField] float velocity;
    private void OnEnable()
    {
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
        float randomX = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.x, transform.parent.Find("R_WallJumpRangeMax").position.x);
        float randomY = Random.Range(transform.parent.Find("L_WallJumpRangeMin").position.y, transform.parent.Find("R_WallJumpRangeMax").position.y);

        Vector3 endPos = new Vector3(randomX, randomY,0);
        moveDistance = Vector3.Distance(bossPos.position, endPos);
        moveDuration = (moveDistance/ velocity);
        transform.DOMove(endPos, moveDuration).SetEase(Ease.Linear).OnComplete(() => {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(DelayAttack());
        });
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
        foreach (Transform o in transform)
        {
            Destroy(o.gameObject);
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
}
