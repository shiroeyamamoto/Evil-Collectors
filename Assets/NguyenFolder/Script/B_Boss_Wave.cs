using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Boss_Wave : StateMachineBehaviour
{
    public float moveDistance;
    public float velocity;
    public int scaleValue;
    [SerializeField] Transform Boss_Wave_Prefab_Left;
    [SerializeField] Transform Boss_Wave_Prefab_Right;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 bossPosition = animator.transform.parent.Find("BossTest").position;
        GameObject wave_left = Instantiate(Boss_Wave_Prefab_Left, bossPosition, Quaternion.identity, animator.transform.parent).gameObject;
        GameObject wave_right = Instantiate(Boss_Wave_Prefab_Right, bossPosition, Quaternion.identity, animator.transform.parent).gameObject;
        wave_left.transform.DOMoveX(bossPosition.x - moveDistance, moveDistance / velocity).OnUpdate(() =>
        {
            wave_left.transform.DOScale(scaleValue, 1);
        }).OnComplete(() =>
        {
            wave_left.transform.DOKill();
            Destroy(wave_left);
        });
        wave_right.transform.DOMoveX(bossPosition.x + moveDistance, moveDistance / velocity).OnUpdate(() =>
        {
            wave_right.transform.DOScale(scaleValue, 1);
        }).OnComplete(() =>
        {
            wave_right.transform.DOKill();
            Destroy(wave_right);
        });
        animator.SetTrigger("NextStep");
    }
}
