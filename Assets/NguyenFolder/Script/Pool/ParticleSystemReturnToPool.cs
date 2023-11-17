using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemReturnToPool : MonoBehaviour
{
    public float disableTime;
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    public void Awake()
    {
        StartCoroutine(ReturnToPoolAfterSecond(gameObject, disableTime));
    }

    public IEnumerator ReturnToPoolAfterSecond(GameObject gameobject,float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPoolManager.ReturnObjectToPool(gameobject);
    }
}
