using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class B_Boss_Move_Swords : StateMachineBehaviour
{
    public List<Transform> listSwords;
    public Transform swordPrefab;

    public float moveDistance;
    public float moveDuration;

    public float swordSpawnYPosition;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        listSwords = animator.GetComponent<BossController>().listSwords;
        MoveAllSwords(animator);
        
        
    }
    public AudioClip clip;
    void MoveAllSwords(Animator animator)
    {

        foreach (Transform t in listSwords) {
            Transform sword = Instantiate(swordPrefab, new Vector3(t.position.x, swordSpawnYPosition, 0), Quaternion.identity, null);
            sword.DORotate(new Vector3(0, 0, -90), 0).OnStart(() =>
            {
                SoundManager.PlaySound(clip);
            }).OnComplete(() =>
            {
                sword.DOMoveY(moveDistance, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(sword.gameObject);
                    DestroyAllSwords(animator);
                });
            });
        }
    }
    public void DestroyAllSwords(Animator animator)
    {
        foreach (Transform t in listSwords)
        {
            Destroy(t.gameObject);
        }
        animator.GetComponent<BossController>().listSwords.Clear();
        listSwords.Clear();
    }
}
