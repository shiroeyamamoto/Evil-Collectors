using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : SingletonMonobehavious<StoreManager>
{
    [SerializeField] private List<DictionaryForItem> storeData;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool BuyItem(ItemBase itemBase) {
        foreach (var data in storeData)
        {
            if (data.ItemBase == itemBase) {
                if (GameManager.Instance.Coin < data.price) {
                    continue;
                }
                
                data.IsBought = true;
                GameManager.Instance.UseCoin(data.price);
                return true;
            }
        }

        return false;
    }
    
    
    
    
    
    
    
    
    public List<DictionaryForItem> StoreData => storeData;
}

[Serializable]
public class DictionaryForItem {
    public ItemBase ItemBase;
    public int price;
    public bool IsBought = false;
}
