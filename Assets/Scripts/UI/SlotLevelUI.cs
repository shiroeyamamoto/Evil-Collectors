using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SlotLevelUI : MonoBehaviour {
    public Action OnClick;
    public Button btn;
    public TMP_Text txtLevel;

    private LevelSO levelSO;
    
    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            Debug.Log("Play Button Click");
            /*GameManager.Instance.SetLevelData(levelSO);
            foreach (SlotLevelUI slot in GameManager.Instance.listSlotLevelUI)
            {
                slot.btn.interactable = false;
            }
            //btn.interactable = false;
            OnClick?.Invoke();*/
        });
    }
    
    public void Init(LevelSO levelSO, string txt)
    {
        this.levelSO = levelSO;
        txtLevel.text = txt;
    }

    
}
