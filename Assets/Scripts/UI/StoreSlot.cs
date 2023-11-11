using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    public Func<DictionaryForItem, bool> OnBuy;
    private DictionaryForItem dic;
    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text txtContent, txtPrice;
    [SerializeField] private Image icon;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            bool result = OnBuy.Invoke(dic);
            if (result)
            {
                Debug.Log("Mua Thanh cong");
                btn.interactable = false;
            }
            else
            {
                Debug.Log("Mua k Thanh cong");
            }
        });
    }

    public void Init(DictionaryForItem dic)
    {
        this.dic = dic;
        this.icon.sprite = this.dic.ItemBase.itemIcon;
        this.txtContent.text = this.dic.ItemBase.itemDescription;
        this.txtPrice.text = this.dic.price + "$";
        if (dic.IsBought) {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }
}
