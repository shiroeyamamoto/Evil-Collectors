using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class B_Boss_Sword_Dagger : StateMachineBehaviour
{
    public int swordNumber;
    public float angleRangeMin;
    public float angleRangeMax;
    public float force;
    public Transform swordPrefab;
    public Transform swordIndicatePrefab;
    public float fadeTime;
    public float existTime;
    public float delayOffset;
    public Coroutines c;
    public AudioClip clip;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float angleStep = (angleRangeMax - angleRangeMin) / (swordNumber);
        for (int  i= 0; i < swordNumber; i++)
        {
            float angle = angleRangeMin + (i * angleStep);
            //spawn indicate
            Transform o = ObjectPoolManager.SpawnObject(swordIndicatePrefab.gameObject, animator.transform.position, Quaternion.identity).transform;
            
            o.rotation = Quaternion.Euler(0, 0, ((animator.transform.lossyScale.x < 0) ? (90 - angle) : (angle - 90)));
            
            o.transform.GetComponent<SpriteRenderer>().DOFade(0.75f, fadeTime).SetDelay(i * delayOffset).SetEase(Ease.Linear).OnStart(() =>
            {
                
            }).OnComplete(() =>
            {
                
                MoveSword(o, animator, angle);
                
                
            });

        }

    }

    public void MoveSword(Transform o, Animator animator,float angle)
    {
        ObjectPoolManager.ReturnObjectToPool(o.gameObject);
        // Spawn Sword

        Transform o1 = ObjectPoolManager.SpawnObject(swordPrefab.gameObject, animator.transform.position, Quaternion.identity).transform;
            //Instantiate(swordPrefab, animator.transform.position, Quaternion.identity, null);
        o1.rotation = Quaternion.Euler(0, 0, (animator.transform.lossyScale.x < 0) ? (180 - angle) : angle); //

        AudioSource source = o1.GetComponent<AudioSource>();
        //Vector2 vector = Vector2.zero;
        source.clip = clip;
        source.Play();
        o1.GetComponent<Rigidbody2D>().AddForce(o1.right * force, ForceMode2D.Impulse);
        
        //Destroy(o1.gameObject, existTime);
    }
}
