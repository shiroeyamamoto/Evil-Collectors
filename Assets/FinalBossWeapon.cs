using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FinalBossWeapon : MonoBehaviour
{
    [SerializeField] Transform bossPos;
    [SerializeField] Transform targetPos;
    float moveDuration;
    [SerializeField] float moveDistance;
    [SerializeField] bool isEnabled;

    [Min(0.0001f)]
    [SerializeField] float velocity;
    private void OnEnable()
    {
        isEnabled = true;
        //
        moveDuration = moveDistance / velocity;
        bossPos = transform.parent.Find("BossTest");
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = bossPos.position;
        Vector2 movePos;
        movePos.x = (bossPos.position.x - targetPos.position.x) < 0 ? 1 : -1;
        movePos.y = 1;

        Vector2 endPos = (Vector2)bossPos.position + movePos * moveDistance;

        transform.DOMove(endPos, moveDuration).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        isEnabled = false;
    }
    private void Start()
    {
        if (isEnabled)
        {
            
        }
    }
}
