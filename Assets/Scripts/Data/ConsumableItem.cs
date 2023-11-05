using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create New Consumable Item", menuName = "Data/Item/ConsumableItem")]
public class ConsumableItem : ItemBase
{
    [SerializeField] private bool isCanUseInMyTeam;
    [SerializeField] private bool isCanUseInTargetTeam;
    
    public Transform startLocation;
    public float duration;
    
    [Header("HP")]
    [SerializeField][Min(0)] private int hpAmount;
    [SerializeField] private bool restoreMaxHP;
    
    [Header("Mana")]
    [SerializeField][Min(0)] private int manaAmount;
    [SerializeField] private bool restoreMaxMana;
    
    public override bool UseToMyTeam(List<Player> players){
        if (isCanUseInMyTeam) {
            foreach (var player in players) {
                DoUse(player);
            }
        }

        return isCanUseInMyTeam;
    }

    public override bool UseToTargetTeam(List<Player> players) {
        if (isCanUseInTargetTeam) {
            foreach (var player in players) {
                DoUse(player);
            }
        }

        return isCanUseInTargetTeam;
    }

    private void DoUse(Player player) {
        if (player.CurrentInfo.health > 0) {
            if (restoreMaxHP) {
                player.IncreaceHp(player.InfoDefaultSO.health);
            }
            else {
                player.IncreaceHp(hpAmount);
            }
                
            if (restoreMaxMana) {
                player.IncreaceMana(player.InfoDefaultSO.mana);
            }
            else {
                player.IncreaceMana(manaAmount);
            }
        }
    }
}
