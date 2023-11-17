using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolAfterTime : MonoBehaviour
{
    public float returnTime;
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterSecond(returnTime));
    }
    

    public IEnumerator ReturnToPoolAfterSecond(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
