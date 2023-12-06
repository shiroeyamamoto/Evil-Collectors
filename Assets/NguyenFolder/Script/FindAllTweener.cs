using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllTweener : MonoBehaviour
{
    public List<Transform> tweener;
    private void Start()
    {
        tweener = new List<Transform>();
        var tweenerList = DOTween.TotalActiveTweeners();
        Debug.Log(tweenerList);
    }
}
