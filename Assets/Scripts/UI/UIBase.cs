using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    [SerializeField] protected Image BG;
    [SerializeField] protected Transform mainUI;

    private void Start() {
        if (BG.TryGetComponent(out Button btn)) {
            btn.onClick.AddListener(() => {
                Hide();
            });
        }
    }

    public void Show(float opacity = 0.7f, float timeShow = 0.75f) {
        gameObject.transform.DOScale(0, 0);
        gameObject.SetActive(true);
        if (BG) {
            BG.DOFade(opacity, 0f);
        }
        
        gameObject.transform.DOScale(1, timeShow).SetEase(Ease.InExpo);
    }
    
    public void Hide(float timeShow = 0.5f) {
        gameObject.transform.DOScale(0, timeShow).SetEase(Ease.InExpo).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
