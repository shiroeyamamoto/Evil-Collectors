using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject {
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    public bool isAutoUse = false;

    public virtual bool UseToMySelf(Player player) {
        return false;
    }
    
    public virtual bool UseToTarget(Player target) {
        return false;
    }
    
    public virtual bool UseToMyTeam(List<Player> players) {
        return false;
    }

    public virtual bool UseToTargetTeam(List<Player> players) {
        return false;
    }
    
}

public enum TagItem {
    Deco, 
    Active,
    Passives
}

public enum ItemID {
    Hp,
    Mana
}

