using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : UIBase
{
    [SerializeField] private Button btnBackHome;
    [SerializeField] private StoreSlot storeSlot;
    [SerializeField] private Transform content; 
    protected override void Start()
    {
        base.Start();
        btnBackHome.onClick.AddListener(() => { GameManager.Instance.LoadSceneMenu(); });

        LoadUI();
    }

    private void LoadUI()
    {
        Remove();
        storeSlot.gameObject.SetActive(true);
        foreach (var dic in StoreManager.Instance.StoreData)
        {
            var slot = Instantiate(storeSlot, content);
            slot.Init(dic);
            slot.OnBuy += StoreManager.Instance.BuyItem;
        }
        
        storeSlot.gameObject.SetActive(false);
    }

    private void Remove()
    {
        foreach (Transform slot in content)
        {
            if (slot != storeSlot.transform)
            {
                Destroy(slot.gameObject);
            }
        }
    }
}