using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitcher : SingletonMonobehavious<ItemSwitcher>
{

    private ItemBase currentItemQuickKey;
    private int indexCurrentItem=0;

    [SerializeField] List<ItemBase> itemList; 

    public ItemBase CurrentItemQuickKey { get => currentItemQuickKey; set => currentItemQuickKey = value; }


    public Action<Sprite, Sprite> OnIconSwitch;
    public void Init()
    {
        //CurrentItemQuickKey = GameController.Instance.ItemBases[indexCurrentItem];

        CurrentItemQuickKey = itemList[indexCurrentItem];

        OnIconSwitch?.Invoke(CurrentItemQuickKey.itemIcon, itemList[indexCurrentItem == 0 ? 1 : 0].itemIcon);

    }

    private void Update()
    {

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            // Chuyển đổi vật phẩm dựa vào hướng lăng chuột
            if (scrollInput > 0)
            {
                SwitchItem(1); // Lên
            }
            else
            {
                SwitchItem(-1); // Xuống
            }
        }
    }

    private void SwitchItem(int direction)
    {
        // Thực hiện logic chuyển đổi vật phẩm ở đây
        // Sử dụng biến 'direction' để xác định hướng chuyển đổi (1: lên, -1: xuống)
        // Ví dụ: GameManager.Instance.SwitchItem(direction);

        if (direction > 0)
        {
            // Chọn item tiếp theo
            indexCurrentItem = (indexCurrentItem + 1) % itemList.Count;
        }
        else if(direction < 0)
        {
            // Chọn item trước đó
            indexCurrentItem = (indexCurrentItem - 1 + itemList.Count) % itemList.Count;
        }

        CurrentItemQuickKey = itemList[indexCurrentItem];

        if(CurrentItemQuickKey.itemTag == TagItem.Passives)
        {
            SwitchItem(direction);
        }
        ///Debug.Log("_----------------");
        //Debug.Log($"{CurrentItemQuickKey.itemName} - {CurrentItemQuickKey.itemTag}");

        OnIconSwitch?.Invoke(CurrentItemQuickKey.itemIcon, itemList[indexCurrentItem == 0 ? 1 : 0].itemIcon);

        // Cập nhật UI hoặc thông tin hiển thị với item hiện tại
        UpdateDisplay();
    }

    public void UseItem()
    {
        //Kamehameha kamehameha = itemX as Kamehameha;
        //kamehameha.UseToDir(Player.Instance.transform.position);

        CurrentItemQuickKey.UseItem();
        Debug.Log("ItemSwitcher");
    }

    private void UpdateDisplay()
    {
        // Cập nhật UI hoặc thông tin hiển thị với item hiện tại (inventory[currentItemIndex])
        // ...
    }
}
