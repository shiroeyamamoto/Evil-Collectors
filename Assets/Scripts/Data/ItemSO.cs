using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Data/Item")]

public class ItemSO : ScriptableObject {
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    
    [Header("Parameters")]
    public TypeItem typeItem;
    public TypeBuff typeBuff;
    [Min(0)] public float value;
}

public enum TypeItem {
    none,
    Buff,
    AttackToTarget,
    Skill
}

public enum TypeBuff {
    none,
    HP,
    Mana
}
