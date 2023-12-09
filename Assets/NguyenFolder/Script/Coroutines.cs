using System.Collections;
using UnityEngine;
using System;

public class Coroutines : MonoBehaviour
{
    public void WaitForSeconds(float time, Action function)
    {
        StartCoroutine(DoFunction(time, function));
    }

    private IEnumerator DoFunction(float time, Action function)
    {
        yield return new WaitForSeconds(time);
        function();
    }
}
