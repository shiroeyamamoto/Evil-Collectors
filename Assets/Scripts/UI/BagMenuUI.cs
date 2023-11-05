using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagMenuUI : UIBase {
    
    [SerializeField] private List<ItemBase> items;
    [SerializeField] private Transform page_01;
    [SerializeField] private Transform page_02;
    [SerializeField] private List<SlotItemUI> slotItemUis_Bag;
    [SerializeField] private List<SlotItemUI> slotItemUis_Inv;
    [SerializeField] private Button btnSubmit;

    private List<ItemBase> currentItemsBag;
    private List<ItemBase> currentItemsInv;
    
    protected override void Start() {
        base.Start();

        foreach (var slot in slotItemUis_Bag) {
            slot.Reset();
        }
        
        foreach (var slot in slotItemUis_Inv) {
            slot.Reset();
        }
        
        btnSubmit.onClick.AddListener(() => {
            GameManager.Instance.SetItemsInventory(currentItemsInv);
            GameManager.Instance.LoadSceneLevel();
        });

        currentItemsBag = new List<ItemBase>();
        currentItemsInv = new List<ItemBase>();
        page_01.gameObject.SetActive(true);
        page_02.gameObject.SetActive(false);
        

        foreach (var slot in slotItemUis_Bag) {
            slot.OnClick += (x) => {
                currentItemsBag.Remove(x);
                slot.Reset();
                
                slotItemUis_Inv[currentItemsInv.Count].Init(x);
                currentItemsInv.Add(x);

                ResetUiWhenClick(slotItemUis_Bag, currentItemsBag);
            };
        }
        
        foreach (var slot in slotItemUis_Inv) {
            slot.OnClick += (x) => {
                currentItemsInv.Remove(x);
                slot.Reset();
                
                slotItemUis_Bag[currentItemsBag.Count].Init(x);
                currentItemsBag.Add(x);
                
                ResetUiWhenClick(slotItemUis_Inv, currentItemsInv);
            };
        }
        
        for (int i = 0; i < items.Count; i++) {
            slotItemUis_Bag[i].Init(items[i]);
            currentItemsBag.Add(items[i]);
        }
    }

    private void ResetUiWhenClick(List<SlotItemUI> list, List<ItemBase> items) {
        for (int i = 0; i < list.Count; i++) {
            if (i < items.Count) {
                list[i].Init(items[i]);
            }
            else {
                list[i].Reset();
            }
        }
    }

    public void ShowPage_01() {
        page_01.gameObject.SetActive(true);
        page_02.gameObject.SetActive(false);
    }
    
    public void ShowPage_02() {
        page_01.gameObject.SetActive(false);
        page_02.gameObject.SetActive(true);
    }
}
