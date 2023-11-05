using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemUI : MonoBehaviour {
    public Action<ItemSO> OnClick;
    
    [SerializeField] private Image icon;
    [SerializeField] private Button btn;
    private ItemSO itemSo;

    private void Start() {
        btn.onClick.AddListener(() => {
            if (itemSo != null) {
                OnClick?.Invoke(this.itemSo);
            }
        });
    }

    public void Init(ItemSO itemSo) {
        this.itemSo = itemSo;
        icon.sprite = itemSo.itemIcon;
        icon.gameObject.SetActive(true);
    }

    public void Reset() {
        itemSo = null;
        icon.gameObject.SetActive(false);
    }
}
