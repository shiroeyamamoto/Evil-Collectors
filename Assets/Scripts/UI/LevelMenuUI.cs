using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : UIBase
{
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
        slotLevelUI.gameObject.SetActive(true);
        
        for (int i = 0; i < data.Levels.Count; i++) {
            var slotLevel = Instantiate(slotLevelUI, content);
            string txt = "Level " + (i + 1);
            slotLevel.Init(data.Levels[i], txt);
            slotLevelList.Add(slotLevel);
        }
        
        slotLevelUI.gameObject.SetActive(false);
    }

    private void Remove()
    {
        foreach (var slot in slotLevelList)
        {
            Destroy(slot.gameObject);
        }
    }
}
