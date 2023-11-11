using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Plunge_Diagonally : StateMachineBehaviour
{
    public float maxAngle;
    public float velocity;
    public LayerMask groundLayer;
    public List<Transform> swords;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swords = animator.GetComponent<BossController>().listSwords;
        // vector transform -> player
        Vector2 direction = Player.Instance.transform.position - animator.transform.position;
        float angle = CaculateAngle(direction, Vector2.down);
        Vector3 endPoint;
        int directionInt = direction.x < 0 ? -1 : 1;
        if (angle < maxAngle)
        {
            
            endPoint.x = Player.Instance.transform.position.x;
            endPoint.y = Physics2D.Raycast(Player.Instance.transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y + Mathf.Abs(animator.transform.lossyScale.y) / 2;
            endPoint.z = 0;
        } else
        {

            endPoint.x = Physics2D.Raycast(animator.transform.position, CaculateSecondVector(Vector2.down, directionInt * maxAngle), Mathf.Infinity,groundLayer).point.x;
            endPoint.y = Physics2D.Raycast(Player.Instance.transform.position, Vector2.down, Mathf.Infinity, groundLayer).point.y + Mathf.Abs(animator.transform.lossyScale.y) / 2;
            endPoint.z = 0;
        }
        animator.transform.DOMove(endPoint, Vector2.Distance(Player.Instance.transform.position, animator.transform.position) / velocity).SetEase(Ease.Linear).SetDelay(0.25f).

            OnComplete(() =>
            {
                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f, 0.5f);
                foreach(Transform t in swords)
                {
                    Destroy(t.gameObject);
                }
                swords.Clear();
                animator.SetTrigger("NextStep");
            });
        //Debug.Log("angle =" +angle);


        // vector transform -> max angle
    }

    public float CaculateAngle(Vector2 vector1 ,Vector2 vector2)
    {
        return Vector3.Angle(vector1,vector2);
    }

    public Vector2 CaculateSecondVector(Vector2 vector, float angle)
    {
        // Tạo quaternion xoay từ góc
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Nhân quaternion với vector A để tính toán vector B
        Vector2 vectorB = rotation * vector;
        return vectorB;
    }
}
