using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotLevelUI : MonoBehaviour
{
    public Button btn;
    public TMP_Text txtLevel;

    private LevelSO levelSO;
    
    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadScene(levelSO);
        });
    }
    
    public void Init(LevelSO levelSO, string txt)
    {
        this.levelSO = levelSO;
        txtLevel.text = txt;
    }

    
}
