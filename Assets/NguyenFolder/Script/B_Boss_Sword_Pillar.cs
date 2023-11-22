using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using DG.Tweening;

public class B_Boss_Sword_Pillar : StateMachineBehaviour
{
    public int swordNumbs;
    public float distanceToOther;
    public Transform swordPrefab;
    public float fadeTime;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        listSwords = new List<Transform>();
        SpawnSwordMoveDirection(animator);
    }

    [SerializeField] List<Transform> listSwords;
    public void SpawnSwordMoveDirection(Animator animator)
    {
        Transform player = Player.Instance.transform;
        for (int i = 0; i < swordNumbs; i++)
        {
            int isRight = (i % 2 == 0) ? 1 : -1;
            float xSpawn = player.position.x + distanceToOther * isRight * ((i+1)/2);
            float ySpawn = player.position.y;

            Vector3 endPoint = new Vector3(xSpawn, ySpawn - 10,0);
            
            Transform sword = Instantiate(swordPrefab, endPoint,Quaternion.identity,null);
            animator.transform.GetComponent<BossController>().listSwords.Add(sword);
            sword.transform.GetComponent<SpriteRenderer>().DOFade(0.25f, fadeTime).OnComplete(() =>
            {
                //animator.SetTrigger("NextStep");
            });
            sword.name = "I am sword";
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("NextStep");
    }
}
