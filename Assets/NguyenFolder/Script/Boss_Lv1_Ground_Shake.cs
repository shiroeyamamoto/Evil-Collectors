using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lv1_Ground_Shake : StateMachineBehaviour
{
    [Header("Spawn Object")]
    public Transform damagableObjectPrefab;
    public int objectNumber;
    public int unitSizeNumber;
    public float startPosYMin;
    public float startPosYMax;
    float mapSize, mapUnitSize, randomOffset;
    public LayerMask wallLayer;
    Animator animator;
    
    public int directionInt;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        Transform player = Player.Instance.transform;
        if (player)
        {
            directionInt = (player.position.x < animator.transform.position.x) ? 1 : -1;

            Vector3 scale = animator.transform.lossyScale;

            animator.transform.DOScaleX(directionInt * Mathf.Abs(scale.x), 0);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 scale = animator.transform.lossyScale;
        animator.transform.DOScaleX(Mathf.Abs(scale.x), 0);
    }
    public void SpawnDamagableObject()
    {
        SpawnDamagableObject(damagableObjectPrefab, objectNumber, 24, this.animator);
    }
    public void SpawnDamagableObject(Transform transform, int number, int max, Animator animator)
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
                    float positionX = hitWallLeft.point.x + randomIndex * mapUnitSize + randomOffset;
                    Transform o = Instantiate(transform, new Vector3(positionX, Random.Range(startPosYMin, startPosYMax), 0), Quaternion.identity, animator.transform.parent);
                }
                animator.SetTrigger("NextStep");
                Debug.Log("object not null");
                return;
            }


        }
        Debug.Log("object null");
    }
}
