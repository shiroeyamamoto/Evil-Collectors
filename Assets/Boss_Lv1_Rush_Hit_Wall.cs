using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Rush_Hit_Wall : StateMachineBehaviour
{
    [Header("Camera")]
    public float shakeDuration;
    public float shakeForce;
    [Space]
    [Header("Movement")]
    public LayerMask wallLayer;
    public float velocity;
    [Space]
    [Header("Spawn Object")]
    public Transform damagableObjectPrefab;
    public int objectNumber;
    public int unitSizeNumber;
    public float startPosYMin;
    public float startPosYMax;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform tranformThis = animator.transform;
        // Find player right or left from current pos
        Transform player = Player.Instance.transform;
        if (player)
        {
            int isPlayerLeft = (player.position.x - tranformThis.position.x) <= 0 ? -1 : 1;

            RaycastHit2D hit = Physics2D.Raycast(tranformThis.position, Vector2.right * isPlayerLeft, Mathf.Infinity, wallLayer);
            if (hit)
            {
                float rushPointX = hit.point.x - Mathf.Abs(tranformThis.lossyScale.x) /2* isPlayerLeft;
                Debug.Log(rushPointX);
                float distance = Mathf.Abs(Mathf.Min(rushPointX, tranformThis.position.x) - Mathf.Max(rushPointX, tranformThis.position.x));
                float duration = distance / velocity;
                tranformThis.DOMoveX(rushPointX, duration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    animator.SetInteger("FaceRight", isPlayerLeft);
                    animator.SetTrigger("NextStep");
                    Camera.main.GetComponent<CameraController>().ShakeCamera(shakeDuration, shakeForce);
                    SpawnDamagableObject(damagableObjectPrefab, objectNumber,24,animator);
                });
            }
        }

    }
    float mapSize, mapUnitSize, randomOffset;
    void SpawnDamagableObject(Transform transform, int number , int max, Animator animator)
    {
        if (transform)
        {
            // Phân chia khu vực spawn , 24 khu vực
                //lấy giới hạn map 

            RaycastHit2D hitWallLeft = Physics2D.Raycast(animator.transform.position, Vector2.left, Mathf.Infinity, wallLayer);
            RaycastHit2D hitWallRight = Physics2D.Raycast(animator.transform.position, Vector2.right, Mathf.Infinity, wallLayer);
            Debug.Log("hit left" + hitWallLeft.point.x); Debug.Log("hit right" + hitWallRight.point.x);
            // kiểm tra tồn tại giới hạn 
            if (hitWallLeft && hitWallRight)
            {
                mapSize = Mathf.Abs(hitWallLeft.point.x - hitWallRight.point.x);
                mapUnitSize = mapSize / max;
                randomOffset = Random.Range(0, mapUnitSize);

                Debug.Log("mapSize" + mapSize);
                Debug.Log("mapUnitSize" + mapUnitSize);
                Debug.Log("randomOffset" + randomOffset);

                if (number > max || number <= 0)
                {
                    Debug.Log("out of size");
                    return;
                }
                for (int i = 0; i < number; i++)
                {
                    int randomIndex = Random.Range(0, max);
                    float positionX = hitWallLeft.point.x+randomIndex * mapUnitSize + randomOffset;
                    Transform o = Instantiate(transform, new Vector3(positionX, Random.Range(startPosYMin,startPosYMax), 0), Quaternion.identity, animator.transform.parent);
                }
                animator.SetTrigger("NextStep");
                Debug.Log("object not null");
                return;
            }

            
        }
        Debug.Log("object null");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("NextStep");
    }
}
