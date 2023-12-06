using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EchoEffect : MonoBehaviour
{
    public Sprite echoSprite;
    public Color echoColor;
    public float echoCooldown;
    public float echoStartime;
    public Vector3 echoScale;
    public Vector3 echoOffset;
    public float fadeTime;

    public GameObject spawner;
    public bool useIdentityRotation;
    public void Start()
    {
        echoStartime = echoCooldown;
        spawner = new GameObject();
        spawner.AddComponent<SpriteRenderer>().color = echoColor;
    }
    private void Update()
    {
        if (echoStartime > 0)
        {
            echoStartime -= Time.deltaTime;
        } else
        {
            echoStartime = echoCooldown;
            GameObject gameobjectCreated = ObjectPoolManager.SpawnObject(spawner, transform.position + echoOffset, useIdentityRotation?Quaternion.identity : transform.rotation);
            //GameObject gameobjectCreated = new GameObject();
            gameobjectCreated.transform.localScale = echoScale;
            gameobjectCreated.GetComponent<SpriteRenderer>().DOFade(1, 0).OnComplete(() => {
            }); ;
            gameobjectCreated.GetComponent<SpriteRenderer>().sprite = echoSprite;
            gameobjectCreated.GetComponent<SpriteRenderer>().color = echoColor;
            gameobjectCreated.GetComponent<SpriteRenderer>().sortingOrder = -1;
            gameobjectCreated.GetComponent<SpriteRenderer>().DOFade(0, fadeTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                ObjectPoolManager.ReturnObjectToPool(gameobjectCreated);
                gameobjectCreated.transform.DOKill();
            });

        }
        
    }
}
