using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : UIBase {
    public Action OnClick;
    
    [SerializeField] private SlotLevelUI slotLevelUI;
    [SerializeField] private Transform content;
    [SerializeField] private Button btnBack;

    private List<SlotLevelUI> slotLevelList;
    private void Start()
    {
        btnBack.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });
    }

    public void LoadLevels(LevelManagerSO data) {
        if (slotLevelList != null) {
            Remove();
        }
        
        slotLevelList = new List<SlotLevelUI>();
        GameManager.Instance.listSlotLevelUI = new List<SlotLevelUI>();
        slotLevelUI.gameObject.SetActive(true);
        
        for (int i = 0; i < data.Levels.Count; i++) {
            var slotLevel = Instantiate(slotLevelUI, content);
            string txt = "Level " + (i + 1);
            slotLevel.Init(data.Levels[i], txt);
            slotLevel.OnClick += OnClick;
            slotLevelList.Add(slotLevel);

            if (i > 0)
            {
                slotLevel.btn.interactable = false;
            }
            if(GameManager.Instance.levelTwoScene)
            {
                slotLevel.btn.interactable = true;
            }

            // truyền vào item level vào GameManager
            GameManager.Instance.listSlotLevelUI.Add(slotLevel);
        }

        slotLevelUI.gameObject.SetActive(false);
    }

    private void Remove()
    {
        foreach (var slot in slotLevelList)
        {
            Destroy(slot.gameObject);
        }
        foreach (var slot in GameManager.Instance.listSlotLevelUI)
        {
            Destroy(slot.gameObject);
        }
    }
}
