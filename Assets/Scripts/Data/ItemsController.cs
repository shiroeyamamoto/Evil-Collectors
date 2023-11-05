using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private List<ItemBase> itemBases;
    private Player player;
    private List<Player> playerTeam;
    private void Init() {
        foreach (ItemBase item in itemBases) {
            if (item.isAutoUse ) {
                item.UseToMySelf(player);
                item.UseToMyTeam(playerTeam);
                item.UseToTarget(player);
                item.UseToTargetTeam(playerTeam);
            }
        }
    }
}