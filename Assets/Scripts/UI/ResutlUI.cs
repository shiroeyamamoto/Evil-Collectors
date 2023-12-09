using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResutlUI : UIBase {
    public Action OnClick;
    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text txt;
    private void Start() {
        btn.onClick.AddListener(() => {
            // kill Tween
            var activeTween = DOTween.KillAll();
            OnClick?.Invoke();
        });
        txt.DOFade(0, 1f).SetLoops(-1);
    }
}
