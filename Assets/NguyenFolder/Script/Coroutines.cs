using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.EditorApplication;

public class Coroutines : MonoBehaviour
{
    public void WaitForSeconds(float time,CallbackFunction function)
    {
        StartCoroutine(DoFunction(time, function));
    }
    public static IEnumerator DoFunction(float time,CallbackFunction function)
    {
        yield return new WaitForSeconds(time);
        function();
        
    }
}
